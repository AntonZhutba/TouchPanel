using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouchPanel.Helpers;
using TouchPanel.Models;
using TouchPanel.Models.Enums;
using TouchPanel.Models.Interfaces;
using TouchPanel.Services.Interfaces;
using TouchPanel.Views;
using TouchPanel.Views.Contacts;
using TouchPanel.Views.Dial;
using TouchPanel.Views.Power;
using TouchPanel.Views.Settings;
using TouchPanel.Views.Source;
using Xamarin.Forms;

namespace TouchPanel.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ContentView _powerControlPartial;
        private readonly ContentView _timePartial;
        private ContentView _currentPartial;


        public MainViewModel() 
        {
            _powerControlPartial = new PowerControlView();
            _timePartial = new CurrentDateView();
            CurrentPartial = new CurrentDateView();
        }


        public MainViewModel(INavigation navigation) : this()
        {
            //ApplicationNavigation.Navigation = navigation;          

            CurrentPartial = new CurrentDateView();
        }

        public ICommand OpenPageCommand
        {
            get
            {
                return new Command(x => OpenPage((MainPageType)x));
            }
        }
        
        public ContentView CurrentPartial
        {
            get { return _currentPartial; }
            set { SetProperty(ref _currentPartial, value); }
        }

        private void OpenPage(MainPageType pageType)
        {
            switch (pageType)
            {
                case MainPageType.Dial:
                    DependencyService.Resolve<INavigationService>().Push(new NavigationView(new ContactDialListPage(), true));
                    break;
                case MainPageType.Settings:
                    DependencyService.Resolve<INavigationService>().Push(new NavigationView(new SettingsPage()));
                    break;
                case MainPageType.Contacts:
                    DependencyService.Resolve<INavigationService>().Push(new NavigationView(new ContactsListPage(), true));
                    break;
                case MainPageType.Source:
                    DependencyService.Resolve<INavigationService>().Push(new NavigationView(new PinEntryPage(), true));
                    break;
                case MainPageType.Power:
                    CurrentPartial = CurrentPartial == _timePartial ? _powerControlPartial : _timePartial;                   
                    break;
            }
        }      
    }
}
