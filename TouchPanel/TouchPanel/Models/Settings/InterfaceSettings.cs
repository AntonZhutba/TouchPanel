using System;
using System.Collections.Generic;
using System.Text;

namespace TouchPanel.Models.Settings
{
    public class InterfaceSettings
    {
        public InterfaceSettings() { }

        public InterfaceSettings(InterfaceSettings settings)
        {
            PanelName = settings.PanelName;
            Location = settings.Location;
            IsSafeMode = settings.IsSafeMode;
            IsPinRequiredToSelectSource = settings.IsPinRequiredToSelectSource;
            IsShutdownAllowed = settings.IsShutdownAllowed;
            IsRestartAllowed = settings.IsRestartAllowed;
            IsChangingServerSettingsAllowed = settings.IsChangingServerSettingsAllowed;
            IsConnectToDeskView = settings.IsConnectToDeskView;
            IsKeepPannelAfterPowerLoss = settings.IsKeepPannelAfterPowerLoss;
            IsRememberLastUser = settings.IsRememberLastUser;
            IsRememnerLastCalls = settings.IsRememnerLastCalls;
        }


        public string PanelName { get; set; }

        public string Location { get; set; }

        public bool IsSafeMode { get; set; }

        public bool IsPinRequiredToSelectSource { get; set; }

        public bool IsShutdownAllowed { get; set; }

        public bool IsRestartAllowed { get; set; }

        public bool IsChangingServerSettingsAllowed { get; set; }

        public bool IsConnectToDeskView { get; set; }

        public bool IsKeepPannelAfterPowerLoss { get; set; }

        public bool IsRememberLastUser { get; set; }

        public bool IsRememnerLastCalls { get; set; }        
    }
}
