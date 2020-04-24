using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchPanel.Models;
using TouchPanel.ViewModels.Contacts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouchPanel.Views.Contacts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDetailsPage : ContentView
    {
        public ContactDetailsPage()
        {
            InitializeComponent();
            this.BindingContext = new ContactDetailsViewModel();
        }

        public ContactDetailsPage(Contact contact)
        {
            InitializeComponent();
            this.BindingContext = new ContactDetailsViewModel(contact);
        }
    }
}