using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace TouchPanel.CustomControls
{
    public class RepeaterView : StackLayout
    {
        private static bool first = true;

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(RepeaterView)
           );

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(ICollection),
            typeof(RepeaterView),
            null,
            BindingMode.OneWay,
            propertyChanged: ItemsChanged);

        public RepeaterView()
        {
            Spacing = 0;
        }

        public ICollection ItemsSource
        {
            get => (ICollection)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set 
            { 
                SetValue(ItemTemplateProperty, value);
                first = false;
            }

        }

        protected virtual View ViewFor(object item)
        {
            View view = null;

            if (ItemTemplate != null)
            {
                var content = ((DataTemplate)ItemTemplate).CreateContent();

                view = content is View ? content as View : ((ViewCell)content).View;

                view.BindingContext = item;
            }

            return view;
        }

        private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {

                var control = bindable as RepeaterView;

                if (control == null) return;

                control.Children.Clear();

                var items = (ICollection)newValue;

                if (items == null) return;

                foreach (var item in items)
                {
                    control.Children.Add(control.ViewFor(item));
                }
            


        }
    }
}
