using System;
using System.Collections.Generic;
using System.Text;
using TouchPanel.Models.Settings;
using TouchPanel.Services.Interfaces;
using TouchPanel.ViewModels;
using TouchPanel.Views;
using Xamarin.Forms;

namespace TouchPanel.Helpers
{
    public static class SharedApplication
    {      

        public static BaseNavigationPage BaseNavigationPage { get; set; }
    }
}
