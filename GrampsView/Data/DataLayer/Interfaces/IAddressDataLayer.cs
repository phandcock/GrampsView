// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Collections;
using GrampsView.Data.DataLayer.Interfaces;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels.Minor;

namespace GrampsView.Data.DataView
{
    /// <summary>
    /// Interface for the Address Repository.
    /// </summary>
    public interface IAddressDataLayer : IDataLayerBase<AddressDBModel, HLinkAddressDBModel, HLinkAddressDBModelCollection>
    {
        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// Hlink of Address model collection.
        /// </returns>
        HLinkAddressDBModelCollection GetAllAsHLink();

        /// <summary>
        /// HLink collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// </returns>
        new HLinkAddressDBModelCollection HLinkCollectionSort(HLinkAddressDBModelCollection collectionArg);
    }
}