namespace GrampsView.Data.External.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Data.Repository;
    using GrampsView.e2e.Test.Utility;
    using GrampsView.Test.e2e.Utility;

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
            var resourceName = DataStoreUtility.BasePath + ".Test_Data.GrampsView Test Basic.gpkg";

            DataStore.Instance.AD.CurrentInputStream = assemblyExec.GetManifestResourceStream(resourceName);

            DataStore.Instance.AD.CurrentInputStreamPath = "Test Data/Test_Data.GrampsView Test Basic.gpkg";

            e2e_test.doTest();
        }

        [Test()]
        public void DataStoreLoad_Gramps()
        {
            DataStoreUtility.ListEmbeddedResources();

            // Load Resource
            var assemblyExec = Assembly.GetExecutingAssembly();
            var resourceName = DataStoreUtility.BasePath + ".Test_Data.example.gramps";

            DataStore.Instance.AD.CurrentInputStream = assemblyExec.GetManifestResourceStream(resourceName);

            DataStore.Instance.AD.CurrentInputStreamPath = "Test Data/Test_Data.example.gramps";

            e2e_test.doTest();
        }

        [SetUp]
        public void Init()
        {
            DataStoreUtility.DataStoreSetup();
        }
    }
}