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
            Contract.Assert(!(value is null));
            Contract.Assert(value is HLinkBase);

            HLinkBase t = value as HLinkBase;

            return t.Valid;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
    }
}