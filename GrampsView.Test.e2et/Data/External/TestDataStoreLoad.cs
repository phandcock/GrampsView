namespace GrampsView.Data.External.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Data.Repository;
    using GrampsView.e2e.Test.Utility;
    using GrampsView.Test.e2e.Utility;

    using System.Reflection;
    using System.Threading.Tasks;

    [TestFixture()]
    public class DataStoreLoadTests
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
        public async Task TestDataStoreLoad_Gpkg()
        {
            DataStoreUtility.ListEmbeddedResources();

            // Load Resource
            Assembly assemblyExec = Assembly.GetExecutingAssembly();
            string resourceName = DataStoreUtility.BasePath + ".Test_Data.GrampsView Test Basic.gpkg";

            DataStore.Instance.AD.CurrentInputStream = assemblyExec.GetManifestResourceStream(resourceName);

            DataStore.Instance.AD.CurrentInputStreamPath = "Test Data/Test_Data.GrampsView Test Basic.gpkg";

            e2e_test.TestDecompressTar();

            e2e_test.TestDecompressGzip();

            await e2e_test.TestGrampsUnzip();
        }

        [Test()]
        public async Task TestDataStoreLoad_Gramps()
        {
            DataStoreUtility.ListEmbeddedResources();

            // Load Resource
            Assembly assemblyExec = Assembly.GetExecutingAssembly();
            string resourceName = DataStoreUtility.BasePath + ".Test_Data.example.gramps";

            DataStore.Instance.AD.CurrentInputStream = assemblyExec.GetManifestResourceStream(resourceName);

            DataStore.Instance.AD.CurrentInputStreamPath = "Test Data/Test_Data.example.gramps";

            GeneralData.GrampsFile = new FileInfoEx();  // Mark as invalid as do not need to unzip

            await e2e_test.TestGrampsUnzip();
        }
    }
}