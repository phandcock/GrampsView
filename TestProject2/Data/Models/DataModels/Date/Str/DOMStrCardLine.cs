namespace GrampsView.Data.Model.Tests
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using GrampsView.e2e.Test.Utility;

    using SharedSharp.Model;

   [TestClass]
    public partial class DOMStrTests
    {
      [TestMethod]
        public void AsCardListLineYear()
        {
            InitYearOnly();

            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title")
            {
                Assert.Fail();
                return;
            }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "1939");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Str:", "1939");

            Assert.IsTrue(AsCardListLineTest_Basic.Count == 2);
        }

      [TestMethod]
        public void AsCardListLineYearMonth()
        {
            InitYearMonth();

            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title")
            {
                Assert.Fail();
                return;
            }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "1939-01");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Str:", "1939-01");

            Assert.IsTrue(AsCardListLineTest_Basic.Count == 2);
        }

      [TestMethod]
        public void AsCardListLineYearMonthDay()
        {
            InitYearMonthDay();

            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title")
            {
                Assert.Fail();
                return;
            }

            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date:", "1939-01-01");
            CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Str:", "1939-01-01");

            Assert.IsTrue(AsCardListLineTest_Basic.Count == 2);
        }
    }
}