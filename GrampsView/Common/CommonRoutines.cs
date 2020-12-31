namespace GrampsView.Common
{
    using Newtonsoft.Json;

    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

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

        public static void LoadHubPage()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (Shell.Current.Navigation.NavigationStack.Count > 0)
                {
                    if (Shell.Current.Navigation.NavigationStack.First() == null)
                    {
                        //var t = ((IShellContentController)Shell.Current.CurrentItem.CurrentItem.CurrentItem).Page;
                        //Shell.Current.Navigation.RemovePage(((IShellContentController)Shell.Current.CurrentItem.CurrentItem.CurrentItem).Page);
                    }
                }

                await Shell.Current.Navigation.PopToRootAsync(animated: true);

                // await Shell.Current.GoToAsync(nameof(HubPage));
            });
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
            if (Application.Current.Resources.TryGetValue(keyName, out var retVal)) { }
            return retVal;
        }

        // serialise object
        public static string SerialiseObject<T>(T dataIn) where T : new()
        {
            var ser = JsonConvert.SerializeObject(dataIn);
            return ser;
        }
    }
}