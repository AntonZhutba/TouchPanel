using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TouchPanel.Models;
using TouchPanel.Models.Provider;
using TouchPanel.Services;
using Xamarin.Forms;

namespace TouchPanel.Providers
{
    public class SourceResponseData
    {
        public Dictionary<string, Source> Receivers { get; set; }

        public Dictionary<string, Room> Transmitters { get; set; }

        [JsonProperty("error_message")]
        public string Status { get; set; }
    }

    public class SourceService
    {
        private string _baseUri = "https://demoapi.deskview.no:443";

        public SourceService()
        {
            var server = DependencyService.Resolve<ConfigurationService>().GetSharedConfiguration().DeskViewSettings.DeskViewServer;
            _baseUri = "https://" + (!String.IsNullOrEmpty(server) ? server : "demoapi.deskview.no:443");
        }

        public ServiceResultWithModel<SourceResultModel> GetSources(string baseUri = null)
        {
            try
            {
                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(string.IsNullOrEmpty(baseUri) ? _baseUri : "https://" + baseUri)
                };

                var response = httpClient.GetAsync("/?module=touchpad&id=123&myip=192.168.1.4&fw=panelsoftwareversion&hw=hwversion&action=getlist").Result;
                var data = JsonConvert.DeserializeObject<SourceResponseData>(response.Content.ReadAsStringAsync().Result);
                if (data.Status != "ok")
                {
                    return new ServiceResultWithModel<SourceResultModel>(false);
                }

                var rooms = data.Transmitters.Select(x => x.Value).ToList();

                var sources = data.Receivers.Select(x => x.Value).ToList();
                // fill row,column
                int row = 0;
                int col = 0;
                foreach (var source in sources)
                {
                    source.Row = row;
                    source.Column = col;
                    if (col == 3)
                    {
                        col = 0;
                        row++;
                    }
                    else
                    {
                        col++;
                    }
                }

                foreach (var source in sources)
                {
                    var current = source.CurrentRoomId.HasValue ? rooms.FirstOrDefault(x => x.Id == source.CurrentRoomId) : null;
                }

                return new ServiceResultWithModel<SourceResultModel>()
                {
                    Data = new SourceResultModel()
                    {
                        Rooms = rooms,
                        Sources = sources
                    }
                };
            } 
            catch(Exception e)
            {
                return new ServiceResultWithModel<SourceResultModel>(false)
                {
                    MessageError = "Cannot connect to the server"
                };
            }
        }

        public async Task<ServiceResult> SwitchCurrent(int roomId, int sourceId)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_baseUri)
            };

            var response = await httpClient.
                GetAsync($"/?module=touchpad&id=123&myip=192.168.1.4&fw=panelsoftwareversion&hw=hwversion&action=switch&rxid={roomId}&txid={sourceId}");

            return new ServiceResult(response.IsSuccessStatusCode);
        }
    }
}
