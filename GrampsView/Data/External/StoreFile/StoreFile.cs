namespace GrampsView.Data
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
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
        ///// <summary>
        ///// Initializes a new instance of the <see cref="StoreFile"/> class.
        ///// </summary>
        ///// <param name="iocCommonProgress">
        ///// The ioc common progress.
        ///// </param>
        //public StoreFile()
        //{
        //}

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
            if (relativeFilePath == "handcocks\\cemetaries\\Grave - Ethel handcock.jpg")
            {
            }

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
            DirectoryInfo tt = DataStore.Instance.AD.CurrentDataFolder;
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
                    await DataStore.Instance.CN.DataLogEntryAdd(ex.Message + ex.FileName).ConfigureAwait(false);

                    // default to a standard file marker
                }
                catch (Exception ex)
                {
                    await DataStore.Instance.CN.NotifyException(ex.Message + relativeFilePath, ex);
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
            await DataStore.Instance.CN.DataLogEntryAdd("Deleting existing datastorage").ConfigureAwait(false);
            {
                try
                {
                    DirectoryInfo t = DataStore.Instance.AD.CurrentDataFolder;

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
                    await DataStore.Instance.CN.NotifyException("DataStorageInitialiseAsync", ex);
                    throw;
                }
            }

            await DataStore.Instance.CN.DataLog.Remove().ConfigureAwait(false);

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
            await DataStore.Instance.CN.DataLogEntryAdd("Decompressing GRAMPS GZIP file").ConfigureAwait(false);

            // Check arguments
            if (inputFile == null)
            {
                await DataStore.Instance.CN.NotifyError(new ErrorInfo("The input file is null"));
                return false;
            }

            try
            {
                ExtractGZip(inputFile);

                await DataStore.Instance.CN.DataLogEntryReplace("GRAMPS GSZIP file decompress complete").ConfigureAwait(false);
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                ErrorInfo t = new ErrorInfo("Unauthorised Access exception when trying to acess file")
                    {
                        { "Exception Message ", ex.Message },
                    };

                await DataStore.Instance.CN.NotifyError(t);
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
            await DataStore.Instance.CN.DataLogEntryAdd("Decompressing GRAMPS TAR files").ConfigureAwait(false);

            // Check arguments
            if (!DataStore.Instance.AD.CurrentInputStreamValid)
            {
                await DataStore.Instance.CN.NotifyAlert("The input file is invalid");
                return false;
            }

            Stream originalFileStream = DataStore.Instance.AD.CurrentInputStream;

            // open the gzip and extract the tar file
            //await DataStore.Instance.CN.DataLogEntryAdd("Decompressing individual TAR files").ConfigureAwait(false);
            //await DataStore.Instance.CN.DataLogEntryAdd("This will take a while...").ConfigureAwait(false);

            //await DataStore.Instance.CN.DataLogEntryAdd("UnTaring file").ConfigureAwait(false);

            using (Stream stream = new GZipInputStream(originalFileStream))
            {
                using (TarInputStream tarIn = new TarInputStream(stream, System.Text.Encoding.ASCII))
                {
                    // DO NOT AWAIT as causes thread blocking await
                    await ExtractTarArchive(tarIn).ConfigureAwait(false);
                }
            }

            await DataStore.Instance.CN.DataLogEntryReplace("UnTaring of files complete").ConfigureAwait(false);
            return true;
        }
    }
}