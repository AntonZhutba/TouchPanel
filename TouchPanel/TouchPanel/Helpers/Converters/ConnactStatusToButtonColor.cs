using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TouchPanel.Models.Enums;
using Xamarin.Forms;

namespace TouchPanel.Helpers.Converters
{
    class ConnactStatusToButtonColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (ConnectStatus)value;
            switch (status)
            {
                case ConnectStatus.Idle:
                    return Color.DodgerBlue;
                case ConnectStatus.InProgress:
                    return Color.FromHex("#8cc4e7");
                case ConnectStatus.Connected:
                    return Color.FromHex("a8d4cf");
                default:
                    return Color.DodgerBlue;
            }          
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
