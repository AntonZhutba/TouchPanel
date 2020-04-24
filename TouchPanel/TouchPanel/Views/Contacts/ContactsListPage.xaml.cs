using Android.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchPanel.ViewModels.Contacts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouchPanel.Views.Contacts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsListPage : ContentView
    {
        public ContactsListPage()
        {
            InitializeComponent();
            this.BindingContext = new ContactsListViewModel();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var x = (VisualElement)sender;
            
        }
    }
}