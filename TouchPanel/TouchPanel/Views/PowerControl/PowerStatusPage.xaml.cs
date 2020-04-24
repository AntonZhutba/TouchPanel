using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchPanel.Models.Enums;
using TouchPanel.ViewModels.PowerControl;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouchPanel.Views.PowerControl
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PowerStatusPage : ContentView
    {
        public PowerStatusPage(PowerStatusType powerStatus)
        {
            InitializeComponent();
            BindingContext = new PowerStatusViewModel(powerStatus);
        }
    }
}