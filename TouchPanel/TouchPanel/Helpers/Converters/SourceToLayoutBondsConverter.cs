using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TouchPanel.Models;
using Xamarin.Forms;
namespace TouchPanel.Helpers.Converters
{
    class SourceToLayoutBondsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var source = value as Source;
            if (source != null)
            {
                var x = 72 + (source.Column * 296);
                int y = 0;
                switch (source.Row)
                {
                    case 0:
                        y = 100;
                        break;
                    case 1:
                        y = 310;
                        break;
                    case 2:
                        y = 450;
                        break;
                }
                return new Xamarin.Forms.Rectangle(x, y, 250, 180);
            }
            return new Xamarin.Forms.Rectangle(0, 0, 0, 0);


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
