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
    public class ServerViewModel : NavigationViewModel
    {
        private ServerSettings _serverSettings;
        private ConfigurationService _configurationService;

        public ServerViewModel(ContentView view, string name) : base(name, view)
        {
            ConferenceSystems = new List<string>()
            {
                "Abs system",
                "RTofilas rnk"
            };
            _configurationService = DependencyService.Resolve<ConfigurationService>();
            _serverSettings = _configurationService.GetSharedConfiguration().ServerSettings;
        }

        public List<string> ConferenceSystems { get; set; }

        public ServerSettings ServerSettings
        {
            get { return _serverSettings; }
            set { SetProperty(ref _serverSettings, value); }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new Command(async (x) =>
                {
                    await _configurationService.UpdateSettings(ServerSettings);
                });
            }
        }
    }
}
