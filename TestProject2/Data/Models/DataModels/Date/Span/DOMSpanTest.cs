﻿namespace GrampsView.Data.Model.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using GrampsView.Common;

    [TestClass]
    public partial class DateObjectModelSpanTests
    {
        // TODO Add more tests and add the same to other dateobjectmodel types

        private DateObjectModelSpan testVal;

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
            string aStart = "1939-01";
            string aStop = "1948-10";

            testVal = new DateObjectModelSpan(aStart, aStop, aCFormat, aDualDated, aNewYear, aQuality);
        }

        public void InitYearMonthDay()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939-01-01";
            string aStop = "1948-10-11";

            testVal = new DateObjectModelSpan(aStart, aStop, aCFormat, aDualDated, aNewYear, aQuality);
        }

        public void InitYearOnly()
        {
            string aCFormat = null;
            bool aDualDated = false;
            string aNewYear = null;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = "1939";
            string aStop = "1948";

            testVal = new DateObjectModelSpan(aStart, aStop, aCFormat, aDualDated, aNewYear, aQuality);
        }
    }
}