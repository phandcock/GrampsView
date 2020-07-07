namespace GrampsView.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    public class NegateBooleanConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}