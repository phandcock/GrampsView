﻿// TODO Needs XML 1.71 check

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkCitationModel>))]
    public class HLinkPlaceModelCollection : HLinkBaseCollection<HLinkPlaceModel>
    {
        public HLinkPlaceModelCollection()
        {
            Title = "Place Collection";
        }

        public override CardGroup GetCardGroup()
        {
            CardGroup t = base.GetCardGroup();

            t.Title = Title;

            return t;
        }

        public override void SetGlyph()
        {
            foreach (HLinkPlaceModel argHLink in this)
            {
                ItemGlyph t = DV.PlaceDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.ImageHLinkMediHLink = t.ImageHLinkMediHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;
            }

            //// Set the first image link. Assumes main image is manually set to the first image in
            //// Gramps if we need it to be, e.g. Citations.
            SetFirstImage();

            if (Common.CommonLocalSettings.SortHLinkCollections)
            {
                Sort();
            }
        }

        /// <summary>
        /// Helper method to sort and set the firt image link.
        /// </summary>
        public void Sort()
        {
            //// Set the first image link. Assumes main image is manually set to the first image in
            //// Gramps if we need it to be, e.g. Citations.
            //PlaceModel tempModel = new PlaceModel();

            //FirstHLinkHomeImage.ImageType = CommonEnums.HLinkGlyphType.Unknown;

            //if (Count > 0)
            //{
            //    // Step through each citationmodel hlink in the collection
            //    for (int i = 0; i < Count; i++)
            //    {
            //        tempModel = DV.PlaceDV.PlaceData.GetModelFromHLink(this[i]);

            // if (tempModel.ModelItemGlyph.ImageType == CommonEnums.HLinkGlyphType.Image) {
            // FirstHLinkHomeImage = tempModel.ModelItemGlyph; break; } }

            // Sort the collection
            List<HLinkPlaceModel> t = this.OrderBy(HLinkCitationModel => HLinkCitationModel.DeRef.GetDefaultText).ToList();

            Items.Clear();

            foreach (HLinkPlaceModel item in t)
            {
                Items.Add(item);
            }
        }
    }
}