// TODO Needs XML 1.71 check

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkBackLink>))]
    public class HLinkBackLinkModelCollection : HLinkBaseCollection<HLinkBackLink>
    {
        public HLinkBackLinkModelCollection()
        {
            Title = "BackLink Collection";
        }

        public CardGroup AsGroupedCardGroup
        {
            get
            {
                CardGroup t = new CardGroup();

                var query = from item in Items
                            orderby item.HLinkType

                            group item by (item.HLinkType) into g
                            select new
                            {
                                GroupName = g.Key,
                                Items = g
                            };

                foreach (var g in query)
                {
                    CardGroup info = new CardGroup()
                    {
                        Title = g.GroupName.ToString(),
                    };

                    foreach (var item in g.Items)
                    {
                        info.Add(item.HLink());
                    }

                    t.Add(info);
                }

                return t;
            }
        }

        public override CardGroup CardGroupAsProperty
        {
            get
            {
                CardGroup t = new CardGroup();

                foreach (HLinkBackLink item in Items)
                {
                    t.Add(item.HLink());
                }

                t.Title = Title;

                return t;
            }
        }
    }
}