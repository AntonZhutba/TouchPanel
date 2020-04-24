using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TouchPanel.Helpers;
using TouchPanel.Models;
using TouchPanel.Models.Interfaces;
using TouchPanel.Models.Settings;
using TouchPanel.ViewModels;
using Xamarin.Forms;

namespace TouchPanel.Services
{
    public class ConfigurationService
    {
        private ISharedConfigurationViewModel _sharedViewModel;
        private const string ConfigFileName = "conf";

        public ConfigurationService()
        {
            _sharedViewModel = DependencyService.Resolve<ISharedConfigurationViewModel>();
            _sharedViewModel.ApplicationConfiguration = GetConfigurationFromPCL().Result;
        }

        public async Task UpdateSettings(NetworkSettings settings)
        {
            _sharedViewModel.ApplicationConfiguration.NetworkSettings = new NetworkSettings(settings);
            await SaveToPCL();
        }

        public async Task UpdateSettings(InterfaceSettings settings)
        {
            _sharedViewModel.ApplicationConfiguration.Interface = new InterfaceSettings(settings);
            await SaveToPCL();
        }

        public async Task UpdateSettings(ServerSettings settings)
        {
            _sharedViewModel.ApplicationConfiguration.ServerSettings = new ServerSettings(settings);
            await SaveToPCL();
        }

        public async Task UpdateSettings(DeskViewSettings settings)
        {
            _sharedViewModel.ApplicationConfiguration.DeskViewSettings = new DeskViewSettings(settings);
            await SaveToPCL();
        }

        public async Task UpdateSettings(ServerSettings server, NetworkSettings network, DeskViewSettings deskview)
        {
            _sharedViewModel.ApplicationConfiguration.DeskViewSettings = new DeskViewSettings(deskview);
            _sharedViewModel.ApplicationConfiguration.ServerSettings = new ServerSettings(server);
            _sharedViewModel.ApplicationConfiguration.NetworkSettings = new NetworkSettings(network);
            await SaveToPCL();
        }

        public ApplicationConfiguration GetSharedConfiguration()
        {
            return _sharedViewModel.ApplicationConfiguration;
        }

        public async Task<ApplicationConfiguration> GetConfigurationFromPCL()
        {

            if (!await PCLHelper.IsFileExistAsync(ConfigFileName))
            {
                return new ApplicationConfiguration()
                {  
                    DeskViewSettings = new DeskViewSettings()
                    {
                        PanelName = "West Bollsta - meeting room",
                        CurrentBackgroundImage = "Background_Blue.jpg"
                    }
                };
            }


            var content = PCLHelper.ReadAllTextAsync(ConfigFileName);
            var config = JsonConvert.DeserializeObject<ApplicationConfiguration>(content);
            return config;
        }

        public async Task SaveToPCL()
        {
            if (!await PCLHelper.IsFileExistAsync(ConfigFileName))
            {
                await PCLHelper.CreateFile(ConfigFileName);
            }

            var configJson = JsonConvert.SerializeObject(_sharedViewModel.ApplicationConfiguration);
            await PCLHelper.WriteTextAllAsync(ConfigFileName, configJson);
        }
    }
}
