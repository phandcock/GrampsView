namespace GrampsView.Data.Model.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Common;
    using GrampsView.NUnit.Test.Utility;

    [TestFixture()]
    public class DateObjectModelSpanTests
    {
        private DateObjectModelSpan testVal;

        [Test()]
        public void AsCardListLineTest()
        {
            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title") { Assert.Fail(); return; }

            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date Type:", "Span")) { Assert.Fail(); return; }
            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Date:", "1939 to 1948")) { Assert.Fail(); return; }
            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[2], "Start:", "1939")) { Assert.Fail(); return; }
            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[3], "Stop:", "1948")) { Assert.Fail(); return; }

            Assert.True(AsCardListLineTest_Basic.Count == 4);
        }

        [TearDown]
        public void Cleanup()
        {
        }

        [Test()]
        public void DateObjectModelSpan_Basic()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939";
            string aStop = "1948";

            DateObjectModelSpan testVal = new DateObjectModelSpan(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);

            Assert.True(testVal.Valid);
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

            testVal = new DateObjectModelSpan(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);
        }
    }
}