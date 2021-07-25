namespace GrampsView.e2e.Test.Utility
{
    using GrampsView.Data;
    using GrampsView.Data.External.StoreSerial;
    using GrampsView.Data.ExternalStorage;
    using GrampsView.Data.Repository;
    using GrampsView.Test.e2e.Utility;

    using NUnit.Framework;

    using Prism.Events;

    using System.Diagnostics;
    using System.Reflection;

    using Xamarin.Forms;

    public static class DataStoreUtility
    {
        public const string BasePath = "GrampsView.Test.e2e";

        public static void DataStoreSetup()
        {
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

        public static void LoadTestFile()
        {
            // Load Resource
            var assemblyExec = Assembly.GetExecutingAssembly();
            var resourceName = BasePath + ".Test_Data.GrampsView Test Basic.gpkg";

            DataStore.Instance.AD.CurrentInputStream = assemblyExec.GetManifestResourceStream(resourceName);

            DataStore.Instance.AD.CurrentInputStreamPath = "Test Data/Test_Data.GrampsView Test Basic.gpkg";

            // Remove the old dateTime stamps so the files get reloaded even if they have been seen
            // before TODO CommonLocalSettings.SetReloadDatabase();

            GeneralData.setupMocks();

            // Other setup
            IEventAggregator iocEventAggregator = GeneralData.mocEventAggregator.Object;

            IStoreXML iocExternalStorage = new StoreXML(GeneralData.iocCommonLogging, GeneralData.iocCommonNotifications);

            IStorePostLoad iocGrampsStorePostLoad = new StorePostLoad(GeneralData.iocCommonLogging, GeneralData.iocCommonNotifications, iocEventAggregator, GeneralData.iocPlatformSpecific);

            IGrampsStoreSerial iocGrampsStoreSerial = new GrampsStoreSerial(GeneralData.iocCommonLogging);

            IStoreFile iocStoreFile = new StoreFile();

            DataRepositoryManager newManager = new DataRepositoryManager(GeneralData.iocCommonLogging, GeneralData.iocCommonNotifications, iocEventAggregator, iocExternalStorage, iocGrampsStorePostLoad, iocGrampsStoreSerial, iocStoreFile);

            StorePostLoad newPostLoad = new StorePostLoad(GeneralData.iocCommonLogging, GeneralData.iocCommonNotifications, iocEventAggregator, GeneralData.iocPlatformSpecific);

            //// Clear the repositories in case we had to restart after being interupted. TODO have
            //// better mock DataStore.Instance.AD.LoadDataStore();
            //DataStore.Instance.AD.CurrentDataFolder.Value = new DirectoryInfo(DataStorePath);
            //if (DataStore.Instance.AD.CurrentDataFolder.Value.Exists)
            //{
            //    DataStore.Instance.AD.CurrentDataFolder.Value.Delete(true);
            //}
            //DataStore.Instance.AD.CurrentDataFolder.Value.Create();

            // Time to start loading the data
            DataStoreSetup();

            DataRepositoryManager.ClearRepositories();

            newManager.StartDataLoad();

            newPostLoad.LoadXMLUIItems(null);

            //// 1) UnTar *.GPKG
            //iocStoreFile.DataStorageInitialiseAsync().ConfigureAwait(false);

            //newManager.TriggerLoadGPKGFileAsync().ConfigureAwait(false);

            //// 2) UnZip new data.GRAMPS file
            //FileInfoEx GrampsFile = StoreFolder.FolderGetFile(CommonConstants.StorageGRAMPSFileName);

            //DataStore.Instance.CN.DataLogEntryAdd("Later version of Gramps data file found. Loading it into the program").ConfigureAwait(false);

            //newManager.TriggerLoadGRAMPSFileAsync(false).ConfigureAwait(false);

            //// 3) Load new data.XML file
            //FileInfoEx dataXML = StoreFolder.FolderGetFile(CommonConstants.StorageXMLFileName);

            //DataStore.Instance.CN.DataLogEntryAdd("Later version of Gramps XML data file found. Loading it into the program").ConfigureAwait(false);

            //// Load the new data
            //newManager.TriggerLoadGrampsUnZippedFolderAsync().ConfigureAwait(false);

            //// Fixup the models and hlinks
            //StorePostLoad myStorePostLoad = new StorePostLoad(iocCommonLogging, iocEventAggregator, iocPlatformSpecific);
            //myStorePostLoad.LoadXMLUIItems(null);

            //DataStore.Instance.CN.DataLogHide();

            Assert.True(DataStore.Instance.AD.CurrentInputStream != null);
        }
    }
}