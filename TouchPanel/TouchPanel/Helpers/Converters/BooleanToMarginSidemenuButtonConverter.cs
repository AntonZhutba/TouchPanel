﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TouchPanel.Helpers.Converters
{
    class BooleanToMarginSidemenuButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? new Xamarin.Forms.Rectangle(1, .15, 400, 80) : new Xamarin.Forms.Rectangle(1, .15, 0, 80);

        }
       
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {   
            throw new NotImplementedException();
        }
    }
}
