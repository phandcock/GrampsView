using GrampsView.Data.Repository;
using GrampsView.e2e.Test.Utility;
using GrampsView.Test.e2e.Utility;

using NUnit.Framework;

namespace GrampsView.Test.e2e.Data.External
{
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
            GeneralData.iocCommonLogging.Variable("DataStoreCreate_Basic", DataStore.Instance.AD.CurrentDataFolder.Path);

            Assert.True(DataStore.Instance.AD.CurrentDataFolder.Valid);
        }
    }
}