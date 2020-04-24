using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using TouchPanel.Models;
using Xamarin.Forms;

namespace TouchPanel.ViewModels.Source
{
    public class PresetListViewModel : BaseViewModel
    {
        private bool _isVisible;
        private bool _isEditModalVisible;
        private bool _isSavePresetButtonPressed;
        private Preset _selectedPreset;
        private string _newPresetName;

        public PresetListViewModel(bool isShowAdvancedButton)
        {
            Presets = new ObservableCollection<Preset>()
            {
                new Preset()
                {
                    Number=1,
                    Name = "Default"
                },
                new Preset()
                {
                    Number=2,
                    Name = "Preset"
                },
                new Preset()
                {
                    Number=3,
                    Name = "Custom preset"
                },
                  new Preset()
                {
                    Number=4,
                    Name = "Custom preset"
                },
                    new Preset()
                {
                    Number=5,
                    Name = "Custom preset"
                },
            };
            IsShowButtons = isShowAdvancedButton;
        }

        public bool IsShowButtons { get; set; }

        public ObservableCollection<Preset> Presets { get; set; }

        public bool IsEditModalVisible
        {
            get { return _isEditModalVisible; }
            set { SetProperty(ref _isEditModalVisible, value); }
        }

        public bool IsSavePresetButtonPressed
        {
            get { return _isSavePresetButtonPressed; }
            set { SetProperty(ref _isSavePresetButtonPressed, value); }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value); }
        }

        public Preset SelectedPreset
        {
            get { return _selectedPreset; }
            set { SetProperty(ref _selectedPreset, value); }
        }

        public string NewPresetName
        {
            get { return _newPresetName; }
            set { SetProperty(ref _newPresetName, value); }
        }

        public ICommand ToggleSideMenuCommand
        {
            get
            {
                return new Command(() => ToggleSideMenu());
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new Command(() => StopEditing());
            }
        }

        public ICommand SaveCurrentAsPresetCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsSavePresetButtonPressed = true;
                });
            }
        }

        public ICommand SelectPresetCommand
        {
            get
            {
                return new Command((x) => SelectPreset((Preset)x));
            }
        }

        public ICommand SavePresetCommand
        {
            get
            {
                return new Command(() => SavePreset());
            }
        }

        public void ToggleSideMenu()
        {
            IsVisible = !IsVisible;
        }

        private void SelectPreset(Preset preset)
        {
            if (IsSavePresetButtonPressed)
            {
                SelectedPreset = preset;
                IsEditModalVisible = true;
            }
        }

        private void StopEditing()
        {
            IsEditModalVisible = false;
            IsSavePresetButtonPressed = false;
            SelectedPreset = null;
            NewPresetName = "";
        }

        private void SavePreset()
        {
            SelectedPreset.Name = NewPresetName;
            StopEditing();
        }
    }
}
