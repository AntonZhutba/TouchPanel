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
    public partial class PresetListView : ContentView
    {
        public PresetListView()
        {
            InitializeComponent();
        }
        public PresetListView(bool isShowAdvancedButton) : this()
        {
           
            BindingContext = new PresetListViewModel(isShowAdvancedButton);
        }
    }
}