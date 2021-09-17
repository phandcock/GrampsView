﻿// TODO Needs XML 1.71 check

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// </summary>

    [KnownType(typeof(ObservableCollection<HLinkMediaModel>))]
    public class HLinkMediaModelCollection : HLinkBaseCollection<HLinkMediaModel>
    {
        public HLinkMediaModelCollection()
        {
            Title = "Media Collection";
        }

        public override void SetGlyph()
        {
            foreach (HLinkMediaModel argHLink in this)
            {
                ItemGlyph t = DV.MediaDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.ImageHLink = t.ImageHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;

                argHLink.HLinkGlyphItem.MediaHLink = t.MediaHLink;
            }

            base.SetGlyph();
        }

        ///// <summary>
        ///// Helper method to sort.
        ///// </summary>
        ///// <param name="collectionArg">
        ///// The collection argument.
        ///// </param>
        //public void Sort()
        //{
        //    List<HLinkMediaModel> t = this.OrderBy(hLinkMediaModel => hLinkMediaModel.DeRef.GDescription).ToList();

        // Items.Clear();

        //    foreach (HLinkMediaModel item in t)
        //    {
        //        Items.Add(item);
        //    }
        //}
    }
}