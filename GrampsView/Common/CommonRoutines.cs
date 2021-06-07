namespace GrampsView.Common
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using GrampsView.Views;

    using Newtonsoft.Json;

    using System;
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

        public static Data.Model.CardListLineCollection GetHLinkInfoFormatted(HLinkBase argHLink)
        {
            if (argHLink is null)
            {
                throw new ArgumentNullException(nameof(argHLink));
            }

            CardListLineCollection hlinkInfoList = new CardListLineCollection
               {
                 new CardListLine("Private Object:", argHLink.Priv.ToString()),
               };

            hlinkInfoList.Title = "Admin Details";

            return hlinkInfoList;
        }

        public static CardListLineCollection GetModelInfoFormatted(ModelBase argModel)
        {
            if (argModel is null)
            {
                throw new ArgumentNullException(nameof(argModel));
            }

            CardListLineCollection modelInfoList = new CardListLineCollection
               {
                 new CardListLine("Handle:", argModel.Handle),
                 new CardListLine("Id:", argModel.Id),
                 new CardListLine("Change:", argModel.Change.ToString(System.Globalization.CultureInfo.CurrentCulture)),
                 new CardListLine("Private Object:", argModel.Priv.ToString()),
               };

            modelInfoList.Title = "Admin Details";

            return modelInfoList;
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

        public static async Task NavigateAsync(string argPageName)
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Shell.Current.GoToAsync(argPageName, animate: false);
                });
            }
            else
            {
                await Shell.Current.GoToAsync(argPageName, animate: false);
            };
        }

        public static async Task NavigateBack()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    AppShell.Current.Navigation.PopAsync();               // Go back
                });
            }
            else
            {
                await AppShell.Current.Navigation.PopAsync();               // Go back
            };
        }

        public static void NavigateHub()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Shell.Current.GoToAsync($"//{nameof(HubPage)}");
                });
            }
            else
            {
                Shell.Current.GoToAsync($"//{nameof(HubPage)}");
            };
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
            // Test if running in NUnit test mode
            if (Application.Current is null)
            {
                return null;
            }

            // Search all dictionaries
            if (Application.Current.Resources.TryGetValue(keyName, out var retVal))
            {
                //TODO cleanup
            }
            return retVal;
        }
    }
}