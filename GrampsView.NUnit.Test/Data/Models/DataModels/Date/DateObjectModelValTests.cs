namespace GrampsView.Data.Model.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Common;
    using GrampsView.NUnit.Test.Utility;

    [TestFixture()]
    public class DateObjectModelValTests
    {
        private DateObjectModelVal testVal;

        [Test()]
        public void AsCardListLineTest_After()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aVal = "1939";
            CommonEnums.DateValType aValType = CommonEnums.DateValType.after;

            testVal = new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, aValType);

            CardListLineCollection AsCardListLineTest_After = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_After.Title != "Test Title") { Assert.Fail(); return; }

            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[0], "Date:", "after 1939")) { Assert.Fail("Should be 'Date after 1939'"); return; }
            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[1], "Val:", "1939")) { Assert.Fail(); return; }
            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[2], "Type:", "after")) { Assert.Fail(); return; }

            Assert.True(AsCardListLineTest_After.Count == 3);
        }

        [Test()]
        public void AsCardListLineTest_Basic()
        {
            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title") { Assert.Fail(); return; }

            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "1939")) { Assert.Fail(); return; }
            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Val:", "1939")) { Assert.Fail(); return; }

            Assert.True(AsCardListLineTest_Basic.Count == 2);
        }

        [TearDown]
        public void Cleanup()
        {
        }

        [Test()]
        public void DateObjectModelVal_Basic()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            CommonEnums.DateValType aValType = CommonEnums.DateValType.unknown;
            string aVal = "1970";

            DateObjectModelVal testVal = new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, aValType);

            Assert.True(testVal.Valid);
        }

        [SetUp]
        public void Init()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aVal = "1939";
            CommonEnums.DateValType aValType = CommonEnums.DateValType.unknown;

            testVal = new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, aValType);
        }
    }
}