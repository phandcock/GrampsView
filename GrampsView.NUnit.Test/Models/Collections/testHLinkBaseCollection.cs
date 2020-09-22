// <copyright file="testHLinkBaseCollection.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsViewXUnit.Data.Models.Collections
{
    using System;
    using GrampsView.Data.Model;

   

    public class TestHLinkBaseCollection
    {
        /// <summary>
        /// Tests the date null age.
        /// </summary>
        //[Fact]
        public void TestCreateHLinkCollection()
        {
            // Setup Collection
            HLinkBaseCollection<HLinkMediaModel> imageModelCollection = new HLinkBaseCollection<HLinkMediaModel>
            {
                //FirstImageHLink = new HLinkMediaModel(),
            };

            // Setup test hlink
            HLinkMediaModel testHLink = new HLinkMediaModel
            {
                //BackgroundColour = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor("White"),
            };

            // Test FirstImageHLink is correct
            //Assert.True(imageModelCollection.FirstImageHLink.CompareTo(testHLink) == 0);
        }
    }
}