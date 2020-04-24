using System;
using System.Collections.Generic;
using System.Text;

namespace TouchPanel.Models.Provider
{
    public class ContactMethod
    {
        public string Number { get; set; }

        public string Protocol { get; set; }

        public string ContactMethodId { get; set; }

    }

    public class ApiContact
    {
        public string ContactId { get; set; }
        public string Name { get; set; }

        public ContactMethod ContactMethod { get; set; }
    }

    public class ApiSingleContactResult
    {
        public ApiContact Contact { get; set; }

    }

    public class ApiContactListResult
    {
        public List<ApiContact> Contact { get; set; }

    }
    public class ApiContactResult
    {
        public ApiContactListResult PhonebookSearchResult { get; set; }
        
    }
}
