namespace GrampsView.Data.External.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Data.Repository;
    using GrampsView.NUnit.Test.Utility;

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

            Assert.True(DataStore.Instance.AD.CurrentDataFolder.FullName == DataStoreUtility.DataStorePath);
        }

        [SetUp]
        public void Init()
        {
        }
    }
}