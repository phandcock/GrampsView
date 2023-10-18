// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Collections;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels;
using GrampsView.Models.DBModels;

using Microsoft.EntityFrameworkCore;

namespace GrampsView.Data.DataView
{
    /// <summary>
    /// Interface for the Note Repository.
    /// </summary>
    public interface ICitationDataLayer : IDataLayerBase<CitationModel, HLinkCitationModel, HLinkCitationModelCollection>
    {
        DbSet<CitationDBModel> CitationAccess { get; }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        new IReadOnlyList<CitationModel> DataAsDefaultSort { get; }

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