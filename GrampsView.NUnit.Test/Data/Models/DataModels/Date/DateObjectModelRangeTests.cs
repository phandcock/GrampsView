namespace GrampsView.Data.Model.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Common;
    using GrampsView.NUnit.Test.Utility;

    [TestFixture()]
    public class DateObjectModelRangeTests
    {
        private DateObjectModelRange testVal;

        [Test()]
        public void AsCardListLineTest_Basic()
        {
            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title") { Assert.Fail(); return; }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "From 1939 to 1948");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Start:", "1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[2], "Stop:", "1948");

            Assert.True(AsCardListLineTest_Basic.Count == 3);
        }

        [Test()]
        public void AsCardListLineTest_DualDated()
        {
            string aCFormat = null;
            bool aDualDated = true;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939";
            string aStop = "1948";

            testVal = new DateObjectModelRange(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);

            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title") { Assert.Fail(); return; }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "From 1939 to 1948 (Dual dated)");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Start:", "1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[2], "Stop:", "1948");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[3], "Dual Dated:", "True");

            Assert.True(AsCardListLineTest_Basic.Count == 4);
        }

        [TearDown]
        public void Cleanup()
        {
        }

        [Test()]
        public void DateObjectModelRange_Basic()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939";
            string aStop = "1948";

            DateObjectModelRange DateObjectModelRange_Basic = new DateObjectModelRange(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);

            Assert.True(DateObjectModelRange_Basic.Valid);
        }

        [SetUp]
        public void Init()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939";
            string aStop = "1948";

            testVal = new DateObjectModelRange(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);
        }
    }
}