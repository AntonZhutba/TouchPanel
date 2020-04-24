using System;
using System.Collections.Generic;
using System.Text;

namespace TouchPanel.Models
{
    public class Preset : BaseNotifyPropertyChanged
    {
        private string _name;
        public int Number { get; set; }


        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
    }
}
