namespace GrampsView.Data.Model.Tests
{
    using GrampsView.Common;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class DOMValTests
    {
        // TODO Add more tests and add the same to other dateobjectmodel types

        private DateObjectModelVal testVal;

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestInitialize]
        public void Init()
        {
        }

        public void InitYearMonth()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aVal = "1939-01";
            CommonEnums.DateValType aValType = CommonEnums.DateValType.unknown;

            testVal = new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, aValType);
        }

        public void InitYearMonthDay()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aVal = "1939-10-01";
            CommonEnums.DateValType aValType = CommonEnums.DateValType.unknown;

            testVal = new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, aValType);
        }

        public void InitYearOnly()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aVal = "1939";
            CommonEnums.DateValType aValType = CommonEnums.DateValType.unknown;

            testVal = new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, aValType);
        }
    }
}