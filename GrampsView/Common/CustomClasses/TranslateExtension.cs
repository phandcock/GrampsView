using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace GrampsView.Common.CustomClasses
{
    // You exclude the 'Extension' suffix when using in XAML
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private const string ResourceId = "GrampsView.Assets.Strings.AppResources";

        private static readonly Lazy<ResourceManager> ResMgr = new(() => new ResourceManager(ResourceId, IntrospectionExtensions.GetTypeInfo(typeof(TranslateExtension)).Assembly));

        private readonly CultureInfo? ci = null;

        [Obsolete]
        public TranslateExtension()
        {
            if (Device.RuntimePlatform is Device.iOS or Device.Android)
            {
                ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            }
        }

        public string Text
        {
            get; set;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                return string.Empty;
            }

            string? translation = ResMgr.Value.GetString(Text, ci);
            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException($"Key '{nameof(Text)}' was not found in resources '{ResourceId}' for culture '{ci.Name}'.",
                        nameof(serviceProvider));
#else
				translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;
        }
    }
}