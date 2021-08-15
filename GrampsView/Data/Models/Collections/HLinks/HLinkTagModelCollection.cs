namespace GrampsView.Data.Collections
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Collection of HLinks to Tags
    /// <list type="table">
    /// <listheader>
    /// <term> Item </term>
    /// <term> Status </term>
    /// </listheader>
    /// <item>
    /// <description> XML 1.71 check </description>
    /// <description> Done </description>
    /// </item>
    /// </list>
    /// <para> <br/> </para>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkTagModel>))]
    public class HLinkTagModelCollection : HLinkBaseCollection<HLinkTagModel>
    {
        public HLinkTagModelCollection()
        {
            Title = "Tag Collection";
        }

        public override void SetGlyph()
        {
            foreach (HLinkTagModel argHLink in this)
            {
                ItemGlyph modelGylph = DV.TagDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = modelGylph.ImageType;
                argHLink.HLinkGlyphItem.ImageHLink = modelGylph.ImageHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = modelGylph.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = modelGylph.ImageSymbolColour;

                // Tags are special in that the colour of the icon is the colour of the tag
                argHLink.HLinkGlyphItem.SymbolColour = modelGylph.SymbolColour;
            }

            base.SetGlyph();
        }
    }
}