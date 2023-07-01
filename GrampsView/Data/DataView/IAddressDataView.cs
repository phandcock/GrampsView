// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Collections;
using GrampsView.Data.Model;
using GrampsView.Data.Repository;
using GrampsView.Models.DataModels.Minor;

using System.Collections.Generic;

namespace GrampsView.Data.DataView
{
    /// <summary>
    /// Interface for the Address Repository.
    /// </summary>
    public interface IAddressDataView : IDataViewBase<AddressModel, HLinkAdressModel, HLinkAddressModelCollection>
    {
        /// <summary>
        /// Gets or sets the specified address hlink string.
        /// </summary>
        /// <value>
        /// The citation data.
        /// </value>
        RepositoryModelDictionary<AddressModel, HLinkAdressModel> AddressData
        {
            get;
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        new IReadOnlyList<AddressModel> DataDefaultSort { get; }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// Hlink of Address model collection.
        /// </returns>
        HLinkAddressModelCollection GetAllAsHLink();

        /// <summary>
        /// HLink collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// </returns>
        new HLinkAddressModelCollection HLinkCollectionSort(HLinkAddressModelCollection collectionArg);
    }
}