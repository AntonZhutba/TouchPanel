using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TouchPanel.Services;
using TouchPanel.Views;
using TouchPanel.ViewModels;
using TouchPanel.Providers;
using TouchPanel.Services.Interfaces;

namespace TouchPanel
{
    public partial class App : Application
    {

        public App()
        {
  

            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<BaseNavigationViewModel>();
            DependencyService.Register<CiscoService>();
            DependencyService.Register<ConfigurationService>();
            DependencyService.Register<SourceService>();

            //navigation
            DependencyService.Register<BaseNavigationPage>();
            DependencyService.Register<NavigationService>();

            DependencyService.Resolve<ConfigurationService>();
            // MainPage = new NavigationPage(new MainPage());

            MainPage = DependencyService.Get<BaseNavigationPage>();
            DependencyService.Get<INavigationService>();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            DependencyService.Get<CiscoService>().EndSession();
        }

        protected override void OnResume()
        {
        }
    }
}
