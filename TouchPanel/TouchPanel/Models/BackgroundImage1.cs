using System;
using System.Collections.Generic;
using System.Text;

namespace TouchPanel.Models
{
    public class BackgroundImage : BaseNotifyPropertyChanged
    {
        private bool _isSelected;

        public string Name { get; set; }

        public bool IsSelected
    {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
    }
}
