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

        [SetUp]
        public void Init()
        {
        }

        [Test()]
        public void TestModelPerson_Basic()
        {
            DataStoreUtility.DataStoreSetup();

            DataStoreUtility.LoadTestFile();

            PersonModel tt = DV.PersonDV.GetModelFromId("I0000");

            if (tt.Valid)
            {
                Assert.True(tt.Valid, "Invalid person");
            }
        }
    }
}