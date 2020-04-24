using System;
using System.Collections.Generic;
using System.Text;
using TouchPanel.Models.Settings;

namespace TouchPanel.Models
{
    public class ApplicationConfiguration : BaseNotifyPropertyChanged
    {
        private InterfaceSettings _interfaceSettings;
        private NetworkSettings _networkSettings;
        private ServerSettings _serverSettings;
        private DeskViewSettings _deskViewSettings;

        public ApplicationConfiguration()
        {
          
            NetworkSettings = new NetworkSettings();
            DeskViewSettings = new DeskViewSettings();
            ServerSettings = new ServerSettings();
        }

        public InterfaceSettings Interface
        {
            get { return _interfaceSettings; }
            set { SetProperty(ref _interfaceSettings, value); }
        }

        public NetworkSettings NetworkSettings
        {
            get { return _networkSettings; }
            set { SetProperty(ref _networkSettings, value); }
        }

        public ServerSettings ServerSettings
        {
            get { return _serverSettings; }
            set { SetProperty(ref _serverSettings, value); }
        }

        public DeskViewSettings DeskViewSettings
        {
            get { return _deskViewSettings; }
            set { SetProperty(ref _deskViewSettings, value); }
        }
    }
}
