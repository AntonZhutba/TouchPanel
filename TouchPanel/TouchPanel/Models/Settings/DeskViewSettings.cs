using System;
using System.Collections.Generic;
using System.Text;
using TouchPanel.Models.Enums;

namespace TouchPanel.Models.Settings
{
    public class DeskViewSettings
    {
        public DeskViewSettings() { }

        public DeskViewSettings(DeskViewSettings settings)
        {
            DeskViewServer = settings.DeskViewServer;            
            Location = settings.Location;
            PanelName = settings.PanelName;
            CurrentBackgroundImage = settings.CurrentBackgroundImage;
        }

        public string DeskViewServer { get; set; }

        public string Location { get; set; }

        public string PanelName { get; set; }

        public string CurrentBackgroundImage { get; set; }


    }
}
