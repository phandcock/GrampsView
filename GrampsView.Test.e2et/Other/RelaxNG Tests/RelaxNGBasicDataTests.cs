namespace GrampsView.Other.Tests
{
    using Commons.Xml.Relaxng;

    using global::NUnit.Framework;

    using GrampsView.Common;
    using GrampsView.Data.Repository;
    using GrampsView.e2e.Test.Utility;

    using System.IO;
    using System.Reflection;
    using System.Xml;

    [TestFixture()]
    public class RelaxNGBasicDataTests
    {
        public System.IO.Stream rngStream;

        [TearDown]
        public void Cleanup()
        {
        }

        [SetUp]
        public void Init()
        {
            DataStoreUtility.DataStoreSetup();

            // Load Resource
            DataStoreUtility.ListEmbeddedResources();

            var assemblyExec = Assembly.GetExecutingAssembly();
            var resourceName = DataStoreUtility.BasePath + ".Test_Data.grampsxml.171.rng";

            rngStream = assemblyExec.GetManifestResourceStream(resourceName);
        }

        [Test()]
        public void RelaxNGBasicDataTests_Basic()
        {
            DataStoreUtility.DataStoreSetup();

            string filePath = Path.Combine(DataStore.Instance.AD.CurrentDataFolder.Path, CommonConstants.StorageXMLFileName);

            XmlReader instance = new XmlTextReader(filePath);

            XmlReader grammar = new XmlTextReader(rngStream);

            RelaxngValidatingReader reader =
                new RelaxngValidatingReader(instance, grammar);

            while (reader.Read())
            {
                // TODO parse based on NodeType
            }
        }
    }
}