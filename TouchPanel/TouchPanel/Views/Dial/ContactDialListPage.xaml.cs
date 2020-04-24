using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchPanel.ViewModels.Dial;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouchPanel.Views.Dial
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDialListPage : ContentView
    {
        public ContactDialListPage()
        {
            InitializeComponent();
            BindingContext = new ContactDialListViewModel();
        }
    }
}