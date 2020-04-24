using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TouchPanel.Models.Provider
{
    class Entry
    {
        public string RemoteNumber { get; set; }

        public string DisplayName { get; set; }
    }
    class ApiRecentCallsResultList
    {
        [JsonProperty("Entry")]
        public List<Entry> Entries { get; set; }
    }
    class ApiRecentCallsResult
    {
        public ApiRecentCallsResultList CallHistoryRecentsResult { get; set; }
    }
}
