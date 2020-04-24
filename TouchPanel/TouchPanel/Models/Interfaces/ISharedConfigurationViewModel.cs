using System;
using System.Collections.Generic;
using System.Text;

namespace TouchPanel.Models.Interfaces
{
    public interface ISharedConfigurationViewModel
    {
        ApplicationConfiguration ApplicationConfiguration { get; set; }
    }
}
