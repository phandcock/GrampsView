/// <summary>
/// XML 1.71 check done
/// </summary>
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
    /// Collection of Family hLinks.
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.HLinkBaseCollection{GrampsView.Data.ViewModel.HLinkFamilyModel}"/>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkFamilyModel>))]
    public class HLinkFamilyModelCollection : HLinkBaseCollection<HLinkFamilyModel>
    {
        public HLinkFamilyModelCollection()
        {
            Title = "Family Collection";
        }

        /// <summary>
        /// Gets the dereferenced Family Models.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public ObservableCollection<FamilyModel> DeRef
        {
            get
            {
                ObservableCollection<FamilyModel> t = new ObservableCollection<FamilyModel>();

                foreach (HLinkFamilyModel item in Items)
                {
                    t.Add(item.DeRef);
                }

                return t;
            }
        }

        public override CardGroup GetCardGroup()
        {
            CardGroup t = base.GetCardGroup();

            t.Title = Title;

            return t;
        }

        public override void SetGlyph()
        {
            foreach (HLinkFamilyModel argHLink in this)
            {
                ItemGlyph t = DV.FamilyDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.ImageHLink = t.ImageHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;
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
        /// Helper method to sort
        /// </summary>
        public void Sort()
        {
            // Sort the collection
            List<HLinkFamilyModel> t = this.OrderBy(HLinkCitationModel => HLinkCitationModel.DeRef.FamilyDisplayNameSort).ToList();

            Items.Clear();

            foreach (HLinkFamilyModel item in t)
            {
                Items.Add(item);
            }
        }
    }
}