using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TouchPanel.Helpers;
using TouchPanel.Models;
using TouchPanel.Models.Settings;
using TouchPanel.Services;
using Xamarin.Forms;

namespace TouchPanel.ViewModels.Settings
{
    public class InterfaceViewModel : NavigationViewModel
    {
        private InterfaceSettings _interfaceSettings;
        private ConfigurationService _configurationService;

        public InterfaceViewModel(ContentView view, string name) : base(name, view)
        {
            _configurationService = DependencyService.Resolve<ConfigurationService>();
            _interfaceSettings = _configurationService.GetSharedConfiguration().Interface;
        }
        public InterfaceSettings InterfaceSettings
        {
            get { return _interfaceSettings; }
            set { SetProperty(ref _interfaceSettings, value); }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new Command(async (x) =>
                {
                    await _configurationService.UpdateSettings(InterfaceSettings);
                });
            }
        } 
    }
}
