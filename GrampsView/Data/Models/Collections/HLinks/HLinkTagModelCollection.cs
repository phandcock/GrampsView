// <copyright file="HLinkTagModelCollection.cs" company="PlaceholderCompany">
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
    /// HLink Tag Model Collection.
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkTagModel>))]
    public class HLinkTagModelCollection : HLinkBaseCollection<HLinkTagModel>
    {
        public HLinkTagModelCollection()
        {
            Title = "Tag Collection";
        }

        public override CardGroup GetCardGroup()
        {
            CardGroup t = base.GetCardGroup();

            t.Title = Title;

            return t;
        }
    }
}