namespace GrampsView.Data.External.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.e2e.Test.Utility;

    [TestFixture()]
    public class ModelNoteTests
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
        public void ModelNote_Biography()
        {
            DataStoreUtility.LoadTestFile();

            NoteModel tt = DV.NoteDV.GetModelFromId("N0000");

            Assert.True(tt.Valid, "Invalid Note");

            Assert.True(tt.GType == Common.CommonConstants.NoteTypeBiography, "Invalid Note");
        }
    }
}