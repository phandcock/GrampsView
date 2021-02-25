// TODO Needs XML 1.71 check

/// <summary>
/// Collection of HLinks to Address models.
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Collection of Address models.
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkAdressModel>))]
    public class HLinkOCAddressModelCollection : HLinkBaseCollection<HLinkAdressModel>
    {
        public HLinkOCAddressModelCollection()
        {
            Title = "Address Collection";
        }

        public override CardGroup GetCardGroup()
        {
            CardGroup t = base.GetCardGroup();

            t.Title = Title;

            return t;
        }

        public override void SetGlyph()
        {
            foreach (HLinkAdressModel argHLink in this)
            {
                argHLink.HLinkGlyphItem = DV.PlaceDV.GetGlyph(argHLink.HLinkKey);
            }

            // TODO Needed? SortAndSetFirst();
        }
    }
}