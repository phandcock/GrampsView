/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkBackLink>))]
    public class HLinkBackLinkModelCollection : HLinkBaseCollection<HLinkBackLink>
    {
        public override CardGroup CardGroupAsProperty
        {
            get
            {
                CardGroup t = new CardGroup();

                foreach (HLinkBackLink item in Items) { t.Add(item.HLink()); }

                t.Title = "Backlink Collection";

                return t;
            }
        }
    }
}