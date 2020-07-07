// <copyright file="HLinkCitationModelCollection.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Observable collection of Citation HLinks.
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkCitationModel>))]
    public class HLinkCitationModelCollection : HLinkBaseCollection<HLinkCitationModel>
    {
        public HLinkCitationModelCollection()
        {
            Title = "Citation Collection";
        }

        public override CardGroup GetCardGroup()
        {
            CardGroup t = base.GetCardGroup();

            t.Title = Title;

            return t;
        }

        /// <summary>
        /// Helper method to sort and set the firt image link.
        /// </summary>
        public void SortAndSetFirst()
        {
            // Set the first image link. Assumes main image is manually set to the first image in
            // Gramps if we need it to be, e.g. Citations.
            ICitationModel tempCitationModel = new CitationModel();

            FirstHLinkHomeImage.HomeImageType = CommonEnums.HomeImageType.Unknown;

            if (Count > 0)
            {
                // Step through each citationmodel hlink in the collection
                for (int i = 0; i < Count; i++)
                {
                    tempCitationModel = DV.CitationDV.CitationData.GetModelFromHLink(this[i]);

                    if (tempCitationModel.HomeImageHLink.LinkToImage)
                    {
                        FirstHLinkHomeImage = tempCitationModel.HomeImageHLink;
                        break;
                    }
                }

                // Sort the collection
                List<HLinkCitationModel> t = this.OrderBy(HLinkCitationModel => HLinkCitationModel.DeRef.GDateContent).ToList();

                Items.Clear();

                foreach (HLinkCitationModel item in t)
                {
                    Items.Add(item);
                }
            }
        }
    }
}