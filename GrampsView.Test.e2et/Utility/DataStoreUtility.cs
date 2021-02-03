namespace GrampsView.e2e.Test.Utility
{
    using GrampsView.Data.Repository;

    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    using Xamarin.Forms;

    public static class DataStoreUtility
    {
        public const string BasePath = "GrampsView.Test.e2e";

        public static string DataStorePath = Path.Combine(Path.GetTempPath(), "UnitTestDataStore");

        public static void DataStoreSetup()
        {
            if (!DataStore.Instance.AD.CurrentDataFolderValid)
            {
                // Delete if it exists
                if (Directory.Exists(DataStorePath))
                {
                    Directory.Delete(DataStorePath, true);
                }

                Directory.CreateDirectory(DataStorePath);

                DataStore.Instance.AD.CurrentDataFolder = new DirectoryInfo(DataStorePath);
            }
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void ListEmbeddedResources()
        {
            // ... // NOTE: use for debugging, not in released app code!
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var res in assembly.GetManifestResourceNames())
            {
                Debug.WriteLine($"Found resource: {res} ? {ImageSource.FromResource(res, typeof(App)) != null}");
            }
        }
    }
}