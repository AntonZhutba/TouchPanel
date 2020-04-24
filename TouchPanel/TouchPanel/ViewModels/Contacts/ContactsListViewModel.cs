using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouchPanel.Helpers;
using TouchPanel.Models;
using TouchPanel.Services;
using TouchPanel.Services.Interfaces;
using TouchPanel.Views.Contacts;
using Xamarin.Forms;

namespace TouchPanel.ViewModels.Contacts
{
    public class ContactsListViewModel : BaseViewModel
    {
        protected string _searchContactText;
        protected List<Contact> _allContacts;
        protected readonly CiscoService _ciscoService;
        protected readonly INavigationService _navigationService;


        public ContactsListViewModel(bool isLoadAllContacts = true)
        {
            _ciscoService = DependencyService.Resolve<CiscoService>();
            _navigationService = DependencyService.Get<INavigationService>();
   

            if (isLoadAllContacts)
            {
                _allContacts = _ciscoService.GetContactList().Data;
                FilteredContacts = new ObservableCollection<Contact>();
                foreach (var contact in _allContacts)
                {
                    FilteredContacts.Add(contact);
                }
            }
        }

        public string SearchContactText
        {
            get { return _searchContactText; }
            set
            {
                SetProperty(ref _searchContactText, value);
                FilterContacts();
            }
        }

        public ObservableCollection<Contact> FilteredContacts { get; set; }

        public ICommand OpenCreateUserPageCommand
        {
            get
            {
                return new Command(() => OpenCreateUserPage());
            }
        }

        public ICommand OpenEditUserPageCommand
        {
            get
            {
                return new Command((x) => OpenEditUserPage((Contact)x));
            }
        }

        public ICommand StartCallCommand
        {
            get
            {
                return new Command((x) => StartCall((Contact)x));
            }
        }

        public ICommand DeleteUserCommand
        {
            get
            {
                return new Command((x) => DeleteUser((Contact)x));
            }
        }

        public ICommand SelectContactCommand
        {
            get
            {
                return new Command((x) => SelectContact((Contact)x));
            }
        }

        public ICommand AdvancedButtonClickCommand
        {
            get
            {
                return new Command((x) =>
                {
                    CleatState();
                    ((Contact)x).IsSelected = true;
                    ((Contact)x).IsMenuClicked = true;
                });
            }
        }

        protected void FilterContacts()
        {
            FilteredContacts.Clear();
            var filteredContact = _allContacts.Where(x =>
                x.ContactName.ToLower()
                .Contains(!String.IsNullOrEmpty(SearchContactText) ? SearchContactText.ToLower() : "")).ToList();
            foreach (var contact in filteredContact)
            {
                FilteredContacts.Add(contact);
            }
        }

        protected void SelectContact(Contact contact)
        {
            CleatState();
            contact.IsSelected = true;
        }

        protected void DeleteUser(Contact contact)
        {
            _allContacts.Remove(contact);
            FilterContacts();
            _ciscoService.DeleteContact(contact.Id);
        }

        protected void CleatState()
        {
            foreach (var c in FilteredContacts)
            {
                c.IsSelected = false;
                c.IsMenuClicked = false;
            }
        }

        protected void OpenCreateUserPage()
        {
            CleatState();
            _navigationService.Push(new NavigationView(new ContactDetailsPage()));
        }

        protected void OpenEditUserPage(Contact contact)
        {
            CleatState();
            _navigationService.Push(new NavigationView(new ContactDetailsPage(contact)));
        }

        protected void StartCall(Contact contact)
        {
            CleatState();
            _navigationService.Push(new NavigationView(new DialingPage(contact)));
        }


    }
}
