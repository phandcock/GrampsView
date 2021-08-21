namespace GrampsView.Data.Model.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Common;

    [TestFixture()]
    public partial class DateObjectModelSpanTests
    {
        // TODO Add more tests and add the same to other dateobjectmodel types

        private DateObjectModelSpan testVal;

        [TearDown]
        public void Cleanup()
        {
        }

        [SetUp]
        public void Init()
        {
        }

        public void InitYearMonth()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939-01";
            string aStop = "1948-10";

            testVal = new DateObjectModelSpan(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);
        }

        public void InitYearMonthDay()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939-01-01";
            string aStop = "1948-10-11";

            testVal = new DateObjectModelSpan(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);
        }

        public void InitYearOnly()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939";
            string aStop = "1948";

            testVal = new DateObjectModelSpan(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);
        }
    }
}