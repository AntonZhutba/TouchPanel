using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TouchPanel.Models
{
    public class Room
    {
        public int Id { get; set; }

        [JsonProperty("label")]
        public string Name { get; set; }

        public string Location { get; set; }

        public string Address { get; set; }
    }
}
