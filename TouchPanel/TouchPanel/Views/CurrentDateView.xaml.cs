using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchPanel.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouchPanel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentDateView : ContentView
    {
        public CurrentDateView()
        {
            InitializeComponent();
            BindingContext = new CurrentDateViewModel();
        }
    }
}