

namespace GrampsView.Data.Model.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Common;
    using GrampsView.NUnit.Test.Utility;

    [TestFixture()]
    public class DateObjectModelStrTests
    {
        private DateObjectModelStr testVal;

        [Test()]
        public void AsCardListLineTest()
        {
            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (AsCardListLineTest_Basic.Title != "Test Title") { Assert.Fail(); return; }

            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date Type:", "Range")) { Assert.Fail(); return; }
            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Notional Date:", "Range from 1939 to 1948")) { Assert.Fail(); return; }
            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[2], "Start:", "1939")) { Assert.Fail(); return; }
            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[3], "Stop:", "1948")) { Assert.Fail(); return; }
         

            Assert.True(AsCardListLineTest_Basic.Count == 4);

            Assert.Pass();
        }

        [TearDown]
        public void Cleanup()
        {
        }


        [SetUp]
        public void Init()
        {
       
            string aVal = "1939";
        

            testVal = new DateObjectModelStr(aVal);

        }

        [Test()]
        public void DateObjectModelStr_Basic()
        {
     
            string aVal = "1948";

            DateObjectModelStr testVal  = new DateObjectModelStr(aVal);

            Assert.True(testVal.Valid);
        }
    }
}