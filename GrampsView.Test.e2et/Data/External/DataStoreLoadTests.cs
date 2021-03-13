namespace GrampsView.Data.External.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.External.StoreSerial;
    using GrampsView.Data.ExternalStorage;
    using GrampsView.Data.Repository;
    using GrampsView.e2e.Test.Utility;
    using GrampsView.Events;

    using Moq;

    using Prism.Events;

    using System.Reflection;

    [TestFixture()]
    public class DataStoreLoadTests
    {
        [TearDown]
        public void Cleanup()
        {
        }

        [Test()]
        public void DataStoreLoad_Basic()
        {
            DataStoreUtility.ListEmbeddedResources();

            // Load Resource
            var assemblyExec = Assembly.GetExecutingAssembly();
            var resourceName = DataStoreUtility.BasePath + ".Test_Data.Basic.gpkg";

            DataStore.Instance.AD.CurrentInputStream = assemblyExec.GetManifestResourceStream(resourceName);

            DataStore.Instance.AD.CurrentInputStreamPath = "Test Data/Basic.gpkg";

            // Remove the old dateTime stamps so the files get reloaded even if they have been seen
            // before TODO CommonLocalSettings.SetReloadDatabase();

            ICommonLogging iocCommonLogging = new CommonLogging();

            ////////////////
            Mock<ICommonNotifications> mockCommonNotifications = new Mock<ICommonNotifications>();

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

            // newManager.StartDataLoad();

            iocStoreFile.DecompressTAR();

            FileInfoEx GrampsFile = StoreFolder.FolderGetFile(DataStore.Instance.AD.CurrentDataFolder, CommonConstants.StorageGRAMPSFileName);

            if (GrampsFile.Valid)
            {
                iocStoreFile.DecompressGZIP(GrampsFile);
            }

            Assert.True(DataStore.Instance.AD.CurrentInputStream != null);
        }

        [SetUp]
        public void Init()
        {
            DataStoreUtility.DataStoreSetup();
        }
    }
}