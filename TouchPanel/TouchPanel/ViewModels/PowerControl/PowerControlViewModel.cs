using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TouchPanel.Helpers;
using TouchPanel.Models;
using TouchPanel.Models.Enums;
using TouchPanel.Services.Interfaces;
using TouchPanel.Views.PowerControl;
using Xamarin.Forms;

namespace TouchPanel.ViewModels.PowerControl
{
    public class PowerControlViewModel : BaseViewModel
    {
        public PowerControlViewModel()
        {
        }

        public ICommand SleepCommand
        {
            get
            {
                return new Command((x) =>
                    DependencyService.Get<INavigationService>()
                   .Push(new NavigationView(new PowerStatusPage(PowerStatusType.Sleep)))
                );
            }
        }

        public ICommand RestartCommand
        {
            get
            {
                return new Command((x) =>
                    DependencyService.Get<INavigationService>()
                   .Push(new NavigationView(new PowerStatusPage(PowerStatusType.Restart)))
               );
            }
        }

        public ICommand ShutdownCommand
        {
            get
            {
                return new Command((x) =>
                    DependencyService.Get<INavigationService>()
                    .Push(new NavigationView(new PowerStatusPage(PowerStatusType.Shutdown)))
                );
            }
        }
    }
}
