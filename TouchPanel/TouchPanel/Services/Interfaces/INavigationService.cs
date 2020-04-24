using System;
using System.Collections.Generic;
using System.Text;
using TouchPanel.Models;
using TouchPanel.Models.Interfaces;
using Xamarin.Forms;

namespace TouchPanel.Services.Interfaces
{
    public interface INavigationService
    {
        IBaseNavigationPage NavigationPage { get; set; }

        void Push(NavigationView view);

        void Pop();
    }
}
