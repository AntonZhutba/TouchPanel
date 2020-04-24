using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TouchPanel.Helpers;
using TouchPanel.Models;
using TouchPanel.Models.Interfaces;
using TouchPanel.Models.Settings;
using TouchPanel.Services;
using TouchPanel.Services.Interfaces;
using Xamarin.Forms;

namespace TouchPanel.ViewModels
{
    public class BaseNavigationViewModel : BaseViewModel, ISharedConfigurationViewModel
    {
        private string _panelName = "West Bollsta - meeting room";
        private ApplicationConfiguration _configuration;     

        public BaseNavigationViewModel()
        {
            //var configService = DependencyService.Resolve<ConfigurationService>();
            //ApplicationConfiguration = configService.GetConfigurationFromProperties() ;

        }

        public string PanelName
        {
            get { return _panelName; }
            set { SetProperty(ref _panelName, value); }
        }

        public ICommand GoBackCommand
        {
            get
            {
                return new Command(() => DependencyService.Get<INavigationService>().Pop());
            }
        }

        public ApplicationConfiguration ApplicationConfiguration
        {
            get { return _configuration; }
            set { SetProperty(ref _configuration, value); }
        }
    }
}
