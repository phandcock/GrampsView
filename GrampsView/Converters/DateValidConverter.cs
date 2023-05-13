// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.DataModels.Date;

using System;
using System.Globalization;

namespace GrampsView.Converters
{
    internal class DateValidConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            return ((DateObjectModelBase)value).Valid;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}