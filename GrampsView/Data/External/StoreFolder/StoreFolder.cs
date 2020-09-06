//-----------------------------------------------------------------------
//
// Storage file routines for the GrampsStoreXML
//
// <copyright file="StoreFile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;

    using System;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    [DataContract]
    public partial class StoreFolder : CommonBindableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreFile"/> class.
        /// </summary>
        /// <param name="iocCommonProgress">
        /// The ioc common progress.
        /// </param>
        public StoreFolder()
        {
        }

        public static async Task<FileStream> FolderCreateFileAsync(DirectoryInfo argBaseFolder, string argFileName)
        {
            // TODO Handle relative paths

            // Check for relative path
            if (!StoreFileUtility.IsRelativeFilePathValid(argFileName))
            {
                return null;
            }

            // load the real file
            if (argBaseFolder != null)
            {
                try
                {
                    FileStream tt = File.Create(Path.Combine(argBaseFolder.FullName, argFileName));

                    return tt;
                }
                catch (FileNotFoundException ex)
                {
                    await DataStore.CN.MajorStatusAdd(ex.Message + ex.FileName).ConfigureAwait(false);

                    // default to a standard file marker
                }
                catch (Exception ex)
                {
                    DataStore.CN.NotifyException(ex.Message + argFileName, ex);
                    throw;
                }
            }

            return null;
        }

        /// <summary>
        /// get the StorageFile of the file.
        /// </summary>
        /// <param name="argFileName">
        /// file path relative to the provider base folder.
        /// </param>
        /// <returns>
        /// StorageFile for the chosen file.
        /// </returns>
        public static async Task<bool> FolderFileExistsAsync(DirectoryInfo argBaseFolder, string argFileName)
        {
            // Check for relative path
            if (!StoreFileUtility.IsRelativeFilePathValid(argFileName))
            {
                return false;
            }

            // load the real file
            if (argBaseFolder != null)
            {
                try
                {
                    FileInfo[] t = argBaseFolder.GetFiles();

                    foreach (FileInfo item in t)
                    {
                        if (item.Name == argFileName)
                        {
                            return true;
                        }
                    }
                }
                catch (FileNotFoundException ex)
                {
                    await DataStore.CN.MajorStatusAdd(ex.Message + ex.FileName).ConfigureAwait(false);

                    // default to a standard file marker
                }
                catch (Exception ex)
                {
                    DataStore.CN.NotifyException(ex.Message + argFileName, ex);
                    throw;
                }
            }

            return false;
        }

        public static FileInfoEx FolderGetFile(DirectoryInfo argBaseFolder, string argFileName)
        {
            Contract.Requires(argBaseFolder != null);

            // TODO Handle relative paths

            // Check for relative path
            if (!StoreFileUtility.IsRelativeFilePathValid(argFileName))
            {
                return new FileInfoEx();
            }

            // load the real file
            DirectoryInfo realPath = new DirectoryInfo(Path.Combine(argBaseFolder.FullName, Path.GetDirectoryName(argFileName)));

            if (realPath != null)
            {
                try
                {
                    FileInfo[] t = realPath.GetFiles();

                    foreach (FileInfo item in t)
                    {
                        if (item.Name == Path.GetFileName(argFileName))
                        {
                            return new FileInfoEx(item);
                        }
                    }
                }
                catch (FileNotFoundException ex)
                {
                    DataStore.CN.NotifyError(ex.Message + ex.FileName);

                    // default to a standard file marker
                }
                catch (DirectoryNotFoundException ex)
                {
                    DataStore.CN.NotifyError("Directory not found when deserialising the data.  Perahps the GPKG filenames are too long?" + ex.Message);

                    // default to a standard file marker
                }
                catch (Exception ex)
                {
                    DataStore.CN.NotifyException(ex.Message + argFileName, ex);
                    throw;
                }
            }

            return new FileInfoEx();
        }
    }
}