using GrampsView.Data.Model;
using GrampsView.Models.Collections.HLinks;

using System.Runtime.Serialization;

namespace GrampsView.Data.Collections
{
    /// <summary>
    /// ChildRefmodel collection
    /// /// XML 1.71 check not required as not part of Gramps model
    /// </summary>

    [KnownType(typeof(HLinkBaseCollection<HLinkChildRefModel>))]
    public class HLinkChildRefCollection : HLinkBaseCollection<HLinkChildRefModel>
    {
        public HLinkChildRefCollection()
        {
            Title = "Child Collection";
        }

        public HLinkPersonModelCollection AsHLinkPersonModels

        {
            get
            {
                HLinkPersonModelCollection returnValue = new()
                {
                    Title = Title
                };

                foreach (HLinkChildRefModel item in this)
                {
                    returnValue.Add(item.DeRef.HLink);
                }

                return returnValue;
            }
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