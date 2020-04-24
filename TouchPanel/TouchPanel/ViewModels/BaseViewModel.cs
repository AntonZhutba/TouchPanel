using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using TouchPanel.Models;
using TouchPanel.Services;
using TouchPanel.Models.Settings;

namespace TouchPanel.ViewModels
{
    public class BaseViewModel : BaseNotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        

        public BaseViewModel()
        {
           
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

   
        

    }
}
