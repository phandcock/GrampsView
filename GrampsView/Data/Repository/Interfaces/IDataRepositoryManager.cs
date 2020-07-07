//-----------------------------------------------------------------------
//
// Interface defintion for IGrampsRepository.cs
//
// <copyright file="IDataRepositoryManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Repository
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface definitions for IGrampsRepository.
    /// </summary>
    public interface IDataRepositoryManager
    {
        ///// <summary>
        ///// Determines whether [is data loaded].
        ///// </summary>
        ///// <returns> Flag if data is loaded </returns>
        // bool DataLoaded { get; }

        /// <summary>
        /// Gets the storage.
        /// </summary>
        /// <value>
        /// The storage.
        /// </value>
        IStoreFile Storage
        {
            get;
        }

        ///// <summary>
        ///// Load Gramps Export XML Plus Media
        ///// </summary>
        ///// <returns> loaded or not </returns>
        // Task<bool> TriggerLoadGPKGFileAsync(bool deleteOld);

        void StartDataLoad(bool unUsed);

        void StartDataLoad();

        /// <summary>
        /// Triggers the load gramps un zipped folder asynchronous.
        /// </summary>
        /// <returns>
        /// </returns>
        Task<bool> TriggerLoadGrampsUnZippedFolderAsync();

        /// <summary>
        /// Loads the data asynchronous.
        /// </summary>
        /// <returns>
        /// </returns>
        Task<bool> TriggerLoadSerialDataAsync();
    }
}