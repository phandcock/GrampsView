using GrampsView.Common;

using NUnit.Framework;

namespace GrampsView.Data.Model.Tests
{
    [TestFixture()]
    public class DateObjectModelValTests
    {
        [Test()]
        public void AsCardListLineTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DateObjectModelVal_Basic()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            CommonEnums.DateValType aValType = CommonEnums.DateValType.unknown;
            string aVal = "1970";

            DateObjectModelVal testVal = new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, aValType);

            Assert.True(testVal.Valid);
        }
    }
}