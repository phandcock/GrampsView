using GrampsView.Common;

using NUnit.Framework;

namespace GrampsView.Data.Model.Tests
{
    [TestFixture()]
    public class DateObjectModelSpanTests
    {
        [Test()]
        public void AsCardListLineTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DateObjectModelSpan_Basic()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939";
            string aStop = "1948";

            DateObjectModelSpan testVal = new DateObjectModelSpan(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);

            Assert.True(testVal.Valid);
        }
    }
}