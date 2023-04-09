// Copyright (c) phandcock.  All rights reserved.

using System.Threading.Tasks;

namespace GrampsView.Data.Repository
{
    /// <summary>
    /// Interface definitions for IGrampsRepository.
    /// </summary>
    public interface IDataRepositoryManager
    {
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

        //void StartDataLoad();


        Task StartDataLoadAsync();

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