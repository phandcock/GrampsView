// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common.CustomClasses;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.ModelsDB.HLinks.Models;

using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace GrampsView.ModelsDB.Collections.HLinks
{
    /// <summary>
    /// Observable collection of Citation HLinks.
    ///  // XML 1.71 check Done
    /// </summary>

    [KnownType(typeof(ObservableCollection<HLinkCitationDBModel>))]
    public class HLinkCitationDBModelCollection : HLinkDBBaseCollection<HLinkCitationDBModel>
    {
        public HLinkCitationDBModelCollection()
        {
            Title = "Citation Collection";
        }

        //public override CardGroup GetCardGroup()
        //{
        //    CardGroup t = base.GetCardGroup();

        // t.Title = Title;

        //    return t;
        //}

        public override void SetGlyph()
        {
            // Back Reference Citation HLinks
            foreach (HLinkCitationDBModel argHLink in this)
            {
                ItemGlyph t = DL.CitationDL.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.ImageHLink = t.ImageHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;
            }

            base.SetGlyph();
        }

        /// <summary>
        /// Helper method to sort and set the firt image link.
        /// </summary>
        public override void Sort()
        {
            // Sort the collection
            List<HLinkCitationDBModel> t = this.OrderBy(HLinkCitationModel => HLinkCitationModel.DeRef.GDateContent.SortDate).ToList();

            Items.Clear();

            foreach (HLinkCitationDBModel item in t)
            {
                Items.Add(item);
            }
        }
    }
}