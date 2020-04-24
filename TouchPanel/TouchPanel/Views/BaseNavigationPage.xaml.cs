using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchPanel.Helpers;
using TouchPanel.Models;
using TouchPanel.Models.Interfaces;
using TouchPanel.Services;
using TouchPanel.Services.Interfaces;
using TouchPanel.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouchPanel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseNavigationPage : ContentPage, IBaseNavigationPage
    {
        public BaseNavigationPage()
        {
            InitializeComponent();
            SharedApplication.BaseNavigationPage = this;
            BindingContext = DependencyService.Get<ISharedConfigurationViewModel>();
           
        }

        protected override bool OnBackButtonPressed()
        {
            DependencyService.Get<INavigationService>().Pop();
            return true;
        }

        public void ChangeContentView(NavigationView nv)
        {
            contentView.Content = nv.ContentView;
            backButton.IsVisible = nv.IsShowingBackButton;
        }
    }
}