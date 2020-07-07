// <copyright file="SurnameModelCollection.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    using GrampsView.Common;
    using GrampsView.Data.Model;

    /// <summary>
    /// Attribute model collection.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectViewModel.ObservableCollection{GrampsView.Data.ViewModel.AttributeModel}"/>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkPersonNameModel>))]
    public class HLinkPersonNameModelCollection : HLinkBaseCollection<HLinkPersonNameModel>
    {
        public HLinkPersonNameModelCollection()
        {
            Title = "Person Names";
        }

        public HLinkPersonNameModel GetPrimaryName
        {
            get
            {
                // Should always have a name but just in case
                if (Items.Count == 0)
                {
                    return new HLinkPersonNameModel();
                }

                // Return the primary name if it exists
                if (Items.Count > 0)
                {
                    return this.Items[0] as HLinkPersonNameModel;
                }

                return new HLinkPersonNameModel();
            }
        }

        public override CardGroup GetCardGroup()
        {
            CardGroup t = base.GetCardGroup();

            t.Title = Title;

            return t;
        }
    }
}