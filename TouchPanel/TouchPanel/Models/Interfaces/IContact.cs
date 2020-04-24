using System;
using System.Collections.Generic;
using System.Text;

namespace TouchPanel.Models.Interfaces
{
    public interface IContact
    {
       string VideoConferenceAddress { get; set; }

       string Company { get; set; }

       string ContactName { get; set; }

       string RoomName { get; set; }

       string ContactFriendlyName { get; set; }       
    }
}
