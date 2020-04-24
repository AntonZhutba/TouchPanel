using System;
using System.Collections.Generic;
using System.Text;

namespace TouchPanel.Models.Settings
{
    public class ServerSettings
    {
        public ServerSettings()
        {

        }
        public ServerSettings(ServerSettings settings)
        {
            ConferenceSystem = settings.ConferenceSystem;
            CiscoUsername = settings.CiscoUsername;
            CiscoPassword = settings.CiscoPassword;
            CiscoPort = settings.CiscoPort;
            IPAddress = settings.IPAddress;
        }

        public string ConferenceSystem { get; set; }

        public string IPAddress { get; set; }

        public string CiscoUsername { get; set; }

        public string CiscoPassword { get; set; }

        public int CiscoPort { get; set; }
    }
}
