namespace GrampsView.Data.Model.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Common;
    using GrampsView.NUnit.Test.Utility;

    [TestFixture()]
    [Culture("en-AU")]
    public class DateObjectModelValTests
    {
        private DateObjectModelVal testValBasic;

        private DateObjectModelVal testValYearOnly;

        private DateObjectModelVal testValYearMonthDayOnly;

        private DateObjectModelVal testValYearMonthOnly;

        [Test()]
        public void AsCardListLineTest_After()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aVal = "1939";
            CommonEnums.DateValType aValType = CommonEnums.DateValType.after;

            testValBasic = new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, aValType);

            CardListLineCollection AsCardListLineTest_After = testValBasic.AsCardListLine("Test Title");

            if (AsCardListLineTest_After.Title != "Test Title") { Assert.Fail(); return; }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[0], "Date:", "after 1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[1], "Val:", "1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[2], "Type:", "after");

            Assert.True(AsCardListLineTest_After.Count == 3);
        }

        [Test()]
        public void AsCardListLineTest_Basic()
        {
            CardListLineCollection AsCardListLineTest_Basic = testValBasic.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title") { Assert.Fail(); return; }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Val:", "1939");

            Assert.True(AsCardListLineTest_Basic.Count == 2);
        }

        [Test()]
        public void AsCardListLineTest_YearMonthDayOnly()
        {
         

            CardListLineCollection AsCardListLineTest_After = testValYearMonthDayOnly.AsCardListLine("Test Title");

            if (AsCardListLineTest_After.Title != "Test Title") { Assert.Fail(); return; }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[0], "Date:", "after Wednesday, 3 May 1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[1], "Val:", "1939-05-03");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[2], "Type:", "after");

            Assert.True(AsCardListLineTest_After.Count == 3);
        }

        [Test()]
        public void AsCardListLineTest_YearMonthOnly()
        {
            CardListLineCollection AsCardListLineTest_After = testValYearMonthOnly.AsCardListLine("Test Title");

            if (AsCardListLineTest_After.Title != "Test Title") { Assert.Fail(); return; }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[0], "Date:", "after May 1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[1], "Val:", "1939-05");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[2], "Type:", "after");

            Assert.True(AsCardListLineTest_After.Count == 3);
        }

        [Test()]
        public void AsCardListLineTest_YearOnly()
        {
            CardListLineCollection AsCardListLineTest_After = testValYearOnly.AsCardListLine("Test Title");

            if (AsCardListLineTest_After.Title != "Test Title") { Assert.Fail(); return; }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[0], "Date:", "after 1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[1], "Val:", "1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[2], "Type:", "after");

            Assert.True(AsCardListLineTest_After.Count == 3);
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
            string aCFormat;
            bool aDualDated;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality;
            string aVal;
            CommonEnums.DateValType aValType;

            // Basic date
            aCFormat = null;
            aDualDated = false;
            aNewYear = null;
            aQuality = CommonEnums.DateQuality.unknown;
            aVal = "1939";
            aValType = CommonEnums.DateValType.unknown;

            testValBasic = new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, aValType);

            // Year Month Day Only
             aCFormat = null;
             aDualDated = false;
             aNewYear = null;
             aQuality = CommonEnums.DateQuality.unknown;
             aVal = "1939-05-03";
             aValType = CommonEnums.DateValType.after;

            testValYearMonthDayOnly = new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, aValType);

            // Year Month Only
            aCFormat = null;
            aDualDated = false;
            aNewYear = null;
            aQuality = CommonEnums.DateQuality.unknown;
            aVal = "1939-05";
            aValType = CommonEnums.DateValType.after;

            testValYearMonthOnly = new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, aValType);

            // Year Only
            aCFormat = null;
            aDualDated = false;
            aNewYear = null;
            aQuality = CommonEnums.DateQuality.unknown;
            aVal = "1939";
            aValType = CommonEnums.DateValType.after;

            testValYearOnly = new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, aValType);
        }
    }
}