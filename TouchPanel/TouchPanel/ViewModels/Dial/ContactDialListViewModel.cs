using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouchPanel.Helpers;
using TouchPanel.Models;
using TouchPanel.ViewModels.Contacts;
using TouchPanel.Views.Contacts;
using Xamarin.Forms;

namespace TouchPanel.ViewModels.Dial
{
    public class ContactDialListViewModel : ContactsListViewModel
    {
        private bool _isShowingSidebar;

        public ContactDialListViewModel() : base(false)
        {
            _allContacts = _ciscoService.GetRecentCalls().Data;
            FilteredContacts = new ObservableCollection<Contact>();
            foreach (var contact in _allContacts)
            {
                FilteredContacts.Add(contact);
            }
        }

        public bool IsShowingSidebar
        {
            get { return _isShowingSidebar; }
            set { SetProperty(ref _isShowingSidebar, value); }
        }

       

        public ICommand ToggleSideMenuCommand
        {
            get
            {
                return new Command(() => ToggleSideMenu());
            }
        }

      

        public ICommand CallToSelectedContactCommand
        {
            get
            {
                return new Command(() => CallToSelectedContact());
            }
        }

        private void CallToSelectedContact()
        {
            var contact = FilteredContacts.FirstOrDefault(x => x.IsSelected);
            if (contact != null)
            {
                StartCall(contact);
            }
        }

        private void ToggleSideMenu()
        {
            IsShowingSidebar = !IsShowingSidebar;
        }
    }
}
