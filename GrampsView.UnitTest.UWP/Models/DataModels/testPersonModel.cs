// <copyright file="testPersonModel.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsViewXUnit.Data.Models.DataModels
{
    using System;

    using GrampsView.Data.Model;

 
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
            string aCFormat = string.Empty;
            bool aDualDated = false;
            string aNewYear = string.Empty;
            string aQuality = string.Empty;
            string aStart = string.Empty;
            string aStop = string.Empty;
            string aVal = DateTime.Now.AddYears(-17).ToUniversalTime().ToString("r"); // To Universal time like Gramps uses.
            string aValType = string.Empty;

            //DateObjectModelVal birthdatemodel = new DateObjectModelVal(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop, aVal, aValType)
            //{
            //};

            PersonModel testPerson = new PersonModel
            {
                //BirthDate = birthdatemodel,
            };

            // Test assigned correctly
            //Assert.True(testPerson.BirthDate == birthdatemodel);

            // Test Age is correct
            //Assert.True(testPerson.BirthDate.GetAge == 17);
        }
    }
}