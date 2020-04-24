using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TouchPanel.Models
{
    public class NavigationView
    {
        public NavigationView(ContentView view)
        {
            ContentView = view;
            IsShowingBackButton = false;
        }

        public NavigationView(ContentView view, bool showBackButton)
        {
            ContentView = view;
            IsShowingBackButton = showBackButton;
        }

        public bool IsShowingBackButton { get; set; }

        public ContentView ContentView { get; set; }
    }
}
