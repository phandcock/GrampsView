using GrampsView.Common;
using GrampsView.Data.Model;

using NUnit.Framework;

using System;
using System.Collections.Generic;

namespace GrampsView.NUnit.Data.Models.DateModels
{
    public class DateModel
    {
        /// <summary>
        /// Gets the test date data. Format for entries is:
        /// - Type
        /// - CFormat
        /// - DualDated
        /// - New Year
        /// - Quality
        /// - Start
        /// - Stop
        /// - Val value
        ///
        /// Format for tests
        /// - Notional Date
        /// - Short Date
        /// - Long Date
        /// - Age
        /// - Type string
        /// - Notional Date Type
        /// - Single Date
        /// - Sort Date.
        /// </summary>
        /// <value>
        /// The test date data.
        /// </value>
        public static IEnumerable<TestDateData> TestDataDateNotional =>
        new List<TestDateData>
              {
                // TODO Test description
                new TestDateData { aType= "dateval1", aCFormat ="cFormat",aDualDated= false,  aNewYear= "New Year", aQuality= "Quality",aStart=  "Start2", aStop="Stop2",aVal= "1975-10-01", argYear=1975, argMonth=10, argDay=1 },
                new TestDateData { aType= "dateval1", aCFormat ="cFormat",aDualDated= false,  aNewYear= "New Year", aQuality= "Quality",aStart=  "Start2", aStop="Stop2",aVal= "1975-10-01", argYear=1975, argMonth=10, argDay=1 },
                new TestDateData { aType= "dateval1", aCFormat ="cFormat",aDualDated= false,  aNewYear= "New Year", aQuality= "Quality",aStart=  "Start2", aStop="Stop2",aVal= "1975-10-01", argYear=1975, argMonth=10, argDay=1 },
              };

        [SetUp]
        public void Setup()
        {
        }

        public void TestDateNotional(string aType, string aCFormat, bool aDualDated, string aNewYear, string aQuality, string aStart, string aStop, string aVal, int argYear, int argMonth, int argDay)
        {
            DateObjectModel testDate = new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, CommonEnums.DateValType.unknown);

            Assert.False(testDate == null);

            Assert.True(testDate.NotionalDate == new DateTime(argYear, argMonth, argDay));
        }

        [Test]
        public void TestDates()
        {
            foreach (TestDateData test in TestDataDateNotional)
            {
                TestDateNotional(test.aType, test.aCFormat, test.aDualDated, test.aNewYear, test.aQuality, test.aStart, test.aStop, test.aVal,  test.argYear, test.argMonth, test.argDay);

            }

            Assert.Pass();
        }

        /// <summary>
        /// Tests the date notional date.
        /// </summary>
        /// <param name="aType">
        /// a type.
        /// </param>
        /// <param name="aCFormat">
        /// a c format.
        /// </param>
        /// <param name="aDualDated">
        /// a dual dated.
        /// </param>
        /// <param name="aNewYear">
        /// a new year.
        /// </param>
        /// <param name="aQuality">
        /// a quality.
        /// </param>
        /// <param name="aStart">
        /// a start.
        /// </param>
        /// <param name="aStop">
        /// a stop.
        /// </param>
        /// <param name="aVal">
        /// a value.
        /// </param>
        /// <param name="argYear">
        /// The argument year.
        /// </param>
        /// <param name="argMonth">
        /// The argument month.
        /// </param>
        /// <param name="argDay">
        /// The argument day.
        /// </param>
        ///// <summary>
        ///// Builds the date.
        ///// </summary>
        ///// <param name="aType">
        ///// a type.
        ///// </param>
        ///// <param name="aCFormat">
        ///// a c format.
        ///// </param>
        ///// <param name="aDualDated">
        ///// a dual dated.
        ///// </param>
        ///// <param name="aNewYear">
        ///// a new year.
        ///// </param>
        ///// <param name="aQuality">
        ///// a quality.
        ///// </param>
        ///// <param name="aStart">
        ///// a start.
        ///// </param>
        ///// <param name="aStop">
        ///// a stop.
        ///// </param>
        ///// <param name="aVal">
        ///// a value.
        ///// </param>
        ///// <returns>
        ///// </returns>
        // private DateObjectModel BuildDate(string aType, string aCFormat, bool aDualDated, string
        // aNewYear, string aQuality, string aStart, string aStop, string aVal) { DateObjectModel
        // returnDate = new DateObjectModelVal(aCFormat, aDualDated, aNewYear, aQuality, aStart,
        // aStop, aVal, null);

        // return returnDate; }
    }
}