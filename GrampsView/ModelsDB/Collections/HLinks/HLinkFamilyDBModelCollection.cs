// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common.CustomClasses;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.DBModels;
using GrampsView.ModelsDB.HLinks.Models;

using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GrampsView.ModelsDB.Collections.HLinks
{
    /// <summary>
    /// Collection of Family hLinks. /// XML 1.71 check done
    /// </summary>

    [KnownType(typeof(ObservableCollection<HLinkFamilyDBModel>))]
    public class HLinkFamilyDBModelCollection : HLinkDBBaseCollection<HLinkFamilyDBModel>
    {
        /// <summary>
        /// Gets the dereferenced Family Models.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        [JsonIgnore]
        public ObservableCollection<FamilyDBModel> DeRef
        {
            get
            {
                ObservableCollection<FamilyDBModel> t = new();

                foreach (HLinkFamilyDBModel item in Items)
                {
                    t.Add(item.DeRef);
                }

                return t;
            }
        }

        public HLinkFamilyDBModelCollection()
        {
            Title = "Family Collection";
        }

        public override void SetGlyph()
        {
            foreach (HLinkFamilyDBModel argHLink in this)
            {
                ItemGlyph t = DL.FamilyDL.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.ImageHLink = t.ImageHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;
            }

            base.SetGlyph();
        }

        //public void Sort()
        //{
        //    // Sort the collection
        //    List<HLinkFamilyModel> t = this.OrderBy(HLinkCitationModel => HLinkCitationModel.DeRef).ToList();

        // Items.Clear();

        //    foreach (HLinkFamilyModel item in t)
        //    {
        //        Items.Add(item);
        //    }
        //}
    }
}