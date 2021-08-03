namespace GrampsView.Data
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;

    using System;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    public class FileInfoEx : ObservableObject, IFileInfoEx
    {
        private FileInfo _FInfo;

        public FileInfoEx()
        {
        }

        public FileInfoEx(string argFileName, FileInfo argFInfo = null, string argRelativeFolder = null, DirectoryInfo argBaseFolder = null, bool argUseCurrentDataFolder = false)
        {
            Contract.Requires(argFileName != null);

            FInfo = argFInfo;

            if (argRelativeFolder != null)
            {
                MakeGetFile(new DirectoryInfo(Path.Combine(DataStore.Instance.AD.CurrentDataFolder.Value.FullName, argRelativeFolder)), argFileName);
            }

            if (argUseCurrentDataFolder)
            {
                MakeGetFile(DataStore.Instance.AD.CurrentDataFolder.Value, argFileName);
            }

            if (argBaseFolder != null)
            {
                MakeGetFile(argBaseFolder, argFileName);
            }
        }

        public bool Exists
        {
            get
            {
                if (FInfo != null)
                {
                    return FInfo.Exists;
                }

                return false;
            }
        }

        public FileInfo FInfo
        {
            get
            {
                return _FInfo;
            }
            set
            {
                SetProperty(ref _FInfo, value);
            }
        }

        public bool Valid
        {
            get
            {
                return (!(FInfo == null) && (FInfo.Exists));
            }
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
        /// TODO Check if same as MakeGetFile
        public async static Task<IFileInfoEx> GetStorageFileAsync(string relativeFilePath)
        {
            IFileInfoEx resultFile = new FileInfoEx(argRelativeFolder: Path.GetDirectoryName(relativeFilePath), argFileName: Path.GetFileName(relativeFilePath));

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

            // Load the real file
            if (DataStore.Instance.AD.CurrentDataFolder.Valid)
            {
                try
                {
                    if (Directory.Exists(Path.Combine(DataStore.Instance.AD.CurrentDataFolder.Path, Path.GetDirectoryName(relativeFilePath))))
                    {
                        FileInfo[] t = DataStore.Instance.AD.CurrentDataFolder.Value.GetFiles(relativeFilePath);

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
                    DataStore.Instance.CN.NotifyException(ex.Message + relativeFilePath, ex);
                    throw;
                }
            }

            return resultFile;
        }

        /// <summary>
        /// Indexes the file get date time modified.
        /// </summary>
        /// <returns>
        /// </returns>
        public DateTime FileGetDateTimeModified()
        {
            try
            {
                if (Valid)
                {
                    return FInfo.LastWriteTimeUtc;
                }

                return new DateTime();
            }
            catch (Exception ex)
            {
                DataStore.Instance.CN.NotifyException("Exception while checking FileGetDateTimeModified for =" + FInfo.FullName, ex);

                throw;
            }
        }

        private void MakeGetFile(DirectoryInfo argBaseFolder = null, string argFileName = null)
        {
            if (argBaseFolder is null)
            {
                argBaseFolder = DataStore.Instance.AD.CurrentDataFolder.Value;
            }

            Contract.Assert(argFileName != null);

            // TODO Handle relative paths

            // Check for relative path
            // TODO Why?
            //if (!StoreFileUtility.IsRelativeFilePathValid(argFileName))
            //{
            //    return new FileInfoEx();
            //}

            // load the real file
            DirectoryInfo realPath = new DirectoryInfo(Path.Combine(argBaseFolder.FullName, Path.GetDirectoryName(argFileName)));

            if (realPath != null)
            {
                try
                {
                    FileInfo[] t = realPath.GetFiles();

                    this.FInfo = null;

                    foreach (FileInfo item in t)
                    {
                        if (item.Name == Path.GetFileName(argFileName))
                        {
                            this.FInfo = item;
                        }
                    }

                    if (FInfo == null)
                    {
                        FInfo = new FileInfo(Path.Combine(argBaseFolder.FullName, argFileName));
                    }
                }
                catch (FileNotFoundException ex)
                {
                    DataStore.Instance.CN.NotifyError(new ErrorInfo("FolderGetFile") { { "Message", ex.Message }, { "Filename", ex.FileName } });

                    // default to a standard file marker
                }
                catch (DirectoryNotFoundException ex)
                {
                    DataStore.Instance.CN.NotifyError(new ErrorInfo("FolderGetFile,Directory not found when deserialising the data.  Perhaps the GPKG filenames are too long?") { { "Message", ex.Message }, });

                    // default to a standard file marker
                }
                catch (Exception ex)
                {
                    DataStore.Instance.CN.NotifyException(ex.Message + argFileName, ex);
                    throw;
                }
            }
        }
    }
}