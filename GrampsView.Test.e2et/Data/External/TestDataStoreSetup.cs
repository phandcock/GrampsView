﻿namespace GrampsView.Data.External.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Data.Repository;
    using GrampsView.e2e.Test.Utility;
    using GrampsView.Test.e2e.Utility;

    [TestFixture()]
    public class DataStoreTests
    {
        [TearDown]
        public void Cleanup()
        {
        }

        [SetUp]
        public void Init()
        {
            GeneralData.setupMocks();

            DataStoreUtility.DataStoreSetup();
        }

        [Test()]
        public void TestDataStoreSetup_Basic()
        {
            GeneralData.iocCommonLogging.LogVariable("DataStoreCreate_Basic", DataStore.Instance.AD.CurrentDataFolder.Path);

            Assert.True(DataStore.Instance.AD.CurrentDataFolder.Valid);
        }
    }
}