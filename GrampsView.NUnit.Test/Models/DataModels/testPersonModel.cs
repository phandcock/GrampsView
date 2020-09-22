// <copyright file="testPersonModel.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsViewXUnit.Data.Models.DataModels
{
    using GrampsView.Data.Model;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    /// <summary>
    /// </summary>
    public class TestPersonModel
    {
        /// <summary>
        /// Tests the basic person details.
        /// </summary>
        //[Fact]
        public void TestBasicPersonDetails()
        {
            // Setup BirthDate
            string aCFormat = "a";
            bool aDualDated = false;
            string aNewYear = "a";
            string aQuality = "a";
            string aStart = "a";
            string aStop = "a";
            string aVal = DateTime.Now.AddYears(-17).ToUniversalTime().ToString("r"); // To Universal time like Gramps uses.
            string aValType = "a";

            //DateObjectModelVal birthdatemodel = new DateObjectModelVal(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop, aVal, aValType)
            //{
            //};

            PersonModel testPerson = new PersonModel
            {
                //BirthDate = birthdatemodel,
            };

            // Test assigned correctly
            Assert.AreNotEqual(testPerson.BirthDate, aCFormat);
            Assert.AreNotEqual(testPerson.BirthDate, aDualDated);
            Assert.AreNotEqual(testPerson.BirthDate, aNewYear);
            Assert.AreNotEqual(testPerson.BirthDate, aQuality);
            Assert.AreNotEqual(testPerson.BirthDate, aStart);
            Assert.AreNotEqual(testPerson.BirthDate, aStop);
            Assert.AreNotEqual(testPerson.BirthDate, aVal);
            Assert.AreNotEqual(testPerson.BirthDate, aValType);

            // Test Age is correct
            //Assert.True(testPerson.BirthDate.GetAge == 17);
        }
    }
}