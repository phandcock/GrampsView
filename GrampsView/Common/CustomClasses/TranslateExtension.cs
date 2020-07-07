namespace GrampsView.Common
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    // You exclude the 'Extension' suffix when using in XAML
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private const string ResourceId = "GrampsView.Assets.Strings.AppResources";

        private static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, IntrospectionExtensions.GetTypeInfo(typeof(TranslateExtension)).Assembly));

        private readonly CultureInfo ci = null;

        public TranslateExtension()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                ci = DependencyService.Get<GrampsView.Common.ILocalize>().GetCurrentCultureInfo();
            }
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;

            var translation = ResMgr.Value.GetString(Text, ci);
            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    string.Format(
                        System.Globalization.CultureInfo.CurrentCulture,
                        "Key '{0}' was not found in resources '{1}' for culture '{2}'.", nameof(Text), ResourceId, ci.Name),
                    "Text");
#else
				translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;
        }
    }
}