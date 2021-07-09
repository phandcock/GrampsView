namespace GrampsView.Test.e2e.Utility
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data;
    using GrampsView.Data.External.StoreSerial;
    using GrampsView.Data.ExternalStorage;
    using GrampsView.Data.Repository;
    using GrampsView.Events;

    using Moq;

    using Prism.Events;

    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    using Xamarin.Forms;

    public static class GeneralData
    {
        //public static string DataStorePath = Path.Combine(DataStore.Instance.ES.FileSystemCacheDirectory, "UnitTestDataStore");
        public static IPlatformSpecific iocPlatformSpecific;

        public static IStoreFile iocStoreFile;

        public static Mock<IEventAggregator> mocEventAggregator = new Mock<IEventAggregator>();
        public static Mock<IPlatformSpecific> mocPlatformSpecific = new Mock<IPlatformSpecific>();
        public static DataRepositoryManager newManager;

        public static void DataStoreSetup()
        {
            // AD

            //if (!DataStore.Instance.AD.CurrentDataFolder.Valid)
            //{
            //    // Delete if it exists
            //    if (Directory.Exists(DataStorePath))
            //    {
            //        Directory.Delete(DataStorePath, true);
            //    }

            // Directory.CreateDirectory(DataStorePath);

            //    DataStore.Instance.AD.CurrentDataFolder.Value = new DirectoryInfo(DataStorePath);
            //}
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

        public static void setupMocks()
        {
            /*
             * Mock Common Logging
             */
            ICommonLogging iocCommonLogging = new CommonLogging();

            /*
             * Mock Common Notifications
             */
            Mock<ICommonNotifications> mockCommonNotifications = new Mock<ICommonNotifications>();

            mockCommonNotifications
                .Setup(x => x.DataLog)
                .Returns(new CommonDataLog());

            ICommonNotifications iocCommonNotifications = mockCommonNotifications.Object;

            /*
            * Mock Image Loading
            */
            Mock<IFFImageLoading> mocFFImageLoading = new Mock<IFFImageLoading>();
            IFFImageLoading iocFFImageLoading = mocFFImageLoading.Object;

            /*
           * Mock Xamarin Essentials
           */
            Mock<IXamarinEssentials> mocXamarinEssentials = new Mock<IXamarinEssentials>();

            mocXamarinEssentials
                .Setup(x => x.FileSystemCacheDirectory)
                .Returns(Path.GetTempPath());

            IXamarinEssentials iocXamarinEssentials = mocXamarinEssentials.Object;

            /*
            * Mock Event Aggregator
            */
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

            IEventAggregator iocEventAggregator = mocEventAggregator.Object;

            /*
            * Mock Platform specific
            */
            iocPlatformSpecific = mocPlatformSpecific.Object;

            /*
             * Configure DataStore
             */
            DataStore.Instance.CN = iocCommonNotifications;
            DataStore.Instance.ES = iocXamarinEssentials;
            DataStore.Instance.FFIL = iocFFImageLoading;

            /*
            * Other setup
            */
            IStoreXML iocExternalStorage = new StoreXML(iocCommonLogging);

            IStorePostLoad iocGrampsStorePostLoad = new StorePostLoad(iocCommonLogging, iocEventAggregator, iocPlatformSpecific);

            IGrampsStoreSerial iocGrampsStoreSerial = new GrampsStoreSerial(iocCommonLogging);

            iocStoreFile = new StoreFile();

            newManager = new DataRepositoryManager(iocCommonLogging, iocEventAggregator, iocExternalStorage, iocGrampsStorePostLoad, iocGrampsStoreSerial, iocStoreFile);
        }
    }
}