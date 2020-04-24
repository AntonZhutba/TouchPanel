using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouchPanel.Models;
using TouchPanel.Models.Enums;
using TouchPanel.Models.Settings;
using TouchPanel.Providers;
using TouchPanel.Services;
using Xamarin.Forms;

namespace TouchPanel.ViewModels.Settings
{
    public class DeskViewModel : NavigationViewModel
    {
        private DeskViewSettings _deskViewSettings;
        private readonly ConfigurationService _configurationService;
        private readonly SourceService _sourceService;
        private ConnectStatus _connectStatus;
        private string _connectButtonText;
        private bool _isConnected;
        private string _errorMessage;

        public DeskViewModel(ContentView view, string name) : base(name, view)
        {
            _configurationService = DependencyService.Resolve<ConfigurationService>();
            _sourceService = DependencyService.Resolve<SourceService>();

            _deskViewSettings = _configurationService.GetSharedConfiguration().DeskViewSettings;
            ConnectStatus = ConnectStatus.Idle;
            ConnectButtonText = "Connect";

            DeskViewSettings.CurrentBackgroundImage = "Background_Blue.jpg";
            BackgroundImages = new ObservableCollection<BackgroundImage>()
            {
                new BackgroundImage()
                {
                    Name="Background_1.jpg",
                },
                   new BackgroundImage()
                {
                    Name="Background_2.jpg",
                },
                      new BackgroundImage()
                {
                    Name="Background_Blue.jpg",
                }                
            };
            BackgroundImages.First(x => x.Name == DeskViewSettings.CurrentBackgroundImage).IsSelected = true;
        }

        public ObservableCollection<BackgroundImage> BackgroundImages {get; set; }

        public ConnectStatus ConnectStatus
        {
            get { return _connectStatus; }
            set { SetProperty(ref _connectStatus, value); }
        }

        public bool IsConnected
        {
            get { return _isConnected; }
            set { SetProperty(ref _isConnected, value); }
        }
        public string ConnectButtonText
        {
            get { return _connectButtonText; }
            set { SetProperty(ref _connectButtonText, value); }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        public DeskViewSettings DeskViewSettings
        {
            get { return _deskViewSettings; }
            set { SetProperty(ref _deskViewSettings, value); }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new Command(async (x) =>
                {
                    await _configurationService.UpdateSettings(DeskViewSettings);
                });
            }
        }

        public ICommand ConnectCommand
        {
            get
            {
                return new Command(() => ConnectToServer());             
            }
        }

        public ICommand SelectBackGroundCommand
        {
            get
            {
                return new Command((x) => SelectBackground((BackgroundImage)x));
            }
        }

        private void SelectBackground(BackgroundImage bi)
        {
            foreach (var image in BackgroundImages)
            {
                if(image.Name == bi.Name)
                {
                    image.IsSelected = true;
                    DeskViewSettings.CurrentBackgroundImage = image.Name;
                }
                else
                {
                    image.IsSelected = false;
                }
            }
        }

        private void ConnectToServer()
        {
            ConnectStatus = ConnectStatus.InProgress;
            ConnectButtonText = "Connecting";
            ErrorMessage = "";
            Task.Run(() =>
            {
                var result = _sourceService.GetSources(this.DeskViewSettings.DeskViewServer);
                if (result.IsSuccessed)
                {
                    ConnectStatus = ConnectStatus.Connected;
                    ConnectButtonText = "Connected";
                    IsConnected = true;
                }
                else
                {
                    ConnectStatus = ConnectStatus.Idle;
                    ConnectButtonText = "Connect";
                    ErrorMessage = result.MessageError;
                }
            });
           

        }
    }
}
