using Android.OS;
using System;
using System.Collections.Generic;
using System.Text;
using TouchPanel.Models.Enums;
using Xamarin.Forms;

namespace TouchPanel.ViewModels.PowerControl
{
    public class PowerStatusViewModel : BaseViewModel
    {
        public PowerStatusViewModel(PowerStatusType powerStatus)
        {
            Initialize(powerStatus);
        }

        public string StatusText { get; set; }

        public string SvgPath { get; set; }

        private void Initialize(PowerStatusType powerStatus)
        {
            switch (powerStatus)
            {
                case PowerStatusType.Sleep:
             
                    StatusText = "Entering sleep mode";
                    SvgPath = "TouchPanel.Icons.moon_stars_white.svg";
                    break;
                case PowerStatusType.Restart:
                    StatusText = "Restarting panel";
                    SvgPath = "TouchPanel.Icons.undo_white.svg";
                    break;
                case PowerStatusType.Shutdown:
                    StatusText = "Shutting down";
                    SvgPath = "TouchPanel.Icons.power_off_white.svg";
                    break;
            }
        }
    }
}