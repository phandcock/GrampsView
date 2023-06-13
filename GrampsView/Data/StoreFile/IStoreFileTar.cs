// Copyright (c) phandcock.  All rights reserved.

using ICSharpCode.SharpZipLib.Tar;

namespace GrampsView.Data.StoreFile
{
    /// <summary>
    /// Interface definitions for File Storage.
    /// </summary>
    public interface IStoreFileTar
    {
        Task<bool> DecompressTAR();

        Task<bool> ExtractTar(TarInputStream tarIn);
    }
}