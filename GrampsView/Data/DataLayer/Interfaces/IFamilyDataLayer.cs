// Copyright (c) phandcock.  All rights reserved.

using GrampsView.DBModels;
using GrampsView.ModelsDB.Collections.HLinks;
using GrampsView.ModelsDB.HLinks.Models;

using Microsoft.EntityFrameworkCore;

namespace GrampsView.Data.DataLayer.Interfaces
{
    /// <summary>
    /// Interface for the Family Repository.
    /// </summary>
    public interface IFamilyDataLayer : IDataLayerBase<FamilyDBModel, HLinkFamilyDBModel, HLinkFamilyDBModelCollection>
    {
        new IReadOnlyList<FamilyDBModel> DataAsDefaultSort
        {
            get;
        }

        // TODO add this to the general Interface
        DbSet<FamilyDBModel> FamilyAccess { get; }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkFamilyDBModelCollection GetAllAsHLink();

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// </returns>
        new HLinkFamilyDBModelCollection HLinkCollectionSort(HLinkFamilyDBModelCollection collectionArg);
    }
}