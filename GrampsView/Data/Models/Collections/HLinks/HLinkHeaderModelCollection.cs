//-----------------------------------------------------------------------
//
// Storage routines for the FamilyModel
//
// <copyright file="HLinkHeaderModelCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Collection of EVent $$(HLinks)$$.
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkHeaderModel>))]
    public class HLinkHeaderModelCollection : HLinkBaseCollection<HLinkHeaderModel>
    {
        public override CardGroup GetCardGroup()
        {
            CardGroup t = base.GetCardGroup();

            t.Title = Title;

            return t;
        }
    }
}