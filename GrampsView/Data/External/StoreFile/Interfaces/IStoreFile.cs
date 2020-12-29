//-----------------------------------------------------------------------
//
// Interface definition for External Storage Providers
//
// <copyright file="IStoreFile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data
{
    using GrampsView.Common;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface definitions for IExternal Storage.
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
        Task<bool> DecompressGZIP(FileInfoEx grampsDataFile);

        Task<bool> DecompressTAR();
    }
}