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
    [KnownType(typeof(ObservableCollection<HLinkCitationModel>))]
    public class HLinkRepositoryModelCollection : HLinkBaseCollection<HLinkRepositoryModel>
    {
        public HLinkRepositoryModelCollection()
        {
            Title = "Repository Collection";
        }

        public override CardGroup GetCardGroup()
        {
            CardGroup t = base.GetCardGroup();

            t.Title = Title;

            return t;
        }

        public override void SetGlyph()
        {
            foreach (HLinkRepositoryModel argHLink in this)
            {
                argHLink.HLinkGlyphItem = DV.RepositoryDV.GetGlyph(argHLink.HLinkKey);
            }

            // TODO Need this SortAndSetFirst();
        }
    }
}