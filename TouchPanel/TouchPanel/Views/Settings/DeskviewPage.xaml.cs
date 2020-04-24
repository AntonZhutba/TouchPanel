﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchPanel.Models.Interfaces;
using TouchPanel.ViewModels.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouchPanel.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeskviewPage : ContentView
    {
        public DeskviewPage()
        {
            InitializeComponent();
            BindingContext = new DeskViewModel(this, Name);
        }

        public string Name { get => "DESKVIEW™"; }
    }
}