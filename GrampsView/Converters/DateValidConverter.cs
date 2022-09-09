namespace GrampsView.Converters
{
    using GrampsView.Data.Model;

    using System;
    using System.Globalization;

    using Xamarin.Forms;

    internal class DateValidConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            return ((DateObjectModel)value).Valid;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}