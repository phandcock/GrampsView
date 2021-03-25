namespace GrampsView.Common
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;

    using Newtonsoft.Json;

    using System.Diagnostics;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// Various common routines.
    /// </summary>

    public static class CommonRoutines
    {
        // deserialise object
        public static T DeserialiseObject<T>(string dataIn) where T : new()
        {
            var ser = JsonConvert.DeserializeObject<T>(dataIn);
            return ser;
        }

        public static Color GetResourceColour(string argColourResourceName)
        {
            // Get colour
            Application.Current.Resources.TryGetValue(argColourResourceName, out var varCardColour);
            return (Color)varCardColour;
        }

        public static bool IsEmulator()
        {
            if (DeviceInfo.DeviceType == DeviceType.Virtual)
            {
                return true;
            }

            return false;
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void ListEmbeddedResources()
        {
            // ... // NOTE: use for debugging, not in released app code!
            var assembly = typeof(App).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
            {
                Debug.WriteLine($"Found resource: {res} ? {ImageSource.FromResource(res, typeof(App)) != null}");
            }
        }

        public static string MimeFileContentTypeGet(string argFileExtension)
        {
            return MimeTypes.MimeTypeMap.GetMimeType(argFileExtension);
        }

        public static string MimeMimeSubTypeGet(string argContentType)
        {
            // Gramps unknown override
            if (argContentType == "unknown")
            {
                return argContentType;
            }

            // get the first part
            string[] t = argContentType.ToLower(System.Globalization.CultureInfo.CurrentCulture).Split('/');

            if (t.Length > 1)
            {
                return t[1].ToLower(System.Globalization.CultureInfo.CurrentCulture);
            }

            ErrorInfo errorDetails = new ErrorInfo("MimeMimeSubTypeGet", "Invalid ContentType")
            {
                { "argContentType", argContentType }
            };

            DataStore.Instance.CN.NotifyError(errorDetails);

            return string.Empty;
        }

        public static string MimeMimeTypeGet(string argContentType)
        {
            // Gramps unknown override
            if (argContentType == "unknown")
            {
                return argContentType;
            }

            // get the first part
            string[] t = argContentType.ToLower(System.Globalization.CultureInfo.CurrentCulture).Split('/');

            if (t.Length > 0)
            {
                return t[0].ToLower(System.Globalization.CultureInfo.CurrentCulture);
            }

            ErrorInfo errorDetails = new ErrorInfo("MimeMimeTypeGet", "Invalid ContentType")
            {
                { "argContentType", argContentType }
            };

            DataStore.Instance.CN.NotifyError(errorDetails);

            return string.Empty;
        }

        public static void Navigate(string argPageName)
        {
            //DataStore.Instance.CN.MinorMessageAdd(string.Format("Navigating to: {0}", argPageName));

            Shell.Current.GoToAsync(argPageName, animate: true);
        }

        public static async Task NavigateAsync(string argPageName)
        {
            //await DataStore.Instance.CN.MinorMessageAdd(string.Format("Navigating to: {0}", argPageName));

            await Shell.Current.GoToAsync(argPageName, animate: true);
        }

        public static void NavigateHub()
        {
            ShellNavigationState t = Shell.Current.CurrentState;

            // Shell.Current.Navigation.PopAsync();

            //DataStore.Instance.CN.MinorMessageAdd("Navigating to Root");

            //if (Device.RuntimePlatform == Device.UWP)
            //{
            //    foreach (var item in Shell.Current.Navigation.NavigationStack)
            //    {
            //        Shell.Current.Navigation.PopAsync();
            //    }
            //}
            //else
            //{
            //MainThread.BeginInvokeOnMainThread(() =>
            //{
            Shell.Current.Navigation.PopToRootAsync();
            //});
            //}
        }

        public static string ReplaceLineSeperators(string argString)
        {
            return Regex.Replace(argString, @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+", "");
        }

        public static Color ResourceColourGet(string keyName)
        {
            var t = ResourceValueGet(keyName);

            if (t is null)
            {
                return Color.White;
            }

            return (Color)t;
        }

        public static object ResourceValueGet(string keyName)
        {
            // Search all dictionaries
            if (Application.Current.Resources.TryGetValue(keyName, out var retVal))
            {
                //TODO cleanup
            }
            return retVal;
        }
    }
}