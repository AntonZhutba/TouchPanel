using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TouchPanel.Models;
using TouchPanel.Models.Settings;
using TouchPanel.Services;
using Xamarin.Forms;

namespace TouchPanel.ViewModels.Settings
{
    public class NetworkViewModel : NavigationViewModel
    {
        private NetworkSettings _networkSettings;
        private ConfigurationService _configurationService;

        public NetworkViewModel(ContentView view, string name) : base(name, view)
        {
            _configurationService = DependencyService.Resolve<ConfigurationService>();
            _networkSettings = _configurationService.GetSharedConfiguration().NetworkSettings;
        }

        public NetworkSettings NetworkSettings
        {
            get { return _networkSettings; }
            set { SetProperty(ref _networkSettings, value); }
        }
        

        public ICommand ToggleDHPCCommand
        {
            get
            {
                return new Command(() =>
                {
                    NetworkSettings.IsDHCPEnabled = !NetworkSettings.IsDHCPEnabled;
                });
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new Command(async (x) =>
                {
                    await _configurationService.UpdateSettings(NetworkSettings);
                });
            }
        }

    }
}
