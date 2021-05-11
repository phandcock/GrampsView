namespace GrampsView.e2e.Test.Utility
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data;
    using GrampsView.Data.External.StoreSerial;
    using GrampsView.Data.ExternalStorage;
    using GrampsView.Data.Repository;
    using GrampsView.Events;

    using Moq;

    using NUnit.Framework;

    using Prism.Events;

    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    using Xamarin.Forms;

    public static class DataStoreUtility
    {
        public const string BasePath = "GrampsView.Test.e2e";

        public static string DataStorePath = Path.Combine(Path.GetTempPath(), "UnitTestDataStore");

        public static DataStore testDataStore;

        public static void createResources()
        {
        }

        public static void DataStoreSetup()
        {
            // AD

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

        public static void LoadTestFile()
        {
            // Load Resource
            var assemblyExec = Assembly.GetExecutingAssembly();
            var resourceName = DataStoreUtility.BasePath + ".Test_Data.e2e test default.gpkg";

            DataStore.Instance.AD.CurrentInputStream = assemblyExec.GetManifestResourceStream(resourceName);

            DataStore.Instance.AD.CurrentInputStreamPath = "Test Data/Test_Data.e2e test default.gpkg";

            // Remove the old dateTime stamps so the files get reloaded even if they have been seen
            // before TODO CommonLocalSettings.SetReloadDatabase();

            ICommonLogging iocCommonLogging = new CommonLogging();

            ////////////////
            Mock<ICommonNotifications> mockCommonNotifications = new Mock<ICommonNotifications>();
            mockCommonNotifications
               .Setup(x => x.DataLog)
               .Returns(new Mock<IDataLog>().Object);

            ICommonNotifications iocCommonNotifications = mockCommonNotifications.Object;
            DataStore.Instance.CN = iocCommonNotifications;

            //////////////////
            Mock<IEventAggregator> mocEventAggregator = new Mock<IEventAggregator>();

            var mockedEventDataLoadXMLEvent = new Mock<DataLoadXMLEvent>();
            mocEventAggregator
                  .Setup(x => x.GetEvent<DataLoadXMLEvent>())
                  .Returns(mockedEventDataLoadXMLEvent.Object);

            var mockedEventDataLoadStartEvent = new Mock<DataLoadStartEvent>();
            mocEventAggregator
                  .Setup(x => x.GetEvent<DataLoadStartEvent>())
                  .Returns(mockedEventDataLoadStartEvent.Object);

            var mockedEventDataSaveSerialEvent = new Mock<DataSaveSerialEvent>();
            mocEventAggregator
                .Setup(x => x.GetEvent<DataSaveSerialEvent>())
                .Returns(mockedEventDataSaveSerialEvent.Object);

            var mockedEventDataLoadCompleteEvent = new Mock<DataLoadCompleteEvent>();
            mocEventAggregator
                .Setup(x => x.GetEvent<DataLoadCompleteEvent>())
                .Returns(mockedEventDataLoadCompleteEvent.Object);

            // Mock Platform specific
            Mock<IPlatformSpecific> mocPlatformSpecific = new Mock<IPlatformSpecific>();

            IPlatformSpecific iocPlatformSpecific = mocPlatformSpecific.Object;

            // Other setup
            IEventAggregator iocEventAggregator = mocEventAggregator.Object;

            IStoreXML iocExternalStorage = new StoreXML(iocCommonLogging);

            IStorePostLoad iocGrampsStorePostLoad = new StorePostLoad(iocCommonLogging, iocEventAggregator, iocPlatformSpecific);

            IGrampsStoreSerial iocGrampsStoreSerial = new GrampsStoreSerial(iocCommonLogging);

            IStoreFile iocStoreFile = new StoreFile();

            DataRepositoryManager newManager = new DataRepositoryManager(iocCommonLogging, iocEventAggregator, iocExternalStorage, iocGrampsStorePostLoad, iocGrampsStoreSerial, iocStoreFile);

            // Clear the repositories in case we had to restart after being interupted. TODO have
            // better mock DataStore.Instance.AD.LoadDataStore();
            DataStore.Instance.AD.CurrentDataFolder = new DirectoryInfo(DataStorePath);
            if (DataStore.Instance.AD.CurrentDataFolder.Exists)
            {
                DataStore.Instance.AD.CurrentDataFolder.Delete(true);
            }
            DataStore.Instance.AD.CurrentDataFolder.Create();

            DataRepositoryManager.ClearRepositories();

            // 1) UnTar *.GPKG

            iocStoreFile.DataStorageInitialiseAsync().ConfigureAwait(false);

            newManager.TriggerLoadGPKGFileAsync().ConfigureAwait(false);

            // 2) UnZip new data.GRAMPS file
            FileInfoEx GrampsFile = StoreFolder.FolderGetFile(DataStore.Instance.AD.CurrentDataFolder, CommonConstants.StorageGRAMPSFileName);

            DataStore.Instance.CN.DataLogEntryAdd("Later version of Gramps data file found. Loading it into the program").ConfigureAwait(false);

            newManager.TriggerLoadGRAMPSFileAsync(false).ConfigureAwait(false);

            // 3) Load new data.XML file
            FileInfoEx dataXML = StoreFolder.FolderGetFile(DataStore.Instance.AD.CurrentDataFolder, CommonConstants.StorageXMLFileName);

            DataStore.Instance.CN.DataLogEntryAdd("Later version of Gramps XML data file found. Loading it into the program").ConfigureAwait(false);

            // Load the new data
            newManager.TriggerLoadGrampsUnZippedFolderAsync().ConfigureAwait(false);

            DataStore.Instance.CN.DataLogHide();

            Assert.True(DataStore.Instance.AD.CurrentInputStream != null);
        }
    }
}