// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common.Interfaces;
using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Interfaces;

namespace GrampsView.Data.StoreFile
{
    /// <summary>
    /// Interface definitions for File Storage.
    /// </summary>
    public interface IStoreFileZip
    {
        bool DecompressGZIP(IFileInfoEx inputFile);

        Task<bool> ExtractGZip(IFileInfoEx argInputFile, string argOutFile);

        IMediaModel ExtractZipFileFirstImage(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel);
    }
}