namespace GrampsView.Data.External.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Data.Repository;
    using GrampsView.e2e.Test.Utility;

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
            DataStoreUtility.DataStoreSetup();

            Assert.True(DataStore.Instance.AD.CurrentDataFolder.Path == DataStoreUtility.DataStorePath);
        }

        [SetUp]
        public void Init()
        {
        }
    }
}