using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TouchPanel.Models;
using TouchPanel.Models.Enums;
using TouchPanel.Models.Provider;
using TouchPanel.Models.Settings;
using Xamarin.Forms;

namespace TouchPanel.Services
{
    public class CiscoService
    {
        private readonly ApplicationConfiguration _configuration;
        private readonly string _baseDomain;
        private Cookie _sessionCookie;
        private const string VirtualRoomsFolderId = "Bportal_rooms";

        public CiscoService()
        {
            _configuration = DependencyService.Resolve<ConfigurationService>().GetSharedConfiguration();
            _baseDomain = $"http://{_configuration.ServerSettings.IPAddress}:{_configuration.ServerSettings.CiscoPort}";
        }

        public ServiceResultWithModel<List<Contact>> GetContactList()
        {
            try
            {
                var xmlContacts = $"<Command><Phonebook><Search command=\"True\">" +
                    "<ContactType>Any</ContactType>" +
                    "<Limit>100</Limit>" +
                    "<PhonebookType>Local</PhonebookType>" +
                    "</Search></Phonebook></Command>";
                var contacts = new List<Contact>();
                var response = PostXmlRequest(_baseDomain + "/putxml", xmlContacts);
                if (response != null)
                {
                    contacts.AddRange(DeserializeContactListXml(response, ContactType.Contact));
                }

                var xmlRoomContacts = $"<Command><Phonebook><Search command=\"True\">" +
                    "<ContactType>Any</ContactType>" +
                    "<Limit>100</Limit>" +
                    $"<FolderId>{VirtualRoomsFolderId}</FolderId>" +
                    "<PhonebookType>Corporate</PhonebookType>" +
                    "</Search></Phonebook></Command>";
                response = PostXmlRequest(_baseDomain + "/putxml", xmlRoomContacts);
                if (response != null)
                {
                    contacts.AddRange(DeserializeContactListXml(response, ContactType.Room));
                }
                return new ServiceResultWithModel<List<Contact>>()
                {
                    Data = new List<Contact>()
                    {
                        new Contact()
                        {
                            ContactName="asdasd",
                        },
                          new Contact()
                        {
                            ContactName="asdasd1",
                        },
                            new Contact()
                        {
                            ContactName="asdasd2",
                        },
                    },
                    IsSuccessed = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResultWithModel<List<Contact>>()
                {
                    IsSuccessed = false,
                    MessageError = e.Message,
                    Data = new List<Contact>()
                    {
                        new Contact()
                        {
                            ContactName="asdasd",
                        },
                          new Contact()
                        {
                            ContactName="asdasd1",
                        },
                            new Contact()
                        {
                            ContactName="asdasd2",
                        },
                    },
                };
            }

        }

        public ServiceResultWithModel<List<Contact>> GetRecentCalls()
        {
            try
            {
                var xmlContacts =
                    $"<Command><CallHistory><Recents command=\"True\">" +
                    $"<DetailLevel>Full</DetailLevel>" +
                    "</Recents></CallHistory></Command>";
                var contacts = new List<Contact>();
                var response = PostXmlRequest(_baseDomain + "/putxml", xmlContacts);
                var doc = new XmlDocument();
                doc.LoadXml(response);
                var json = JsonConvert.SerializeXmlNode(doc.SelectSingleNode("Command/CallHistoryRecentsResult"));
                if (response != null)
                {
                    var apiResult = JsonConvert.DeserializeObject<ApiRecentCallsResult>(json);
                    if (apiResult.CallHistoryRecentsResult.Entries != null && apiResult.CallHistoryRecentsResult.Entries.Any())
                    {
                        contacts = apiResult.CallHistoryRecentsResult.Entries.Select(x => new Contact()
                        {
                            ContactName = x.DisplayName,
                            Address = x.RemoteNumber,
                            Type = ContactType.Room,
                        }).ToList();
                    }

                }

                return new ServiceResultWithModel<List<Contact>>()
                {
                    Data = contacts,
                    IsSuccessed = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResultWithModel<List<Contact>>()
                {
                    IsSuccessed = false,
                    MessageError = e.Message,
                    Data = new List<Contact>()
                };
            }

        }

        public ServiceResult EditContact(Contact contact)
        {
            // change name
            var xmlUpdateName = "<Command><Phonebook><Contact>" +
               $"<Modify command=\"True\">" +
               $"<ContactId>{contact.Id}</ContactId>" +
               $"<Name>{contact.ContactName}</Name>" +
               $"</Modify>" +
             "</Contact></Phonebook></Command>";
            var response = PostXmlRequest(_baseDomain + "/putxml", xmlUpdateName);
            if (!response.Contains("\"OK\""))
            {
                return new ServiceResultWithModel<Contact>(false)
                {
                    MessageError = "Error occured while updating contact's name"
                };
            }

            // change number
            var xmlUpdateNumber = "<Command><Phonebook><ContactMethod>" +
             $"<Modify command=\"True\">" +
             $"<ContactId>{contact.Id}</ContactId>" +
             $"<ContactMethodId>{contact.ContactMethodId}</ContactMethodId>" +
             $"<Number>{contact.Address}</Number>" +
             $"</Modify>" +
           "</ContactMethod></Phonebook></Command>";
            response = PostXmlRequest(_baseDomain + "/putxml", xmlUpdateNumber);
            if (!response.Contains("\"OK\""))
            {
                return new ServiceResultWithModel<Contact>(false)
                {
                    MessageError = "Error occured while updating contact's address"
                };
            }

            return new ServiceResult(true);
        }

        public ServiceResult DeleteContact(string id)
        {

            var xml = "<Command><Phonebook><Contact>" +
                $"<Delete command=\"True\">" +
                $"<ContactId>{id}</ContactId>" +
                $"</Delete>" +
              "</Contact></Phonebook></Command>";
            var response = PostXmlRequest(_baseDomain + "/putxml", xml);
            if (response == null)
            {
                return new ServiceResult(false)
                {
                    MessageError = "Error occured while posting xml request"
                };
            }
            return new ServiceResult(true);
        }

        public ServiceResultWithModel<Contact> CreateContact(Contact contact)
        {
            var xml = "<Command><Phonebook><Contact>" +
             $"<Add command=\"True\">" +
             $"<Name>{contact.ContactName}</Name>" +
             $"<Number>{contact.Address}</Number>" +
             $"<Protocol>SIP</Protocol>" +
             $"</Add>" +
           "</Contact></Phonebook></Command>";
            var response = PostXmlRequest(_baseDomain + "/putxml", xml);
            if (response == null)
            {
                return new ServiceResultWithModel<Contact>(false)
                {
                    MessageError = "Error occured while posting xml request"
                };
            }
            var doc = new XmlDocument();
            doc.LoadXml(response);
            contact.Id = doc.SelectSingleNode("Command/ContactAddResult/Name").Value;
            return new ServiceResultWithModel<Contact>(true)
            {
                Data = contact
            };
        }

        public ServiceResultWithModel<string> StartCall(string callNumber)
        {
            var xml = "<Command>" +
               $"<Dial command=\"True\">" +
               $"<Number>{callNumber}</Number>" +
               $"</Dial>" +
             "</Command>";
            try
            {
                var response = PostXmlRequest(_baseDomain + "/putxml", xml);
                var doc = new XmlDocument();
                doc.LoadXml(response);

                var idNode = doc.SelectSingleNode("Command/DialResult/CallId");
                if (idNode == null)
                {
                    return new ServiceResultWithModel<string>(false)
                    {
                        MessageError = "Error occured while parsing response"
                    };
                }
                return new ServiceResultWithModel<string>(true)
                {
                    Data = idNode.InnerText
                };
            }
            catch
            {
                return new ServiceResultWithModel<string>(false)
                {
                    MessageError = "Cannot connect to the server",
                };
            }

        }


        public ServiceResult DisconnectCall(string callId)
        {
            var xml = "<Command><Call>" +
               $"<Disconnect command=\"True\">" +
               $"<CallId>{callId}</CallId>" +
               $"</Disconnect>" +
             "</Call></Command>";
            var response = PostXmlRequest(_baseDomain + "/putxml", xml);
            if (response == null)
            {
                return new ServiceResult(false)
                {
                    MessageError = "Error occured while posting xml request"
                };
            }
            return new ServiceResult(true);
        }

        public ServiceResult UpdateVolume(int volume)
        {
            var xml = "<Command><Audio><Volume>" +
               $"<Set command=\"True\">" +
               $"<Level>{volume}</Level>" +
               $"</Set>" +
             "</Volume></Audio></Command>";
            var response = PostXmlRequest(_baseDomain + "/putxml", xml);
            if (response == null)
            {
                return new ServiceResult(false)
                {
                    MessageError = "Error occured while posting xml request"
                };
            }
            return new ServiceResult(true);
        }

        public void EndSession()
        {
            if (_sessionCookie != null)
            {
                var cookies = new CookieContainer();
                var handler = new HttpClientHandler()
                {
                    CookieContainer = cookies
                };

                var client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(_baseDomain),

                };
                cookies.Add(_sessionCookie);
                var response = client.PostAsync("/xmlapi/session/end", null).Result;
                _sessionCookie = null;
            }
        }

        private void BeginSession()
        {
            var cookies = new CookieContainer();
            var handler = new HttpClientHandler()
            {
                CookieContainer = cookies
            };

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(_baseDomain),
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"testapi:@p!T3stin_g"))) }
            };
            var path = "/xmlapi/session/begin";
            var response = client.PostAsync(path, null).Result;

            var responseCookies = cookies.GetCookies(new Uri(_baseDomain + path)).Cast<Cookie>();
            foreach (var cookie in responseCookies)
            {
                var x = cookie.Value;
            }
            _sessionCookie = responseCookies.First(x => x.Name == "SessionId");
        }

        private Cookie GetSessionCookie()
        {
            if (_sessionCookie == null || _sessionCookie.Expired)
            {
                BeginSession();
            }
            return _sessionCookie;
        }

        private string PostXmlRequest(string uri, string xml)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            var sessionCookie = GetSessionCookie();
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(sessionCookie);
            var bytes = Encoding.ASCII.GetBytes(xml);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return null;
        }

        private List<Contact> DeserializeContactListXml(string response, ContactType type)
        {
            var doc = new XmlDocument();
            doc.LoadXml(response);
            string json;
            var apiContactResult = new ApiContactResult();
            try
            {
                json = JsonConvert.SerializeXmlNode(doc.SelectSingleNode("Command/PhonebookSearchResult"));
                apiContactResult = JsonConvert.DeserializeObject<ApiContactResult>(json);
            }
            catch (Exception e)
            {
                //Only one contact exists
                json = JsonConvert.SerializeXmlNode(doc.SelectSingleNode("Command/PhonebookSearchResult/Contact"));
                var contactResult = JsonConvert.DeserializeObject<ApiSingleContactResult>(json);
                apiContactResult.PhonebookSearchResult = new ApiContactListResult()
                {
                    Contact = new List<ApiContact>() { contactResult.Contact }
                };
            }

            var contacts = apiContactResult.PhonebookSearchResult.Contact.Select(x => new Contact()
            {
                ContactFriendlyName = x.Name,
                ContactName = x.Name,
                Id = x.ContactId,
                Address = x.ContactMethod.Number,
                Type = type,
                ContactMethodId = x.ContactMethod.ContactMethodId
            }).ToList();
            return contacts;
        }

    }
}
