/// <summary>
/// XML 1.71 check not required as not part of Gramps model
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Data.Model;

    using System.Runtime.Serialization;

    /// <summary>
    /// ChildRefmodel collection
    /// </summary>

    [CollectionDataContract]
    [KnownType(typeof(HLinkBaseCollection<HLinkChildRefModel>))]
    public class ChildRefCollectionCollection : HLinkBaseCollection<HLinkChildRefModel>
    {
        public ChildRefCollectionCollection()
        {
            Title = "Child Reference Collection";
        }

        public override void SetGlyph()
        {
            foreach (HLinkChildRefModel argHLink in this)
            {
                // This is required ot get Glyph valid so we can load the Person HLink
                argHLink.HLinkGlyphItem.ImageType = Common.CommonEnums.HLinkGlyphType.Symbol;

                HLinkPersonModel t = argHLink.DeRef.HLink;

                argHLink.HLinkGlyphItem.ImageType = t.DeRef.ModelItemGlyph.ImageType;
                argHLink.HLinkGlyphItem.ImageHLink = t.DeRef.ModelItemGlyph.ImageHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.DeRef.ModelItemGlyph.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.DeRef.ModelItemGlyph.ImageSymbolColour;

                argHLink.HLinkGlyphItem.MediaHLink = t.DeRef.ModelItemGlyph.MediaHLink;
            }
        }
    }
}