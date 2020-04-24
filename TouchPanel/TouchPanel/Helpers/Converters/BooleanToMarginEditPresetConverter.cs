using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;


namespace TouchPanel.Helpers.Converters
{
    public class BooleanToMarginEditPresetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? new Xamarin.Forms.Rectangle(0.5, .5, 370, 200) : new Xamarin.Forms.Rectangle(1, 1, 0, 0);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
