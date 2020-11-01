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

    using ICSharpCode.SharpZipLib.GZip;
    using ICSharpCode.SharpZipLib.Tar;

    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    [DataContract]
    public partial class StoreFile : CommonBindableBase, IStoreFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreFile"/> class.
        /// </summary>
        /// <param name="iocCommonProgress">
        /// The ioc common progress.
        /// </param>
        public StoreFile()
        {
        }

        /// <summary>
        /// get the StorageFile of the file.
        /// </summary>
        /// <param name="relativeFilePath">
        /// file path relative to the provider base folder.
        /// </param>
        /// <returns>
        /// StorageFile for the chosen file.
        /// </returns>
        public async static Task<FileInfoEx> GetStorageFileAsync(string relativeFilePath)
        {
            FileInfoEx resultFile = new FileInfoEx();

            // Validate the input
            if ((relativeFilePath is null) || (string.IsNullOrEmpty(relativeFilePath)))
            {
                return resultFile;
            }

            // Check for relative path
            if (!StoreFileUtility.IsRelativeFilePathValid(relativeFilePath))
            {
                return resultFile;
            }

            // load the real file
            DirectoryInfo tt = DataStore.AD.CurrentDataFolder;
            if (tt != null)
            {
                try
                {
                    if (Directory.Exists(Path.Combine(tt.FullName, Path.GetDirectoryName(relativeFilePath))))
                    {
                        FileInfo[] t = tt.GetFiles(relativeFilePath);

                        if (t.Length > 0)
                        {
                            resultFile.FInfo = t[0];
                        }
                    }
                    return resultFile;
                }
                catch (FileNotFoundException ex)
                {
                    await DataStore.CN.DataLogEntryAdd(ex.Message + ex.FileName).ConfigureAwait(false);

                    // default to a standard file marker
                }
                catch (Exception ex)
                {
                    DataStore.CN.NotifyException(ex.Message + relativeFilePath, ex);
                    throw;
                }
            }

            return resultFile;
        }

        /// <summary>
        /// Deletes all local copies of GRAMPS data.
        /// </summary>
        /// <returns>
        /// Flag if data cleared.
        /// </returns>
        public async Task<bool> DataStorageInitialiseAsync()
        {
            await DataStore.CN.DataLogEntryAdd("Deleting existing datastorage").ConfigureAwait(false);
            {
                try
                {
                    DirectoryInfo t = DataStore.AD.CurrentDataFolder;

                    foreach (FileInfo item in t.GetFiles())
                    {
                        item.Delete();
                    }

                    foreach (var item in t.GetDirectories())
                    {
                        item.Delete(true);
                    }

                    // Create standard directories
                    t.CreateSubdirectory("Cropped");
                }
                catch (Exception ex)
                {
                    DataStore.CN.NotifyException("DataStorageInitialiseAsync", ex);
                    throw;
                }
            }

            await DataStore.CN.DataLogEntryDelete().ConfigureAwait(false);

            return true;
        }

        /// <summary>
        /// This routine is heavily customized to decompress GRAMPS whole of database export file
        /// (i.e. .gramps) files.
        /// </summary>
        /// <param name="inputFile">
        /// Input GRAMPS export file.
        /// </param>
        /// <returns>
        /// Flag indicating success or not.
        /// </returns>
        public async Task<bool> DecompressGZIP(FileInfoEx inputFile)
        {
            await DataStore.CN.DataLogEntryAdd("Decompressing GRAMPS GZIP file").ConfigureAwait(false);

            // Check arguments
            if (inputFile == null)
            {
                DataStore.CN.NotifyError("The input file is null");
                return false;
            }

            try
            {
                ExtractGZip(inputFile);

                await DataStore.CN.DataLogEntryAdd("GRAMPS file decompressing complete").ConfigureAwait(false);
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                DataStore.CN.NotifyError("Unauthorised Access exception when trying to acess file. " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// This routine is heavily customized to decompress GRAMPS whole of database export file
        /// (i.e. .gramps) files.
        /// </summary>
        /// <returns>
        /// Flag indicating success or not.
        /// </returns>
        public async Task<bool> DecompressTAR()
        {
            await DataStore.CN.DataLogEntryAdd("Decompressing GRAMPS TAR file").ConfigureAwait(false);

            // Check arguments
            if (!DataStore.AD.CurrentInputStreamValid)
            {
                DataStore.CN.NotifyAlert("The input file is invalid");
                return false;
            }

            Stream originalFileStream = DataStore.AD.CurrentInputStream;

            // open the gzip and extract the tar file
            await DataStore.CN.DataLogEntryAdd("Decompressing individual TAR files").ConfigureAwait(false);
            await DataStore.CN.DataLogEntryAdd("This will take a while...").ConfigureAwait(false);

            await DataStore.CN.DataLogEntryAdd("Un Tar ing file").ConfigureAwait(false);

            using (Stream stream = new GZipInputStream(originalFileStream))
            {
                using (TarInputStream tarIn = new TarInputStream(stream, System.Text.Encoding.ASCII))
                {
                    // DO NOT AWAIT as causes thread blocking await
                    await ExtractTarArchive(tarIn).ConfigureAwait(false);
                }
            }

            await DataStore.CN.DataLogEntryDelete().ConfigureAwait(false);
            return true;
        }
    }
}