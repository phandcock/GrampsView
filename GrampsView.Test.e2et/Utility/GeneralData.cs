using CommunityToolkit.Mvvm.Messaging;

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data;
using GrampsView.Data.External.StoreSerial;
using GrampsView.Data.ExternalStorage;
using GrampsView.Data.Repository;
using GrampsView.Events;

using Moq;

using SharedSharp.Errors.Interfaces;

using System.Diagnostics;
using System.IO;
using System.Reflection;

using Xamarin.Forms;

namespace GrampsView.Test.e2e.Utility
{
    public static class GeneralData
    {
        public static IFileInfoEx GrampsFile = new FileInfoEx();
        public static SharedSharp.Logging.Interfaces.ILog iocCommonLogging = new SharedSharp.Logging.Log();
        public static IErrorNotifications? iocCommonNotifications;
        public static IStoreXML? iocExternalStorage;
        public static IStorePostLoad? iocGrampsStorePostLoad;
        public static IGrampsStoreSerial? iocGrampsStoreSerial;
        public static IPlatformSpecific? iocPlatformSpecific;
        public static IStoreFile? iocStoreFile;
        public static Mock<IMessenger> mocEventAggregator = new();
        public static Mock<IPlatformSpecific> mocPlatformSpecific = new();
        public static DataRepositoryManager? newManager;

        public static void DataStoreSetup()
        {
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void ListEmbeddedResources()
        {
            // ... // NOTE: use for debugging, not in released app code!
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (string res in assembly.GetManifestResourceNames())
            {
                Debug.WriteLine($"Found resource: {res} ? {ImageSource.FromResource(res, typeof(App)) != null}");
            }
        }

        public static void setupMocks()
        {
            /*
             * Mock Common Logging
             */
            SharedSharp.Logging.Interfaces.ILog iocCommonLogging = new SharedSharp.Logging.Log();

            /*
             * Mock Common Logging
             */
            Mock<IErrorNotifications> mockCommonNotifications = new();

            iocCommonNotifications = mockCommonNotifications.Object;

            /*
             * Mock Image Loading
             */
            Mock<IFFImageLoading> mocFFImageLoading = new();
            IFFImageLoading iocFFImageLoading = mocFFImageLoading.Object;

            /*
             * Mock Xamarin Essentials
             */
            Mock<Xamarin.Essentials.Interfaces.IFileSystem> mocXamarinEssentials = new();

            _ = mocXamarinEssentials
                .Setup(x => x.CacheDirectory)
                .Returns(Path.GetTempPath());

            Xamarin.Essentials.Interfaces.IFileSystem iocXamarinEssentials = mocXamarinEssentials.Object;

            /*
             * Mock Event Aggregator
             */
            Mock<DataLoadXMLEvent> mockedEventDataLoadXMLEvent = new();

            // TODO fix this

            //mocEventAggregator
            //      .Setup(x => x.Register<DataLoadXMLEvent>())
            //          .Returns(mockedEventDataLoadXMLEvent.Object);

            //Mock<DataLoadStartEvent> mockedEventDataLoadStartEvent = new Mock<DataLoadStartEvent>();
            //mocEventAggregator
            //      .Setup(x => x.GetEvent<DataLoadStartEvent>())
            //          .Returns(mockedEventDataLoadStartEvent.Object);

            //Mock<DataSaveSerialEvent> mockedEventDataSaveSerialEvent = new Mock<DataSaveSerialEvent>();
            //mocEventAggregator
            //    .Setup(x => x.GetEvent<DataSaveSerialEvent>())
            //        .Returns(mockedEventDataSaveSerialEvent.Object);

            //Mock<DataLoadCompleteEvent> mockedEventDataLoadCompleteEvent = new Mock<DataLoadCompleteEvent>();
            //mocEventAggregator
            //    .Setup(x => x.GetEvent<DataLoadCompleteEvent>())
            //        .Returns(mockedEventDataLoadCompleteEvent.Object);

            IMessenger iocEventAggregator = mocEventAggregator.Object;

            /*
             * Mock Platform specific
             */
            iocPlatformSpecific = mocPlatformSpecific.Object;

            /*
             * Configure DataStore
             */
            //DataStore.Instance.ES = iocXamarinEssentials;
            DataStore.Instance.FFIL = iocFFImageLoading;

            /*
             * Other setup
            */
            iocExternalStorage = new StoreXML(iocCommonLogging, iocCommonNotifications);

            iocGrampsStorePostLoad = new StorePostLoad(iocCommonLogging, iocCommonNotifications, iocEventAggregator);

            iocGrampsStoreSerial = new GrampsStoreSerial(iocCommonLogging);

            iocStoreFile = new StoreFile();

            newManager = new DataRepositoryManager(iocCommonLogging, iocCommonNotifications, iocEventAggregator, iocExternalStorage, iocGrampsStorePostLoad, iocGrampsStoreSerial, iocStoreFile);
        }
    }
}