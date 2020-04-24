using System;
using System.Collections.Generic;
using System.Text;
using TouchPanel.Models.Enums;

namespace TouchPanel.Models
{
    public class Contact : BaseNotifyPropertyChanged
    {
        private bool _isSelected;
        private bool _isMenuClicked;

        public string Id { get; set; }

        // Contact method stores address
        public string ContactMethodId { get; set; }

        public string Address { get; set; }

        public string Company { get; set; }

        public string ContactName { get; set; }

        public ContactType Type { get; set; }

        public string RoomName { get; set; }

        public string ContactFriendlyName { get; set; }

        public string Preset { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
        public bool IsMenuClicked
        {
            get { return _isMenuClicked; }
            set { SetProperty(ref _isMenuClicked, value); }
        }
    }
}
