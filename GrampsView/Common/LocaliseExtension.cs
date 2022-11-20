using GrampsView.Resources.Strings;

using Microsoft.Extensions.Localization;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

namespace GrampsView.Common
{
    public class LocaliseExtension : IMarkupExtension
    {
        private readonly IStringLocalizer<AppResources> _localizer;

        public string Key { get; set; } = string.Empty;

        public LocaliseExtension()
        {
            _localizer = Ioc.Default.GetService<IStringLocalizer<AppResources>>();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                string localizedText = _localizer[Key];
                return localizedText;
            }
            catch (FileNotFoundException ex)
            {
                ErrorInfo err = new ErrorInfo("LocaliseExtension", "Key not found") { new SharedSharp.Model.CardListLine("Key", Key) };

                Ioc.Default.GetService<IErrorNotifications>().NotifyException("LocaliseExtension", ex, err);

                return "LocaliseExtension Exception";
            }
            catch (Exception ex)
            {
                ErrorInfo err = new ErrorInfo("LocaliseExtension", "ProvideValue") { new SharedSharp.Model.CardListLine("Key", Key) };

                Ioc.Default.GetService<IErrorNotifications>().NotifyException("LocaliseExtension", ex, err);

                return "LocaliseExtension Exception";
            }

        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }
    }
}