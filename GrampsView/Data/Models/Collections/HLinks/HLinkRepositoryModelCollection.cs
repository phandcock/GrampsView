// <copyright file="HLinkRepositoryModelCollection.cs" company="PlaceholderCompany">
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
    }
}