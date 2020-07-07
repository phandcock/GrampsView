// <copyright file="ICitationDataView.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.DataView
{
    using System.Collections.Generic;

    using GrampsView.Data.Collections;

    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;

    /// <summary>
    /// Interface for the Note Repository.
    /// </summary>
    public interface ICitationDataView : IDataViewBase<CitationModel, HLinkCitationModel, HLinkCitationModelCollection>
    {
        /// <summary>
        /// Gets or sets the specified h link string.
        /// </summary>
        /// <value>
        /// The citation data.
        /// </value>
        /// <param name="HLinkString">
        /// The h link string.
        /// </param>
        RepositoryModelDictionary<CitationModel, HLinkCitationModel> CitationData
        {
            get;
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        new IReadOnlyList<CitationModel> DataDefaultSort { get; }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// Hlink of Citation model collection.
        /// </returns>
        HLinkCitationModelCollection GetAllAsHLink();

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// </returns>
        new HLinkCitationModelCollection HLinkCollectionSort(HLinkCitationModelCollection collectionArg);
    }
}