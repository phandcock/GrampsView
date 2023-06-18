// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Repository;
using GrampsView.Models.DataModels;
using GrampsView.Models.HLinks;

using MimeTypes;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace GrampsView.Common
{
    /// <summary>
    /// Various common routines.
    /// </summary>

    public static partial class CommonRoutines
    {
        public static CardListLineCollection GetHLinkInfoFormatted(HLinkBase argHLink)
        {
            if (argHLink is null)
            {
                throw new ArgumentNullException(nameof(argHLink));
            }

            CardListLineCollection hlinkInfoList = new()
            {
                 new CardListLine("Private Object:", argHLink.Priv.ToString()),
               };

            hlinkInfoList.Title = "Admin Details";

            return hlinkInfoList;
        }

        // De-serialise object
        public static T GetHLinkParameter<T>(IDictionary<string, object> dataIn) where T : new()
        {
            T ser = new();

            if (dataIn is null)
            {
                return ser;
            }

            foreach (KeyValuePair<string, object> item in dataIn)
            {
                Debug.WriteLine($"dataIn - {item.Key}: {item.Value}");
            }

            if (dataIn.Count > 0)
            {
                //object tt = dataIn["HLinkSerial"];
                //Debug.WriteLine(tt);
                //object ttt = dataIn["HLinkSerial"];
                //Debug.WriteLine(ttt);

                ser = JsonSerializer.Deserialize<T>(Uri.UnescapeDataString((string)dataIn[SharedSharpConstants.ShellParameter1]));
            }

            return ser;
        }

        public static T GetHLinkParameter<T>(string dataIn) where T : new()
        {
            T ser = new();

            //object tt = dataIn["HLinkSerial"];
            //Debug.WriteLine(tt);
            //object ttt = dataIn["HLinkSerial"];
            //Debug.WriteLine(ttt);

            ser = JsonSerializer.Deserialize<T>(Uri.UnescapeDataString(dataIn));

            return ser;
        }

        public static CardListLineCollection GetModelInfoFormatted(ModelBase argModel)
        {
            if (argModel is null)
            {
                throw new ArgumentNullException(nameof(argModel));
            }

            CardListLineCollection modelInfoList = new()
            {
                 new CardListLine("Id:", argModel.Id),
                 new CardListLine("Change:", argModel.Change.ToString(System.Globalization.CultureInfo.CurrentCulture)),
                 new CardListLine("Private Object:", argModel.Priv.ToString()),
               };

            modelInfoList.Title = "Admin Details";

            return modelInfoList;
        }

        public static string GetSerialFile(string argObjectName)
        {
            return argObjectName.Trim() + ".json";
        }

        public static string GetSerialFileFull(string argObjectName)
        {
            return DataStore.Instance.AD.CurrentDataFolder.GetAbsoluteFilePath(GetSerialFile(argObjectName));
        }

        public static JsonSerializerOptions GetSerializerOptions()
        {
            JsonSerializerOptions serializeOptions = new();

            // Special converter for colours
            serializeOptions.Converters.Add(new Converters.JsonColorConverter());
            //serializeOptions.Converters.Add(new Converters.JsonDateObjectModelConverter());

            //// TODO Why does this work Preserve reference data
            //serializeOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;

            serializeOptions.IgnoreReadOnlyFields = true;
            serializeOptions.IgnoreReadOnlyProperties = true;
            serializeOptions.DefaultBufferSize = 32768;

            return serializeOptions;
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void ListEmbeddedResources()
        {
            // ... // NOTE: use for debugging, not in released app code!
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;
            foreach (string res in assembly.GetManifestResourceNames())
            {
                Debug.WriteLine($"Found resource: {res} ? {ImageSource.FromResource(res, typeof(App)) != null}");
            }
        }

        public static string MimeFileContentTypeGet(string argFileExtension)
        {
            return MimeTypeMap.GetMimeType(argFileExtension);
        }

        public static bool ReleaseMode()
        {
            // From https://dave-black.blogspot.com/2011/12/how-to-tell-if-assembly-is-debug-or.html

            bool HasDebuggableAttribute = false;
            bool IsJITOptimized = false;
            bool IsJITTrackingEnabled = false;
            string BuildType = "";
            string DebugOutput = "";
            Assembly ReflectedAssembly = Assembly.LoadFile(@"C:\src\TMDE\Git\RedisScalingTest\bin\Release\netcoreapp3.1\RedisScalingTest.dll");

            // var ReflectedAssembly = Assembly.LoadFile(@"path to the dll you are testing");
            object[] attribs = ReflectedAssembly.GetCustomAttributes(typeof(DebuggableAttribute), false);

            // If the 'DebuggableAttribute' is not found then it is definitely an OPTIMIZED build
            if (attribs.Length > 0)
            {
                // Just because the 'DebuggableAttribute' is found doesn't necessarily mean it's a
                // DEBUG build; we have to check the JIT Optimization flag i.e. it could have the
                // "generate PDB" checked but have JIT Optimization enabled
                if (attribs[0] is DebuggableAttribute debuggableAttribute)
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

            return BuildType == "Release";
        }

        public static string ReplaceLineSeparators(string argString)
        {
            return Regex.Replace(argString, @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+", "");
        }

        public static Color ResourceColourGet(string keyName)
        {
            object t = ResourceValueGet(keyName);

            return t is null ? Colors.White : (Color)t;
        }

        public static object? ResourceValueGet(string keyName)
        {
            // Test if running in NUnit test mode
            if (Application.Current is null)
            {
                return null;
            }

            // Search all dictionaries
            if (!Application.Current.Resources.TryGetValue(keyName, out object retVal))
            {
                IErrorNotifications t = Ioc.Default.GetRequiredService<IErrorNotifications>();

                ErrorInfo tt = new("Bad Resource Key", keyName)
                {
                    new CardListLine("T", "test"),
                };

                t.NotifyError(tt);
            }

            return retVal;
        }
    }
}