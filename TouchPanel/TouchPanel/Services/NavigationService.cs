using Android.App;
using System;
using System.Collections.Generic;
using System.Text;
using TouchPanel.Helpers;
using TouchPanel.Models;
using TouchPanel.Models.Interfaces;
using TouchPanel.Services.Interfaces;
using TouchPanel.ViewModels.Contacts;
using TouchPanel.Views;
using TouchPanel.Views.Contacts;
using TouchPanel.Views.Dial;
using Xamarin.Forms;

namespace TouchPanel.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Stack<NavigationView> _navigationStack;

        public NavigationService()
        {            
            _navigationStack = new Stack<NavigationView>();
            NavigationPage = DependencyService.Get<BaseNavigationPage>();
            Push(new NavigationView(new MainPage()));
        }

        public IBaseNavigationPage NavigationPage { get; set; }

        public void Push(NavigationView view)
        {
            _navigationStack.Push(view);
            NavigationPage.ChangeContentView(view);
        }

        public void Pop()
        {
            if (_navigationStack.Count > 1)
            {
                var lastPage =_navigationStack.Pop();
                if (lastPage.ContentView is DialingPage)
                {
                    ((DialingViewModel)lastPage.ContentView.BindingContext).Disconnect();
                }

                if (lastPage.ContentView is ContactsListPage || lastPage.ContentView is ContactDialListPage)
                {
                    var mp = new NavigationView(new MainPage());
                    _navigationStack.Push(mp);
                    NavigationPage.ChangeContentView(mp);
                }
                else if (lastPage.ContentView is MainPage)
                {
                    var activity = (Activity)Forms.Context;
                    activity.FinishAffinity();
                }
                else
                {
                    NavigationPage.ChangeContentView(_navigationStack.Peek());
                }
            }
            else
            {
                var activity = (Activity)Forms.Context;
                activity.FinishAffinity();
            }
        }
    }
}
