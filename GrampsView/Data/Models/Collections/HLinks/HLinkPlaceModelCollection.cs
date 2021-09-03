// TODO Needs XML 1.71 check

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
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkCitationModel>))]
    public class HLinkPlaceModelCollection : HLinkBaseCollection<HLinkPlaceModel>
    {
        public HLinkPlaceModelCollection()
        {
            Title = "Place Collection";
        }

        public override void SetGlyph()
        {
            foreach (HLinkPlaceModel argHLink in this)
            {
                ItemGlyph t = DV.PlaceDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.ImageHLink = t.ImageHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;
            }

            base.SetGlyph();
        }
    }
}