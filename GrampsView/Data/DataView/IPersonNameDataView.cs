// <copyright file="IAddressDataView.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.DataView
{
    using System.Collections.Generic;

    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using GrampsView.Models.DataModels.Minor;

    /// <summary>
    /// Interface for the Address Repository.
    /// </summary>
    public interface IPersonNameDataView : IDataViewBase<PersonNameModel, HLinkPersonNameModel, HLinkPersonNameModelCollection>
    {
        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        new IReadOnlyList<PersonNameModel> DataDefaultSort { get; }

        /// <summary>
        /// Gets or sets the specified address hlink string.
        /// </summary>
        /// <value>
        /// The citation data.
        /// </value>
        RepositoryModelDictionary<PersonNameModel, HLinkPersonNameModel> PersonNameData
        {
            get;
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// Hlink of Address model collection.
        /// </returns>
        HLinkPersonNameModelCollection GetAllAsHLink();

        /// <summary>
        /// HLink collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// </returns>
        new HLinkPersonNameModelCollection HLinkCollectionSort(HLinkPersonNameModelCollection collectionArg);
    }
}