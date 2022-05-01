namespace GrampsView.Converters
{
    using GrampsView.Data.Model;

    using System;
    using System.Globalization;

    using Xamarin.Forms;

    internal class BackLinkDefaultTextShortConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(HLinkBackLink))
            {
                return "???";
            }

            if ((value as HLinkBackLink).HLink.GetType() == typeof(HLinkNoteModel))
            {
                return (((value as HLinkBackLink).HLink) as HLinkNoteModel).DeRef.DefaultTextShort;
            }

            if ((value as HLinkBackLink).HLink.GetType() == typeof(HLinkPersonModel))
            {
                return (((value as HLinkBackLink).HLink) as HLinkPersonModel).DeRef.DefaultTextShort;
            }

            return "??? Unknown Type???";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}