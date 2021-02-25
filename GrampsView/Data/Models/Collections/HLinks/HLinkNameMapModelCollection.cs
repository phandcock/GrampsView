// TODO Needs XML 1.71 check

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkNameMapModel>))]
    public class HLinkNameMapModelCollection : HLinkBaseCollection<HLinkNameMapModel>
    {
        public HLinkNameMapModelCollection()
        {
            Title = "NameMap Collection";
        }

        public override CardGroup GetCardGroup()
        {
            CardGroup t = base.GetCardGroup();

            t.Title = Title;

            return t;
        }

        public override void SetGlyph()
        {
            foreach (HLinkNameMapModel argHLink in this)
            {
                argHLink.HLinkGlyphItem = DV.NameMapDV.GetGlyph(argHLink.HLinkKey);
            }

            // TODO SortAndSetFirst();
        }
    }
}