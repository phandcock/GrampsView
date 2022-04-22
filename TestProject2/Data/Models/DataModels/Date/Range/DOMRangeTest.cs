namespace GrampsView.Data.Model.Tests
{
    using GrampsView.Common;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class DateObjectModelRangeTests
    {
        // TODO Add more tests and add the same to other dateobjectmodel types

        private DateObjectModelRange testVal;

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod]
        public void DateObjectModelRange_Basic()
        {
            InitYearOnly();

            Assert.IsTrue(testVal.Valid);
        }

        //[TestInitialize]
        //public void Init()
        //{
        //    string aCFormat = null;
        //    bool aDualDated = false;
        //    string aNewYear = null;
        //    CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
        //    string aStart = "1939";
        //    string aStop = "1948";

        //    testVal = new DateObjectModelRange(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);
        //}

        public void InitYearMonth()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939-01";
            string aStop = "1948-10";

            testVal = new DateObjectModelRange(aStart, aStop, aCFormat, aDualDated, aNewYear, aQuality);
        }

        public void InitYearMonthDay()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939-01-01";
            string aStop = "1948-10-11";

            testVal = new DateObjectModelRange(aStart, aStop, aCFormat, aDualDated, aNewYear, aQuality);
        }

        public void InitYearOnly()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939";
            string aStop = "1948";

            testVal = new DateObjectModelRange(aStart, aStop, aCFormat, aDualDated, aNewYear, aQuality);
        }
    }
}