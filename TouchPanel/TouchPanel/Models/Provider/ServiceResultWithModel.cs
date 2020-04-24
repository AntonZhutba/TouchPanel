using System;
using System.Collections.Generic;
using System.Text;

namespace TouchPanel.Models.Provider
{
    public class ServiceResultWithModel<TResult> : ServiceResult
    {
        public ServiceResultWithModel() : base()
        {
        }

        public ServiceResultWithModel(bool isSuccessed) : base(isSuccessed)
        {
        }


        public TResult Data { get; set; }
    }
}
