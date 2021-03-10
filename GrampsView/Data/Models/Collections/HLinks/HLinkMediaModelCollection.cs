// TODO Needs XML 1.71 check

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkMediaModel>))]
    public class HLinkMediaModelCollection : HLinkBaseCollection<HLinkMediaModel>
    {
        public HLinkMediaModelCollection()
        {
            Title = "Media Collection";
        }

        public override CardGroup GetCardGroup()
        {
            CardGroup t = base.GetCardGroup();

            t.Title = Title;

            return t;
        }

        public override void SetGlyph()
        {
            foreach (HLinkMediaModel argHLink in this)
            {
                ItemGlyph t = DV.MediaDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.ImageHLinkMediHLink = t.ImageHLinkMediHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;
            }

            //// Set the first image link. Assumes main image is manually set to the first image in
            //// Gramps if we need it to be, e.g. Citations.
            SetFirstImage();

            Sort();
        }

        /// <summary>
        /// Helper method to sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        public void Sort()
        {
            List<HLinkMediaModel> t = this.OrderBy(hLinkMediaModel => hLinkMediaModel.DeRef.GDescription).ToList();

            Items.Clear();

            foreach (HLinkMediaModel item in t)
            {
                Items.Add(item);
            }
        }
    }
}