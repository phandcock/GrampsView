namespace GrampsView.Data.External.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Data.Repository;
    using GrampsView.NUnit.Test.Utility;

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
            var resourceName = "GrampsView.NUnit.Test.Test_Data.Basic.gpkg";

            DataStore.Instance.AD.CurrentInputStream = assemblyExec.GetManifestResourceStream(resourceName);

            DataStore.Instance.AD.CurrentInputStreamPath = "Test Data/Basic.gpkg";

            // Remove the old dateTime stamps so the files get reloaded even if they have been seen
            // before TODO CommonLocalSettings.SetReloadDatabase();

            // DataRepositoryManager.StartDataLoad();

            Assert.True(DataStore.Instance.AD.CurrentInputStream != null);
        }

        [SetUp]
        public void Init()
        {
            DataStoreUtility.DataStoreSetup();
        }
    }
}