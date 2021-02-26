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
    /// Observable collection of Citation HLinks.
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkCitationModel>))]
    public class HLinkCitationModelCollection : HLinkBaseCollection<HLinkCitationModel>
    {
        public HLinkCitationModelCollection()
        {
            Title = "Citation Collection";
        }

        public override CardGroup GetCardGroup()
        {
            CardGroup t = base.GetCardGroup();

            t.Title = Title;

            return t;
        }

        public override void SetGlyph()
        {
            // Back Reference Citation HLinks
            foreach (HLinkCitationModel argHLink in this)
            {
                ItemGlyph t = DV.CitationDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.HLinkMediHLink = t.HLinkMediHLink;
            }

            SortAndSetFirst();
        }

        /// <summary>
        /// Helper method to sort and set the firt image link.
        /// </summary>
        public void SortAndSetFirst()
        {
            // Set the first image link. Assumes main image is manually set to the first image in
            // Gramps if we need it to be, e.g. Citations.
            ICitationModel tempCitationModel = new CitationModel();

            FirstHLinkHomeImage.ImageType = CommonEnums.HLinkGlyphType.Symbol;

            if (Count > 0)
            {
                // Step through each citationmodel hlink in the collection
                for (int i = 0; i < Count; i++)
                {
                    tempCitationModel = DV.CitationDV.GetModelFromHLink(this[i]);

                    if (tempCitationModel.ModelItemGlyph.ImageType == CommonEnums.HLinkGlyphType.Image)
                    {
                        FirstHLinkHomeImage = tempCitationModel.ModelItemGlyph;
                        break;
                    }
                }

                // Sort the collection
                List<HLinkCitationModel> t = this.OrderBy(HLinkCitationModel => HLinkCitationModel.DeRef.GDateContent.SortDate).ToList();

                Items.Clear();

                foreach (HLinkCitationModel item in t)
                {
                    Items.Add(item);
                }
            }
        }
    }
}