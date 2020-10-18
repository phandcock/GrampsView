using GrampsView.Common;
using GrampsView.NUnit.Test.Utility;

using NUnit.Framework;

namespace GrampsView.Data.Model.Tests
{
    [TestFixture()]
    public class DateObjectModelRangeTests
    {
        private DateObjectModelRange testVal;

        [Test()]
        public void AsCardListLineTest_Basic()
        {
            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title") { Assert.Fail(); return; }

            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date Type:", "Range")) { Assert.Fail(); return; }
            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Date:", "Range from 1939 to 1948")) { Assert.Fail(); return; }
            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[2], "Start:", "1939")) { Assert.Fail(); return; }
            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[3], "Stop:", "1948")) { Assert.Fail(); return; }
            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[4], "Dual Dated:", testVal.GDualdated.ToString())) { Assert.Fail(); return; }

            Assert.True(AsCardListLineTest_Basic.Count == 5);

            Assert.Pass();
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