// <copyright file="testDateObjectModelStrings.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsViewXUnit.Data.Models.DataModels
{
    using System;
    using System.Collections.Generic;

    using GrampsView.Data.Model;




    /// <summary>
    /// </summary>
    public class TestDateObjectModelStrings
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
        public static IEnumerable<object[]> TestDataDateNotional =>
              new List<object[]>
              {
                  // TODO Test description
                  new object[] { "dateval1", "cFormat", false,   "New Year", "Quality", "Start2", "Stop2", "1975-10-01", 1975, 10, 1 },
                  new object[] { "dateval2", "cFormat", false,   "New Year", "Quality", "Start", "Stop1", "1975-10-02", 1975, 10, 2 },
                  new object[] { "dateval3", "cFormat", false,   "New Year", "Quality", "Start", "Stop", "1975-10-03", 1975, 10, 3 },
              };

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
        //[Theory]
        //[MemberData(nameof(TestDataDateNotional))]
        public void TestDateNotional(string aType, string aCFormat, bool aDualDated, string aNewYear, string aQuality, string aStart, string aStop, string aVal, int argYear, int argMonth, int argDay)
        {
            //DateObjectModel testDate = new DateObjectModelVal(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop, aVal, null);

            //Assert.False(testDate == null);

            //Assert.True(testDate.NotionalDate == new DateTime(argYear, argMonth, argDay));
        }

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
        // private DateObjectModel BuildDate(string aType, string aCFormat, bool aDualDated, string aNewYear, string aQuality, string aStart, string aStop, string aVal)
        // {
        //    DateObjectModel returnDate = new DateObjectModelVal(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop, aVal, null);

        // return returnDate;
        // }
    }
}