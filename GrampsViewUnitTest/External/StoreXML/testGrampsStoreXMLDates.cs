// <copyright file="testGrampsStoreXMLDates.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GramsViewXUnit.Data.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    using GrampsView.Data.Model;

    using GrampsView.Common;
    using GrampsView.Data.ExternalStorageNS;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class TestGrampsStoreXMLDates
    {
        private static readonly XNamespace Ns = CommonConstants.GrampsXMLNameSpace;

        /// <summary>
        /// The test.
        /// </summary>
        //private readonly GrampsStoreXML test = new GrampsStoreXML(null, null, null, null);

        /// <summary>
        /// Gets the test date eval data.
        /// </summary>
        /// <value>
        /// The test date eval data.
        /// </value>
        public static IEnumerable<object[]> TestDateEvalData =>
                new List<object[]>
                {
                // Normal date
                    new object[]
                    {
                        new XElement (
                            "test",
                            new XElement (
                                Ns + "dateval",
                                new XAttribute("val", "1975-05-11"))),

                        1975,
                        5,
                        11,
                    },

                // Blank value
                    new object[]
                    {
                        new XElement(
                            "test",
                            new XElement (
                                    Ns + "dateval",
                                    new XAttribute("val", string.Empty))),
                        1,
                        1,
                        1,
                    },

                // Minimum date
                    new object[]
                    {
                     new XElement(
                         "test",
                         new XElement (
                                    Ns + "dateval",
                                    new XAttribute("val", "1901-01-01"))),
                     1901,
                     1,
                     1,
                    },

                // No date val
                    new object[]
                    {
                     new XElement(
                         "test",
                         new XElement(Ns + "dateval")),
                     1,
                     1,
                     1,
                    },
                };

        /// <summary>
        /// Tests the date eval age.
        /// </summary>
        //[Theory]
        //[MemberData(nameof(TestDateEvalData))]
        public void TestDateAge(XElement argXElement, int argYear, int argMonth, int argDay)
        {
            //DateObjectModel t = test.SetDate(argXElement);

            TimeSpan outDateSpan = DateTime.Now.Subtract(new DateTime(argYear, argMonth, argDay));

            //Assert.False(t == null);

            //Assert.True(t.GetAge == (new DateTime(1, 1, 1) + outDateSpan).Year);
        }

        /// <summary>
        /// Sets the date eval.
        /// </summary>
        /// <param name="argXElement">
        /// The argument x element.
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
        //[MemberData(nameof(TestDateEvalData))]
        public void TestDateBasic(XElement argXElement, int argYear, int argMonth, int argDay)
        {
            //DateObjectModel t = test.SetDate(argXElement);

            ////Assert.False(t == null);

            ////Assert.True(t.NotionalDate == new DateTime(argYear, argMonth, argDay));
        }

        /// <summary>
        /// Tests the date null age.
        /// </summary>
        //[Fact]
        public void TestDateNullAge()
        {
            // Setup BirthDate
            DateObjectModel birthdatemodel = new DateObjectModel
            {
                // NotionalDateType = DateObjectModel.DateType.NullDate, NotionalDate = DateTime.Now.AddYears(-17),
            };

            // Test Age is correct
            //Assert.True(birthdatemodel.GetAge == 0);
        }

        /// <summary>
        /// Sets the date overall.
        /// </summary>
        /// <param name="argXElement">
        /// The argument x element.
        /// </param>
        /// <param name="argDateObject">
        /// The argument date object.
        /// </param>
        //[Theory]
        //[InlineData("NotADate", null)]
        public void TestDateOverall(string argXElement, DateObjectModel argDateObject)
        {
            //DateObjectModel t = test.SetDate(new XElement(argXElement));

            //Assert.True(t == argDateObject);
        }

        /// <summary>
        /// Tests the date range age.
        /// </summary>
        //[Fact]
        public void TestDateRangeAge()
        {
            // Setup BirthDate
            DateObjectModel birthdatemodel = new DateObjectModel
            {
                // NotionalDateType = DateObjectModel.DateType.Daterange, NotionalDate = DateTime.Now.AddYears(-17),
            };

            // Test Age is correct
            //Assert.True(birthdatemodel.GetAge == 17);
        }

        /// <summary>
        /// Tests the date span age.
        /// </summary>
        //[Fact]
        public void TestDateSpanAge()
        {
            // Setup BirthDate
            DateObjectModel birthdatemodel = new DateObjectModel
            {
                // NotionalDateType = DateObjectModel.DateType.Datespan, NotionalDate = DateTime.Now.AddYears(-17),
            };

            // Test Age is correct
            //Assert.True(birthdatemodel.GetAge == 17);
        }

        /// <summary>
        /// Tests the date string age.
        /// </summary>
        //[Fact]
        public void TestDateStrAge()
        {
            // Setup BirthDate
            //DateObjectModelStr birthdatemodel = new DateObjectModelStr(null, false, null, null, null, null, "1975-10-01");

            // Test Age is correct
            int age = default(DateTime).Year - 1975;

            //Assert.True(birthdatemodel.GetAge == age);
        }
    }
}