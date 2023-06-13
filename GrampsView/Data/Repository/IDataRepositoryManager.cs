// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.Data.Repository
{
    /// <summary>
    /// Interface definitions for IGrampsRepository.
    /// </summary>
    public interface IDataRepositoryManager
    {
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