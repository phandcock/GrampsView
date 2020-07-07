// <copyright file="HLinkBackLinkModelCollection.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    using GrampsView.Common;
    using GrampsView.Data.Model;

    /// <summary>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkBackLink>))]
    public class HLinkBackLinkModelCollection : HLinkBaseCollection<HLinkBackLink>
    {
        public override CardGroup GetCardGroup()
        {
            CardGroup t = new CardGroup();

            foreach (HLinkBackLink item in Items) { t.Add(item.HLink()); }

            t.Title = "Backlink Collection";

            return t;
        }
    }
}