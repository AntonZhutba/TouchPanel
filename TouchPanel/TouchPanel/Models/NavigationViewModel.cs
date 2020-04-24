using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TouchPanel.Helpers;
using TouchPanel.Models.Interfaces;
using TouchPanel.Services.Interfaces;
using TouchPanel.ViewModels;
using Xamarin.Forms;

namespace TouchPanel.Models
{
    public class NavigationViewModel : BaseViewModel, INavigationViewModel
    {
        private bool _isSelected;

        public NavigationViewModel(string name, ContentView view)
        {
            Name = name;
            View = view;
        }
        public string Name { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        public ContentView View { get; set ; }

        public ICommand CancelCommand
        {
            get
            {
                return new Command(() => DependencyService.Get<INavigationService>().Pop());
            }
        }
    }
}
