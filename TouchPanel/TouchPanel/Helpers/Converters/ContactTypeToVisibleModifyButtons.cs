using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using TouchPanel.Models.Enums;

namespace TouchPanel.Helpers.Converters
{
    public class ContactTypeToVisibleModifyButtons : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (ContactType)value == ContactType.Contact;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
