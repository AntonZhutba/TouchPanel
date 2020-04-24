using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouchPanel.Helpers;
using TouchPanel.Models;
using TouchPanel.Services.Interfaces;
using TouchPanel.Views.Source;
using Xamarin.Forms;

namespace TouchPanel.ViewModels.Source
{
    public class PinEntryViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        public PinEntryViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>();
            PinNumbers = new ObservableCollection<string>()
            {
                null, null, null, null
            };
        }

        public ObservableCollection<string> PinNumbers { get; set; }

        public ICommand AddNumberToPinCommand
        {
            get
            {
                return new Command(x => AddNumberToPin((string)x));
            }
        }
        public ICommand BackCommand
        {
            get
            {
                return new Command(x => Back());
            }
        }
        public ICommand ClearCommand
        {
            get
            {
                return new Command(x => Clear());
            }
        }

        private void AddNumberToPin(string pinNum)
        {

            var pinNumEl = PinNumbers.Select((number, index) => new { number, index }).FirstOrDefault(x => x.number == null);
            if (pinNumEl != null)
            {
                PinNumbers[pinNumEl.index] = pinNum;
                if(pinNumEl.index == 3)
                {
                    _navigationService.Push(new NavigationView(new SourceListPage(), true));
                }
            }
            else
            {
                // check pin
                _navigationService.Push(new NavigationView(new SourceListPage(), true));
            }
        }

        private void Clear()
        {
            for (int i = 0; i < PinNumbers.Count; i++)
            {
                PinNumbers[i] = null;
            }
        }

        private void Back()
        {
            _navigationService.Pop();
        }
    }
}
