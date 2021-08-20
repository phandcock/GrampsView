namespace GrampsView.Data.Model.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Common;
    using GrampsView.e2e.Test.Utility;

    [TestFixture()]
    public class DateObjectModelSpanTests
    {
        // TODO Add more tests and add the same to other dateobjectmodel types

        private DateObjectModelSpan testVal;

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
        }

        public void InitYearMonth()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939-01";
            string aStop = "1948-10";

            testVal = new DateObjectModelSpan(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);
        }

        public void InitYearMonthDay()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939-01-01";
            string aStop = "1948-10-11";

            testVal = new DateObjectModelSpan(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);
        }

        public void InitYearOnly()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939";
            string aStop = "1948";

            testVal = new DateObjectModelSpan(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);
        }

        [Test()]
        public void TestAsCardListLineYear()
        {
            InitYearOnly();

            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title")
            {
                Assert.Fail();
                return;
            }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "1939 to 1948");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Start:", "1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[2], "Stop:", "1948");

            Assert.True(AsCardListLineTest_Basic.Count == 3);
        }

        [Test()]
        public void TestAsCardListLineYearMonth()
        {
            InitYearMonth();

            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title")
            {
                Assert.Fail();
                return;
            }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "Jan1939 to Oct1948");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Start:", "Jan1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[2], "Stop:", "Oct1948");

            Assert.True(AsCardListLineTest_Basic.Count == 3);
        }

        [Test()]
        public void TestAsCardListLineYearMonthDay()
        {
            InitYearMonthDay();

            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title")
            {
                Assert.Fail();
                return;
            }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "01Jan1939 to 11Oct1948");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Start:", "01Jan1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[2], "Stop:", "11Oct1948");

            Assert.True(AsCardListLineTest_Basic.Count == 3);
        }
    }
}