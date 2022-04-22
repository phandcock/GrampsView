namespace GrampsView.Data.Model.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using GrampsView.e2e.Test.Utility;

    using SharedSharp.Model;

   [TestClass]
    public partial class DateObjectModelSpanTests
    {
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

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "From 1939 to 1948");
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

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "From Jan 1939 to Oct 1948");
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

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "From 1 Jan 1939 to 11 Oct 1948");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Start:", "1Jan1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[2], "Stop:", "11Oct1948");

            Assert.IsTrue(AsCardListLineTest_Basic.Count == 3);
        }
    }
}