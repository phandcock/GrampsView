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

    using NUnit.Framework;

    using Prism.Events;

    public static class e2e_test
    {
        public static void doTest()
        {
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

            FileInfoEx GrampsFile = StoreFolder.FolderGetFile(CommonConstants.StorageGRAMPSFileName);

            if (GrampsFile.Valid)
            {
                iocStoreFile.DecompressGZIP(GrampsFile);
            }

            Assert.True(DataStore.Instance.AD.CurrentInputStream != null);
        }
    }
}