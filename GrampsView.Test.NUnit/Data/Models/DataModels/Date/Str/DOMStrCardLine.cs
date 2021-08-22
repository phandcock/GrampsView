namespace GrampsView.Data.Model.Tests
{
    using global::NUnit.Framework;

    using GrampsView.e2e.Test.Utility;

    [TestFixture()]
    public partial class DOMStrTests
    {
        [Test()]
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

            Assert.True(AsCardListLineTest_Basic.Count == 2);
        }

        [Test()]
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

            Assert.True(AsCardListLineTest_Basic.Count == 2);
        }

        [Test()]
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

            Assert.True(AsCardListLineTest_Basic.Count == 2);
        }
    }
}