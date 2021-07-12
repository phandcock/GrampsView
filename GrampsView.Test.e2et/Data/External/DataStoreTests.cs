namespace GrampsView.Data.External.Tests
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

        [Test()]
        public void DataStoreCreate_Basic()
        {
            GeneralData.iocCommonLogging.RoutineEntry(DataStore.Instance.AD.CurrentDataFolder.Path);

            Assert.True(DataStore.Instance.AD.CurrentDataFolder.Valid);
        }

        [SetUp]
        public void Init()
        {
            GeneralData.setupMocks();

            DataStoreUtility.DataStoreSetup();
        }
    }
}