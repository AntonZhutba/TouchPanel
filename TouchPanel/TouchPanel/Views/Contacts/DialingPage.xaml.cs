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
    public partial class DialingPage : ContentView
    {
        public DialingPage(Contact contact)
        {
            InitializeComponent();
            BindingContext = new DialingViewModel(contact);            
        }

        private void Slider_DragCompleted(object sender, EventArgs e)
        {
            ((DialingViewModel)this.BindingContext).ChangeVolume();
        }
    }
}