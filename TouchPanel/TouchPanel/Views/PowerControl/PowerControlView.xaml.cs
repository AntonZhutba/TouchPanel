using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchPanel.ViewModels.PowerControl;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouchPanel.Views.Power
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PowerControlView : ContentView
    {
        public PowerControlView()
        {
            InitializeComponent();
            BindingContext = new PowerControlViewModel();
            this.BackgroundColor = new Color(173, 216, 230, 150);
        }
    }
}