using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouchPanel.Models;
using TouchPanel.Models.Interfaces;
using TouchPanel.Services;
using TouchPanel.Services.Interfaces;
using TouchPanel.Views;
using TouchPanel.Views.Settings;
using Xamarin.Forms;

namespace TouchPanel.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        private INavigationViewModel _currentPage;
        private string _nextButtonText = "Next";
        private bool _isPreviousEnabled;
        private ConfigurationService _configurationService;

        public SettingsViewModel()
        {
            _configurationService = DependencyService.Resolve<ConfigurationService>();
            ViewModels = new List<INavigationViewModel>()
            {
                //(INavigationViewModel)new InterfacePage().BindingContext,
                (INavigationViewModel)new DeskviewPage().BindingContext,
                (INavigationViewModel)new NetworkPage().BindingContext,
         
                (INavigationViewModel)new ServerPage().BindingContext,

            };
            CurrentPage = ViewModels[0];
            ViewModels[0].IsSelected = true;
        }

        public INavigationViewModel CurrentPage
        {
            get { return _currentPage; }
            set { SetProperty(ref _currentPage, value); }
        }

        public string NextButtonText
        {
            get { return _nextButtonText; }
            set { SetProperty(ref _nextButtonText, value); }
        }

        public bool IsPreviousEnabled
        {
            get { return _isPreviousEnabled; }
            set { SetProperty(ref _isPreviousEnabled, value); }
        }

        public List<INavigationViewModel> ViewModels { get; set; }

        public ICommand ChangeCurrentPageCommand
        {
            get
            {
                return new Command(x => ChangeCurrentPage((string)x));
            }
        }

        public ICommand GoNextCommand
        {
            get
            {
                return new Command(async () => await GoToNextTab());
            }
        }

        public ICommand GoPreviousCommand
        {
            get
            {
                return new Command(() => GoToPreviousTab());
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new Command(() => DependencyService.Get<INavigationService>().Pop());
            }
        }

        public async Task GoToNextTab()
        {
            ViewModels.ForEach(x => x.IsSelected = false);
            var vmIndex = ViewModels.Select((v, i) => new { vm = v, index = i }).First(x => x.vm.Name == CurrentPage.Name).index;
            if (vmIndex < ViewModels.Count - 1)
            {
                // Go next
                CurrentPage = ViewModels[vmIndex + 1];
                CurrentPage.IsSelected = true;
                if (vmIndex + 1 == ViewModels.Count - 1)
                {
                    NextButtonText = "Finish";
                }
                IsPreviousEnabled = true;
            }
            else
            {
                // Finish setup
                await _configurationService.UpdateSettings(
                    ((ServerViewModel)ViewModels.First(x => x.Name == "Video Conference").View.BindingContext).ServerSettings,
                    ((NetworkViewModel)ViewModels.First(x=>x.Name == "Network").View.BindingContext).NetworkSettings,
                    ((DeskViewModel)ViewModels.First(x => x.Name == "DESKVIEW™").View.BindingContext).DeskViewSettings);
                DependencyService.Get<INavigationService>().Push(new NavigationView(new MainPage()));
            }
        }

        public void GoToPreviousTab()
        {
            ViewModels.ForEach(x => x.IsSelected = false);
            var vmIndex = ViewModels.Select((v, i) => new { vm = v, index = i }).First(x => x.vm.Name == CurrentPage.Name).index;
            if (vmIndex > 0)
            {
                CurrentPage = ViewModels[vmIndex - 1];
                CurrentPage.IsSelected = true;
                if (vmIndex - 1 == 0)
                {
                    IsPreviousEnabled = false;
                }
                NextButtonText = "Next";
            }
        }

        private void ChangeCurrentPage(string pageName)
        {
            ViewModels.ForEach(x => x.IsSelected = false);
            var cp = ViewModels.Select((v, i) => new { vm = v, index = i }).First(x => x.vm.Name == pageName);
            if(cp.index != 0)
            {
                IsPreviousEnabled = true;
            }
            else
            {
                IsPreviousEnabled = false;
            }

            if (cp.index + 1 == ViewModels.Count)
            {
                NextButtonText = "Finish";
            }
            else
            {
                NextButtonText = "Next";
            }
            CurrentPage = ViewModels.First(x => x.Name == pageName);
            CurrentPage.IsSelected = true;

        }
    }
}
