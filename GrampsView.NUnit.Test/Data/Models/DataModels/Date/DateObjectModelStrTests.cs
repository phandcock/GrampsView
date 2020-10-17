using GrampsView.Common;

using NUnit.Framework;

namespace GrampsView.Data.Model.Tests
{
    [TestFixture()]
    public class DateObjectModelStrTests
    {
        [Test()]
        public void AsCardListLineTest()
        {
            Assert.Fail();
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