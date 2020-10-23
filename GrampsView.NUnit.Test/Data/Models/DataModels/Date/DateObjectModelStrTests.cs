

namespace GrampsView.Data.Model.Tests
{
    using global::NUnit.Framework;

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

            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[0], "Date Type:", "String")) { Assert.Fail(); return; }
            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[1], "Date:", "1939")) { Assert.Fail(); return; }
            if (!CardListLineUtils.CheckCardListLine(AsCardListLineTest_Basic[2], "Val:", "1939")) { Assert.Fail(); return; }
         

            Assert.True(AsCardListLineTest_Basic.Count == 3);

         
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