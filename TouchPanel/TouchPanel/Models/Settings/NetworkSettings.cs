using System;
using System.Collections.Generic;
using System.Text;

namespace TouchPanel.Models.Settings
{
    public class NetworkSettings : BaseNotifyPropertyChanged
    {
        private bool _isDHCPEnabled;

        public NetworkSettings()
        {

        }
        public NetworkSettings(NetworkSettings settings)
        {
            IPAddress = settings.IPAddress;
            PrimaryDNS = settings.PrimaryDNS;                   
            Gateway = settings.Gateway;
            SecondaryDNS = settings.SecondaryDNS;            
            Subnet = settings.Subnet;
            IsDHCPEnabled = settings.IsDHCPEnabled;
        }

        public string IPAddress { get; set; }

        public string PrimaryDNS { get; set; }

        public string Gateway { get; set; }

        public string Subnet { get; set; }

        public string SecondaryDNS { get; set; }

        

        public bool IsDHCPEnabled
        {
            get { return _isDHCPEnabled; }
            set { SetProperty(ref _isDHCPEnabled, value); }
        }


    }
}
