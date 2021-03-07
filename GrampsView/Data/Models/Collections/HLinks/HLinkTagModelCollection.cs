// TODO Needs XML 1.71 check

namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
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
                argHLink.HLinkGlyphItem.HLinkMediHLink = t.HLinkMediHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;

                // Tags are special in that the colour of the icon is the colour of the tag
                argHLink.HLinkGlyphItem.SymbolColour = t.SymbolColour;
            }

            SortAndSetFirst();
        }

        public void SortAndSetFirst()
        {
            // TODO Need this
        }
    }
}