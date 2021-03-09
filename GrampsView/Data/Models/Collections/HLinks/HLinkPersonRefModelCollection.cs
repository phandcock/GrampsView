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
    /// Contains pointers to family models.
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.HLinkBaseCollection{GrampsView.Data.ViewModel.HLinkPersonModel}"/>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkPersonRefModel>))]
    public class HLinkPersonRefModelCollection : HLinkBaseCollection<HLinkPersonRefModel>
    {
        public HLinkPersonRefModelCollection()
        {
            Title = "People Ref Collection";
        }

        public override CardGroup GetCardGroup()
        {
            CardGroup t = base.GetCardGroup();

            t.Title = Title;

            return t;
        }

        public override void SetGlyph()
        {
            foreach (HLinkPersonRefModel argHLink in this)
            {
                // TODO check this
                ItemGlyph t = DV.PersonDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.ImageHLinkMediHLink = t.ImageHLinkMediHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;
            }

            SortAndSetFirst();
        }

        /// <summary>
        /// Helper method to sort and set the firt image link.
        /// </summary>
        public void SortAndSetFirst()
        {
            //// Set the first image link. Assumes main image is manually set to the first image in
            //// Gramps if we need it to be, e.g. Citations.
            //PersonModel tempModel = new PersonModel();

            //FirstHLinkHomeImage.ImageType = CommonEnums.ImageType.Unknown;

            //if (Count > 0)
            //{
            //    // Step through each citationmodel hlink in the collection
            //    for (int i = 0; i < Count; i++)
            //    {
            //        tempModel = DV.PersonDV.PersonData.GetModelFromHLink(this[i]);

            // if (tempModel.HLinkGlyph.LinkToImage) { FirstHLinkHomeImage = tempModel.HLinkGlyph;
            // break; } }

            // Sort the collection
            List<HLinkPersonRefModel> t = this.OrderBy(HLinkEventModel => HLinkEventModel.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.SortName).ToList();

            Items.Clear();

            foreach (HLinkPersonRefModel item in t)
            {
                Items.Add(item);
            }
        }
    }
}