//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="NotEmptyVisibleConverter.cs" company="MeMySelfandI">
//     GPL Copyright
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Converters
{
    using System;
    using System.Globalization;

    using Xamarin.Forms;

    public sealed class IconPath : IValueConverter
    {
        #region Methods

        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// Type of the target.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <returns>
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return string.Empty;
            }

            string iconName = value as string;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return (string)iconName;

                case Device.Android:
                    return iconName;

                case Device.UWP:
                    return (string)"Assets/Icons/AreaIcons/" + iconName;

                case Device.macOS:
                    return iconName;

                default:

                    return iconName;
            }
        }

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// Type of the target.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <returns>
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion Methods

        // namespace Contoso.Converters { public class MyValueConverter : IValueConverter { private
        // ILoggerFacade _logger { get; }

        // public MyValueConverter(ILoggerFacade logger) { _logger = logger; }

        // public object Convert(object value, Type targetType, object parameter, CultureInfo
        // culture) { _logger.Log("Converting value", Category.Debug, Priority.None); return value; }

        // public object ConvertBack(object value, Type targetType, object parameter, CultureInfo
        // culture) { _logger.Log("Converting Value Back...", Category.Debug, Priority.None); return
        // value; } } }

        // This can then be used in XAML using the ContainerProvider as follows:

        //<ContentPage xmlns = "http://xamarin.com/schemas/2014/forms"
        //             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
        //             xmlns:ioc="clr-namespace:Prism.Ioc;assembly=Prism.Forms"
        //             xmlns:converters="using:Contoso.Converters"
        //             x:Class="Contoso.Views.ViewA">
        //    <ContentPage.Resources>
        //        <ResourceDictionary>
        //            <ioc:ContainerProvider x:TypeArguments="converters:MyValueConverter" x:Key="myValueConverter" />
        //        </ResourceDictionary>
        //    </ContentPage.Resources>
        //    <Entry Text = "{Binding Demo,Converter={StaticResource myValueConverter}}" />
        //</ ContentPage >
    }
}