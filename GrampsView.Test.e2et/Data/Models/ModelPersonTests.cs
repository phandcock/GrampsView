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

        /// <summary>
        /// Basic person model test - Models are invalid to start
        /// </summary>
        [Test()]
        public void ModelPerson_Basic()
        {
            DataStoreUtility.DataStoreSetup();

            DataStoreUtility.LoadTestFile();

            PersonModel tt = DV.PersonDV.GetModelFromId("I0000");

            Assert.True(tt.Valid, "Invalid person");
        }

        /// <summary>
        /// Basic person model test - check Priv default
        /// </summary>
        [Test()]
        public void ModelPerson_Basic_Priv_Default()
        {
            DataStoreUtility.DataStoreSetup();

            DataStoreUtility.LoadTestFile();

            PersonModel tt = DV.PersonDV.GetModelFromId("I0000");

            Assert.True(tt.Valid, "Invalid person");

            Assert.False(tt.Priv, "Invalid Priv");
        }

        /// <summary>
        /// Basic person model test - check Priv true
        /// </summary>
        [Test()]
        public void ModelPerson_Basic_Priv_True()
        {
            DataStoreUtility.DataStoreSetup();

            DataStoreUtility.LoadTestFile();

            PersonModel tt = DV.PersonDV.GetModelFromId("I0002");

            Assert.True(tt.Valid, "Invalid person");

            Assert.True(tt.Priv, "Invalid Priv");
        }

        /// <summary>
        /// Basic person model test - Make model valid first
        /// </summary>
        [Test()]
        public void ModelPerson_Basic_Valid()
        {
            DataStoreUtility.DataStoreSetup();

            DataStoreUtility.LoadTestFile();

            PersonModel tt = DV.PersonDV.GetModelFromId("I0000");

            Assert.True(tt.Valid, "Invalid person");
        }

        /// <summary>
        /// Person model test
        /// </summary>
        [Test()]
        public void ModelPerson_Notes()
        {
            DataStoreUtility.DataStoreSetup();

            DataStoreUtility.LoadTestFile();

            PersonModel tt = DV.PersonDV.GetModelFromId("I0003");

            Assert.True(tt.Valid, "Invalid person");

            Assert.True(tt.GNoteRefCollection.Count == 2, "Wrong notes count");

            HLinkNoteModel noteModel = tt.GNoteRefCollection.GetFirstOfType(Common.Constants.NoteTypeBiography);

            Assert.True(noteModel.DeRef.Id == "N0000", "Wrong Person Model Notes Bio found");

            Assert.True(noteModel.DeRef.GType == Common.Constants.NoteTypeBiography, "Wrong Person Model Notes Type found");

            Assert.True(noteModel.DeRef.GStyledText.GText == "Biography Note.\n", "Wrong Person Model Notes Text found");
        }
    }
}