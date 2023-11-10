// Copyright (c) phandcock.  All rights reserved.

using GrampsView.DBModels;
using GrampsView.ModelsDB.Collections.HLinks;
using GrampsView.ModelsDB.HLinks.Models;

using Microsoft.EntityFrameworkCore;

namespace GrampsView.Data.DataLayer.Interfaces
{
    /// <summary>
    /// Interface for the Note Repository.
    /// </summary>
    public interface ICitationDataLayer : IDataLayerBase<CitationDBModel, HLinkCitationDBModel, HLinkCitationDBModelCollection>
    {
        DbSet<CitationDBModel> CitationAccess { get; }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        new IReadOnlyList<CitationDBModel> DataAsDefaultSort { get; }

        /// <summary>
        /// Gets all as HLink.
        /// </summary>
        /// <returns>
        /// HLink of Citation model collection.
        /// </returns>
        HLinkCitationDBModelCollection GetAllAsHLink();

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// </returns>
        new HLinkCitationDBModelCollection HLinkCollectionSort(HLinkCitationDBModelCollection collectionArg);
    }
}