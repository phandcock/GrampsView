namespace GrampsView.Converters
{
    using GrampsView.Data.Model;

    using System;
    using System.Diagnostics.Contracts;

    using Xamarin.Forms;

    public class HLinkValidToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Contract.Assert(value != null);
            Contract.Assert(value is HLinkBase);

            HLinkBase t = value as HLinkBase;

            if (t != null)
            {
                return t.Valid;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
    }
}