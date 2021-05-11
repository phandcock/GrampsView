namespace GrampsView.Data.External.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.e2e.Test.Utility;

    using System.Collections.Generic;

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

            List<PersonModel> t = DV.PersonDV.GetAllAsModel();

            PersonModel tt = t[0];

            if (tt.Valid)
            {
                Assert.True(tt.Valid, "Invalid person");
            }
        }
    }
}