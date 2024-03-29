﻿// TODO Needs XML 1.71 check


using GrampsView.Common.CustomClasses;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;

using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace GrampsView.Data.Collections
{
    /// <summary>
    /// </summary>

    [KnownType(typeof(ObservableCollection<HLinkNameMapModel>))]
    public class HLinkNameMapModelCollection : HLinkBaseCollection<HLinkNameMapModel>
    {
        public HLinkNameMapModelCollection()
        {
            Title = "NameMap Collection";
        }

        public override void SetGlyph()
        {
            foreach (HLinkNameMapModel argHLink in this)
            {
                ItemGlyph t = DV.NameMapDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.ImageHLink = t.ImageHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;
            }

            base.SetGlyph();
        }

        ///// <summary>
        ///// Helper method to sort and set the firt image link.
        ///// </summary>
        //public void Sort()
        //{
        //    // Sort the collection
        //    List<HLinkNameMapModel> t = this.OrderBy(HLinkNameMapModel => HLinkNameMapModel.DeRef.ToString()).ToList();

        // Items.Clear();

        //    foreach (HLinkNameMapModel item in t)
        //    {
        //        Items.Add(item);
        //    }
        //}
    }
}