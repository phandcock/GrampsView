using GrampsView.Common;

using NUnit.Framework;

namespace GrampsView.Data.Model.Tests
{
    [TestFixture()]
    public class DateObjectModelRangeTests
    {
        [Test()]
        public void AsCardListLineTest()
        {
            Assert.Fail();
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

            DateObjectModelRange testVal = new DateObjectModelRange(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);

            Assert.True(testVal.Valid);
        }
    }
}