using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouchPanel.Models;
using TouchPanel.Models.Enums;
using TouchPanel.Services;
using TouchPanel.Services.Interfaces;
using TouchPanel.Views.Contacts;
using Xamarin.Forms;

namespace TouchPanel.ViewModels.Contacts
{
    public class ContactDetailsViewModel : BaseViewModel
    {
        private Contact _contact;
        private readonly ContactAction _actionType;
        private readonly CiscoService _ciscoService;

        public ContactDetailsViewModel()
        {
            Contact = new Contact();
            _ciscoService = DependencyService.Get<CiscoService>();
            _actionType = ContactAction.Add;
        }

        public ContactDetailsViewModel(Contact contact)
        {
            Contact = contact;
            _ciscoService = DependencyService.Get<CiscoService>();
            _actionType = ContactAction.Update;
        }

        public string HeaderText { get => _actionType == ContactAction.Add ? "Add new contact" : "Edit contact"; }

        public List<string> UserPresets { get => new List<string>() { "PRESET1", "PRESET2" }; }

        public ICommand SubmitCommand
        {
            get
            {
                return new Command(() => Submit());
            }
        }

        public Contact Contact
        {
            get { return _contact; }
            set { SetProperty(ref _contact, value); }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new Command(() => DependencyService.Get<INavigationService>().Pop());
            }
        }

        private void Submit()
        {
            if(_actionType == ContactAction.Add)
            {
                _ciscoService.CreateContact(Contact);
            }
            else
            {
                _ciscoService.EditContact(Contact);
            }
            DependencyService.Resolve<INavigationService>().Push(new NavigationView(new ContactsListPage(), true));

        }
    }
}
