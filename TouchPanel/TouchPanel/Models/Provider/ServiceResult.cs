using System;
using System.Collections.Generic;
using System.Text;

namespace TouchPanel.Models.Provider
{
    public class ServiceResult
    {
        public ServiceResult()
        {
            IsSuccessed = true;
        }

        public ServiceResult(bool isSuccessed)
        {
            IsSuccessed = isSuccessed;
        }

        public bool IsSuccessed { get; set; }

        public string MessageError { get; set; }
    }
}
