using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TouchPanel.ViewModels;

namespace TouchPanel.Models
{
    public class Source : BaseNotifyPropertyChanged
    {
        private Room _currentRoom;

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("label")]
        public string Name { get; set; }

        [JsonProperty("screnshoot")]
        public string ScreenshootUrl { get; set; }

        [JsonProperty("current")]
        public int? CurrentRoomId { get; set; }

        public Room CurrentRoom
        {
            get { return _currentRoom; }
            set { SetProperty(ref _currentRoom, value); }
        }

    

        public int Row { get; set; }

        public int Column { get; set; }
    }
}
