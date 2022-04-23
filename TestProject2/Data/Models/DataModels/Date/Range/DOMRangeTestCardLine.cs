﻿namespace GrampsView.Data.Model.Tests
{


    using GrampsView.Common;
    using GrampsView.e2e.Test.Utility;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SharedSharp.Model;

   [TestClass]
    public partial class DateObjectModelRangeTests
    {
      [TestMethod]
        public void AsCardListLineTest_DualDated()
        {
            string aCFormat = null;
            bool aDualDated = true;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939";
            string aStop = "1948";

            testVal = new DateObjectModelRange(aStart, aStop, aCFormat, aDualDated, aNewYear, aQuality);

            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title") { Assert.Fail(); return; }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "Between 1939 and 1948 (Dual dated)");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Start:", "1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[2], "Stop:", "1948");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[3], "Dual Dated:", "True");

            Assert.IsTrue(AsCardListLineTest_Basic.Count == 4);
        }

      [TestMethod]
        public void TestAsCardListLineYear()
        {
            InitYearOnly();

            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title")
            {
                Assert.Fail();
                return;
            }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "Between 1939 and 1948");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Start:", "1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[2], "Stop:", "1948");

            Assert.IsTrue(AsCardListLineTest_Basic.Count == 3);
        }

      [TestMethod]
        public void TestAsCardListLineYearMonth()
        {
            InitYearMonth();

            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title")
            {
                Assert.Fail();
                return;
            }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "Between Jan 1939 and Oct 1948");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Start:", "Jan1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[2], "Stop:", "Oct1948");

            Assert.IsTrue(AsCardListLineTest_Basic.Count == 3);
        }

      [TestMethod]
        public void TestAsCardListLineYearMonthDay()
        {
            InitYearMonthDay();

            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title")
            {
                Assert.Fail();
                return;
            }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "Between 1 Jan 1939 and 11 Oct 1948");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Start:", "1Jan1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[2], "Stop:", "11Oct1948");

            Assert.IsTrue(AsCardListLineTest_Basic.Count == 3);
        }
    }
}