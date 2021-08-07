namespace GrampsView.Common
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using GrampsView.Views;

    using Newtonsoft.Json;

    using System;
    using System.Diagnostics;
    using System.IO;
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
        public static CardListLineCollection GetHLinkInfoFormatted(HLinkBase argHLink)
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

        // Deserialise object
        public static T GetHLinkParameter<T>(string dataIn) where T : new()
        {
            T ser = JsonConvert.DeserializeObject<T>(Uri.UnescapeDataString(dataIn));

            return ser;
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

        public static void ImageCacheFolderInit()
        {
            try
            {
                string tt = System.IO.Path.Combine(DataStore.Instance.ES.FileSystemCacheDirectory, CommonConstants.DirectoryCacheBase, CommonConstants.DirectoryImageCache);

                DataStore.Instance.AD.CurrentImageAssetsFolder.Value = new DirectoryInfo(tt);

                DirectoryInfo t = new DirectoryInfo(System.IO.Path.Combine(DataStore.Instance.ES.FileSystemCacheDirectory, CommonConstants.DirectoryCacheBase));

                if (!DataStore.Instance.AD.CurrentImageAssetsFolder.Value.Exists)
                {
                    t.CreateSubdirectory(CommonConstants.DirectoryImageCache);
                }
            }
            catch (Exception ex)
            {
                DataStore.Instance.CN.NotifyException("Exception creating application image cache", ex, null);
                throw;
            }
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
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Shell.Current.GoToAsync(argPageName, animate: false);
            });
        }

        public static async Task NavigateBack()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                AppShell.Current.Navigation.PopAsync();               // Go back
            });
        }

        public static void NavigateHub()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Shell.Current.GoToAsync($"//{nameof(HubPage)}");
            });
        }

        public static bool ReleaseMode()
        {
            // From https://dave-black.blogspot.com/2011/12/how-to-tell-if-assembly-is-debug-or.html

            bool HasDebuggableAttribute = false;
            var IsJITOptimized = false;
            var IsJITTrackingEnabled = false;
            var BuildType = "";
            var DebugOutput = "";
            var ReflectedAssembly = Assembly.LoadFile(@"C:\src\TMDE\Git\RedisScalingTest\bin\Release\netcoreapp3.1\RedisScalingTest.dll");

            // var ReflectedAssembly = Assembly.LoadFile(@"path to the dll you are testing");
            object[] attribs = ReflectedAssembly.GetCustomAttributes(typeof(DebuggableAttribute), false);

            // If the 'DebuggableAttribute' is not found then it is definitely an OPTIMIZED build
            if (attribs.Length > 0)
            {
                // Just because the 'DebuggableAttribute' is found doesn't necessarily mean it's a
                // DEBUG build; we have to check the JIT Optimization flag i.e. it could have the
                // "generate PDB" checked but have JIT Optimization enabled
                DebuggableAttribute debuggableAttribute = attribs[0] as DebuggableAttribute;
                if (debuggableAttribute != null)
                {
                    HasDebuggableAttribute = true;
                    IsJITOptimized = !debuggableAttribute.IsJITOptimizerDisabled;

                    // IsJITTrackingEnabled - Gets a value that indicates whether the runtime will
                    // track information during code generation for the debugger.
                    IsJITTrackingEnabled = debuggableAttribute.IsJITTrackingEnabled;
                    BuildType = debuggableAttribute.IsJITOptimizerDisabled ? "Debug" : "Release";

                    // check for Debug Output "full" or "pdb-only"
                    DebugOutput = (debuggableAttribute.DebuggingFlags &
                                    DebuggableAttribute.DebuggingModes.Default) !=
                                    DebuggableAttribute.DebuggingModes.None
                                    ? "Full" : "pdb-only";
                }
            }
            else
            {
                IsJITOptimized = true;
                BuildType = "Release";
            }

            // Output
            Debug.WriteLine("HasDebuggableAttribute", HasDebuggableAttribute);
            Debug.WriteLine("IsJITOptimized", IsJITOptimized);
            Debug.WriteLine("IsJITTrackingEnabled", IsJITTrackingEnabled);
            Debug.WriteLine("DebugOutput", DebugOutput);

            if (BuildType == "Release")
            {
                return true;
            }

            return false;
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
            if (!Application.Current.Resources.TryGetValue(keyName, out var retVal))
            {
                DataStore.Instance.CN.NotifyError(new ErrorInfo("Bad Resource Key", keyName));
            }

            return retVal;
        }
    }
}