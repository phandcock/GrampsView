using global::NUnit.Framework;

using GrampsView.Common;
using GrampsView.e2e.Test.Utility;
using GrampsView.Models.DataModels.Date;

using SharedSharp.Model;

namespace GrampsView.Data.Model.Tests
{
    [TestFixture()]
    public partial class DOMValTests
    {
        [Test()]
        public void AsCardListLineTest_After()
        {
            string? aCFormat = null;
            bool aDualDated = false;
            string? aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aVal = "1939";
            CommonEnums.DateValType aValType = CommonEnums.DateValType.after;

            testVal = new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, aValType);

            CardListLineCollection AsCardListLineTest_After = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_After.Title != "Test Title")
            {
                Assert.Fail();
                return;
            }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[0], "Date:", "after 1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[1], "Val:", "1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[2], "Type:", "after");

            Assert.True(AsCardListLineTest_After.Count == 3);
        }

        [Test()]
        public void AsCardListLineTest_Basic()
        {
            string? aCFormat = null;
            bool aDualDated = false;
            string? aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aVal = "1939";
            CommonEnums.DateValType aValType = CommonEnums.DateValType.unknown;

            testVal = new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, aValType);

            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title")
            {
                Assert.Fail();
                return;
            }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Val:", "1939");

            Assert.True(AsCardListLineTest_Basic.Count == 2);
        }

        [Test()]
        public void AsCardListLineTest_DualDated()
        {
            string? aCFormat = null;
            bool aDualDated = true;
            string? aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aVal = "1939";
            CommonEnums.DateValType aValType = CommonEnums.DateValType.unknown;

            testVal = new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, aValType);

            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title")
            { Assert.Fail(); return; }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "1939 (Dual dated)");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Val:", "1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[2], "Dual Dated:", "True");

            Assert.True(AsCardListLineTest_Basic.Count == 3);
        }

        [Test()]
        public void AsCardListLineYear()
        {
            InitYearOnly();

            CardListLineCollection AsCardListLineTest_After = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_After.Title != "Test Title")
            {
                Assert.Fail();
                return;
            }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[0], "Date:", "1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[1], "Val:", "1939");

            Assert.True(AsCardListLineTest_After.Count == 2);
        }

        [Test()]
        public void AsCardListLineYearMonth()
        {
            InitYearMonth();

            CardListLineCollection AsCardListLineTest_After = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_After.Title != "Test Title")
            {
                Assert.Fail();
                return;
            }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[0], "Date:", "Jan 1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[1], "Val:", "1939-01");

            Assert.True(AsCardListLineTest_After.Count == 2);
        }

        [Test()]
        public void AsCardListLineYearMonthDay()
        {
            InitYearMonthDay();

            CardListLineCollection AsCardListLineTest_After = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_After.Title != "Test Title")
            {
                Assert.Fail();
                return;
            }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[0], "Date:", "1 Oct 1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_After[1], "Val:", "1939-10-01");

            Assert.True(AsCardListLineTest_After.Count == 2);
        }
    }
}