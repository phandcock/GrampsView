// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.Data.StoreFile
{
    /// <summary>
    /// Interface definitions for File Storage.
    /// </summary>
    public interface IStoreFile
    {
        /// <summary>
        /// Initialises the data storage asynchronous.
        /// </summary>
        /// <returns>
        /// </returns>
        Task<bool> DataStorageInitialiseAsync();
    }
}