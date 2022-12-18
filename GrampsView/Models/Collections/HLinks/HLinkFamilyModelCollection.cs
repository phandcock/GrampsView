﻿using GrampsView.Common.CustomClasses;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels;

using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

/// <summary>
/// XML 1.71 check done
/// </summary>
namespace GrampsView.Data.Collections
{
    /// <summary>
    /// Collection of Family hLinks.
    /// </summary>


    [KnownType(typeof(ObservableCollection<HLinkFamilyModel>))]
    public class HLinkFamilyModelCollection : HLinkBaseCollection<HLinkFamilyModel>
    {
        public HLinkFamilyModelCollection()
        {
            Title = "Family Collection";
        }

        /// <summary>
        /// Gets the dereferenced Family Models.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        [JsonIgnore]
        public ObservableCollection<FamilyModel> DeRef
        {
            get
            {
                ObservableCollection<FamilyModel> t = new();

                foreach (HLinkFamilyModel item in Items)
                {
                    t.Add(item.DeRef);
                }

                return t;
            }
        }

        public override void SetGlyph()
        {
            foreach (HLinkFamilyModel argHLink in this)
            {
                ItemGlyph t = DV.FamilyDV.GetGlyph(argHLink.HLinkKey);

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