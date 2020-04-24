using System;
using System.Collections.Generic;
using System.Text;

namespace TouchPanel.Models.Interfaces
{
    public interface ITabbedNavigationLayounViewModel
    {
        void GoToNextTab();
        void GoToPreviousTab();
        void GoBack();
    }
}
