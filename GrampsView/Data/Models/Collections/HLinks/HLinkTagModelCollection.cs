// TODO Needs XML 1.71 check

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
    /// HLink Tag Model Collection.
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkTagModel>))]
    public class HLinkTagModelCollection : HLinkBaseCollection<HLinkTagModel>
    {
        public HLinkTagModelCollection()
        {
            Title = "Tag Collection";
        }

        public override CardGroup GetCardGroup()
        {
            CardGroup t = base.GetCardGroup();

            t.Title = Title;

            return t;
        }

        public override void SetGlyph()
        {
            foreach (HLinkTagModel argHLink in this)
            {
                ItemGlyph t = DV.TagDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.ImageHLink = t.ImageHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;

                // Tags are special in that the colour of the icon is the colour of the tag
                argHLink.HLinkGlyphItem.SymbolColour = t.SymbolColour;
            }

            //// Set the first image link. Assumes main image is manually set to the first image in
            //// Gramps if we need it to be, e.g. Citations.
            SetFirstImage();

            if (CommonLocalSettings.SortHLinkCollections)
            {
                Sort();
            }
        }

        /// <summary>
        /// Helper method to sort and set the firt image link.
        /// </summary>
        public void Sort()
        {
            // Sort the collection
            List<HLinkTagModel> t = this.OrderBy(HLinkTagModel => HLinkTagModel.DeRef.GetDefaultText).ToList();

            Items.Clear();

            foreach (HLinkTagModel item in t)
            {
                Items.Add(item);
            }
        }
    }
}