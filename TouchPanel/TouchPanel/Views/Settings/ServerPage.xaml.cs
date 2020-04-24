using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchPanel.Models.Interfaces;
using TouchPanel.ViewModels.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouchPanel.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServerPage : ContentView
    {
        public ServerPage()
        {
            InitializeComponent();
            BindingContext = new ServerViewModel(this, Name);
        }

        public string Name { get => "Video Conference"; }        
    }
}