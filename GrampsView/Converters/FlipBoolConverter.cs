namespace GrampsView.Converters
{
    using System;

    using Xamarin.Forms;

    public class FlipBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
          

            if (value is bool)
            {
                bool t = (bool)value;

                return !t;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
    }
}