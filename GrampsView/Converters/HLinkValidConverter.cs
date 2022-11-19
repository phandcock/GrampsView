namespace GrampsView.Converters
{
    using GrampsView.Models.HLinks;

    using System;
    using System.Globalization;

  

    internal class HLinkValidConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            return ((HLinkBase)value).Valid;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}