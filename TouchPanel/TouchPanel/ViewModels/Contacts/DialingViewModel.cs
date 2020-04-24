using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouchPanel.Helpers;
using TouchPanel.Models;
using TouchPanel.Services;
using TouchPanel.Services.Interfaces;
using Xamarin.Forms;

namespace TouchPanel.ViewModels.Contacts
{
    public class DialingViewModel : BaseViewModel
    {
        private int _volume;
        private string _currentStatus;
        private CiscoService _ciscoService;
        private bool _isConnected;
        private bool _isMuted;
        private string _callId;
        private string _presentationButtonText;
        private string _muteButtonText;
        private bool _isPresentationStarted;
        private bool _isShowConfirmEndCallModal;

        public DialingViewModel(Contact contact)
        {
            CurrentStatus = "Calling";
            Contact = contact;
            _ciscoService = DependencyService.Get<CiscoService>();
            var result = _ciscoService.StartCall(contact.Address);
            PresentationButtonText = "Start presentation";
            MuteButtonText = "Mute microphhone";
            if (result.IsSuccessed)
            {
                CurrentStatus = "In call";
                _callId = result.Data;
                IsConnected = true;
            }
            else
            {
                CurrentStatus = "Call failed";
                IsConnected = false;

            }
        }

        public int Volume
        {
            get { return _volume; }
            set { SetProperty(ref _volume, value); }
        }

        public bool IsConnected
        {
            get { return _isConnected; }
            set { SetProperty(ref _isConnected, value); }
        }

        public bool IsMuted
        {
            get { return _isMuted; }
            set { SetProperty(ref _isMuted, value); }
        }

        public bool IsPresentationStarted
        {
            get { return _isPresentationStarted; }
            set { SetProperty(ref _isPresentationStarted, value); }
        }

        public bool IsShowConfirmEndCallModal
        {
            get { return _isShowConfirmEndCallModal; }
            set { SetProperty(ref _isShowConfirmEndCallModal, value); }
        }
        

        public string CurrentStatus
        {
            get { return _currentStatus; }
            set { SetProperty(ref _currentStatus, value); }
        }

        public string PresentationButtonText
        {
            get { return _presentationButtonText; }
            set { SetProperty(ref _presentationButtonText, value); }
        }
        public string MuteButtonText
        {
            get { return _muteButtonText; }
            set { SetProperty(ref _muteButtonText, value); }
        }

        public Contact Contact { get; set; }

        public ICommand EndCallCommand
        {
            get
            {
                return new Command(() => EndCall());
            }
        }

        public ICommand MuteCommand
        {
            get
            {
                return new Command(() => ToggleMuteMode());
            }
        }

        public ICommand TogglePresentationModeCommand
        {
            get
            {
                return new Command(() => TogglePresentationMode());
            }
        }

        public ICommand ToggleConfirmEndCallModalCommand
        {
            get
            {
                return new Command(() => ToggleConfirmEndCallModal());
            }
        }



        public void Disconnect()
        {
            if (!String.IsNullOrEmpty(_callId))
            {
                _ciscoService.DisconnectCall(_callId);
            }
        }

        private void ToggleConfirmEndCallModal()
        {
            if (!_isConnected)
            {
                EndCall();
            }
            else
            {
                IsShowConfirmEndCallModal = !IsShowConfirmEndCallModal;
            }
        }

        public void ChangeVolume()
        {
            Task.Run(() => _ciscoService.UpdateVolume(Volume));
        }

        private void TogglePresentationMode()
        {          
            IsPresentationStarted = !IsPresentationStarted;
            PresentationButtonText = IsPresentationStarted ? "Stop presentation" : "Start presentation";

        }

        private void ToggleMuteMode()
        {            
            IsMuted = !IsMuted;
            MuteButtonText = IsMuted ? "Unmute microphone" : "Mute microphone";                
            //api call            
        }

        private void EndCall()
        {
            Disconnect();
            DependencyService.Get<INavigationService>().Pop();
        }




    }
}
