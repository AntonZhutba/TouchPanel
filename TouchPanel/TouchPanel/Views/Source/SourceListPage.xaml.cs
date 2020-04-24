using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchPanel.ViewModels.Source;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouchPanel.Views.Source
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SourceListPage : ContentView
    {
        public SourceListPage()
        {
            InitializeComponent();
            BindingContext = new SourceListViewModel();
        }

        private void ListView_Scrolled(object sender, ScrolledEventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}