namespace GrampsView.Data.External.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.e2e.Test.Utility;

    [TestFixture()]
    public class ModelPersonTests
    {
        [TearDown]
        public void Cleanup()
        {
        }

        /// <summary>
        /// Basic person model test - Models are invalid to start
        /// </summary>
        [Test()]
        public void CreateModelPerson_Basic()
        {
            DataStoreUtility.DataStoreSetup();

            DataStoreUtility.LoadTestFile();

            PersonModel tt = DV.PersonDV.GetModelFromId("I0000");

            Assert.False(tt.Valid, "Invalid person");
        }

        /// <summary>
        /// Basic person model test - Make model valid first
        /// </summary>
        [Test()]
        public void CreateModelPerson_Basic_Valid()
        {
            DataStoreUtility.DataStoreSetup();

            DataStoreUtility.LoadTestFile();

            PersonModel tt = DV.PersonDV.GetModelFromId("I0000");

            Assert.False(tt.Valid, "Invalid person");
        }

        [SetUp]
        public void Init()
        {
        }
    }
}