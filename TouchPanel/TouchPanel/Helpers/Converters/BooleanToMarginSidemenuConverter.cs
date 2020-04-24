using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TouchPanel.Helpers.Converters
{
    class BooleanToMarginSidemenuConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? new Xamarin.Forms.Rectangle(1,.3, 300, 500) : new Xamarin.Forms.Rectangle(1, .3, 0, 500);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
