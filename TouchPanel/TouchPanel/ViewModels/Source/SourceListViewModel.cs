using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouchPanel.Models;
using TouchPanel.Providers;
using Xamarin.Forms;

namespace TouchPanel.ViewModels.Source
{
    public class SourceListViewModel : BaseViewModel
    {
        private bool _isShowingSidebar;
        private SourceService _sourceService;
        private Models.Source _currentSelectedSource;
        private bool _showSelectRoomModal;

        public SourceListViewModel()
        {
            _sourceService = DependencyService.Resolve<SourceService>();
            Sources = new ObservableCollection<Models.Source>();
            var sourceResults = _sourceService.GetSources();
            if (sourceResults.IsSuccessed)
            {
                foreach (var sr in sourceResults.Data.Sources)
                {
                    Sources.Add(sr);
                }
                Rooms = sourceResults.Data.Rooms;
            }
            
           
            
        }

        public ObservableCollection<Models.Source> Sources { get; set; }

        public List<Room> Rooms { get; set; }

        public Models.Source CurrentSelectedSource
        {
            get { return _currentSelectedSource; }
            set { SetProperty(ref _currentSelectedSource, value); }
        }

        public bool IsShowingSelectRoomModal
        {
            get { return _showSelectRoomModal; }
            set { SetProperty(ref _showSelectRoomModal, value); }
        }

        public bool IsShowingSidebar
        {
            get { return _isShowingSidebar; }
            set { SetProperty(ref _isShowingSidebar, value); }
        }

        public ICommand OpenContextMenuCommand
        {
            get
            {
                return new Command((x) => OpenContextMenu((Models.Source)x));
            }
        }

        public ICommand ToggleSideMenuCommand   
        {
            get
            {
                return new Command(() => IsShowingSidebar = !IsShowingSidebar);
            }
        }

        public ICommand CloseSelectRoomMenuCommand
        {
            get
            {
                return new Command(() => CloseSelectRoomMenu());
            }
        }

        public ICommand SelectRoomForSourceCommand
        {
            get
            {
                return new Command((x) => SelectRoomForSource((Room)x));
            }
        }

        private void SelectRoomForSource(Room room)
        {            
            if (CurrentSelectedSource != null)
            {
                CurrentSelectedSource.CurrentRoom = room;
                Task.Run(() => _sourceService.SwitchCurrent(room.Id, CurrentSelectedSource.Id));
            }
            CloseSelectRoomMenu();
        }

        private void CloseSelectRoomMenu()
        {
            IsShowingSelectRoomModal = false;
            CurrentSelectedSource = null;
        }

        private void OpenContextMenu(Models.Source room)
        {            
            IsShowingSelectRoomModal = true;
            CurrentSelectedSource = room;
        }
    }
}
