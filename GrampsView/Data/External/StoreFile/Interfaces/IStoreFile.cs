// Copyright (c) phandcock.  All rights reserved.

using System.Threading.Tasks;

namespace GrampsView.Data
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

        /// <summary>
        /// Decompresses the gzip.
        /// </summary>
        /// <param name="grampsDataFile">
        /// The gramps data file.
        /// </param>
        /// <returns>
        /// </returns>
        bool DecompressGZIP(IFileInfoEx grampsDataFile);

        Task<bool> DecompressTAR();
    }
}