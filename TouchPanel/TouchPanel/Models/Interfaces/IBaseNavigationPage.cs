using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TouchPanel.Models.Interfaces
{
    public interface IBaseNavigationPage
    {
        void ChangeContentView(NavigationView view);
    }
}
