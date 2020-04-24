using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TouchPanel.Models.Interfaces
{
    public interface INavigationViewModel
    {
        string Name { get; set; }

        bool IsSelected { get; set; }

        ContentView View { get; set; }
    }
}
