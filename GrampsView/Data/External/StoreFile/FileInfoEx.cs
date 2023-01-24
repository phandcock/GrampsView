// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Repository;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using System.Diagnostics.Contracts;

namespace GrampsView.Data.External.StoreFile
{
    public class FileInfoEx : ObservableObject, IFileInfoEx
    {
        public FileInfoEx()
        {
        }

        public FileInfoEx(string argFileName, FileInfo? argFInfo = null, string argRelativeFolder = "", bool argUseCurrentDataFolder = true)
        {
            Contract.Requires(argFileName != null, $"argFileName can not be null");

            Contract.Requires(!argUseCurrentDataFolder && !string.IsNullOrEmpty(argRelativeFolder), $"argUseCurrentDataFolder is false and argRelativeFolder is null");

            FInfo = argFInfo;

            // Use cache base if currentdatafolder not allowed
            if (!argUseCurrentDataFolder && !string.IsNullOrEmpty(argRelativeFolder))
            {
                createFilePath(argFileName, new DirectoryInfo(FileSystem.Current.CacheDirectory));
                return;
            }

            // Standard call
            createFilePath(argFileName, new DirectoryInfo(Path.Combine(DataStore.Instance.AD.CurrentDataFolder.FolderasDirInfo.FullName, argRelativeFolder)));
        }

        public bool Exists
        {
            get
            {
                if (FInfo != null)
                {
                    // Use File.Exists as FileInfo.Exists only changed when FileInfo created
                    return File.Exists(FInfo.FullName);
                }

                return false;
            }
        }

        public FileInfo FInfo { get; set; }

        public bool Valid => FInfo != null && FInfo.Exists && FInfo.FullName != null;

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
        public static IFileInfoEx GetStorageFile(string relativeFilePath)
        {
            IFileInfoEx resultFile = new FileInfoEx(argRelativeFolder: Path.GetDirectoryName(relativeFilePath), argFileName: Path.GetFileName(relativeFilePath));

            // Validate the input
            if (relativeFilePath is null || string.IsNullOrEmpty(relativeFilePath))
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
                    if (Directory.Exists(Path.Combine(DataStore.Instance.AD.CurrentDataFolder.FolderAsString, Path.GetDirectoryName(relativeFilePath))))
                    {
                        FileInfo[] t = DataStore.Instance.AD.CurrentDataFolder.FolderasDirInfo.GetFiles(relativeFilePath);

                        if (t.Length > 0)
                        {
                            resultFile.FInfo = t[0];
                        }
                    }

                    return resultFile;
                }
                catch (FileNotFoundException ex)
                {
                    Ioc.Default.GetRequiredService<ILog>().DataLogEntryAdd(ex.Message + ex.FileName);

                    // default to a standard file marker
                }
                catch (Exception ex)
                {
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex.Message + relativeFilePath, ex);
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
                return Valid ? FInfo.LastWriteTimeUtc : new DateTime();
            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("Exception while checking FileGetDateTimeModified for =" + FInfo.FullName, ex);

                throw;
            }
        }

        /// <summary>
        /// Checks and creates the directory path to a file. The filename may be prepended by a path.
        /// </summary>
        /// <param name="argFileName">
        /// Path of the file. May be prepended by a path.
        /// </param>
        /// <param name="argBaseFolder">
        /// The base folder of the file.
        /// </param>
        private void createFilePath(string argFileName, DirectoryInfo? argBaseFolder = null)
        {
            argBaseFolder ??= DataStore.Instance.AD.CurrentDataFolder.FolderasDirInfo;

            Contract.Assert(argFileName != null);

            // load the real file
            DirectoryInfo realPath = new(Path.Combine(argBaseFolder.FullName, Path.GetDirectoryName(argFileName)));

            FInfo = new FileInfo(Path.Combine(realPath.FullName, Path.GetFileName(argFileName)));

            if (realPath != null)
            {
                try
                {
                    // If the file exists the path exists
                    if (FInfo.Exists)
                    {
                        return;
                    }

                    // Create directory if required
                    if (!realPath.Exists)
                    {
                        realPath.Create();
                    }
                }
                catch (FileNotFoundException ex)
                {
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("FolderGetFile") { { "Message", ex.Message }, { "Filename", ex.FileName } });

                    // default to a standard file marker
                }
                catch (DirectoryNotFoundException ex)
                {
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("FolderGetFile,Directory not found when trying to create the file.  Perhaps the GPKG filename is too long?") { { "Message", ex.Message }, });

                    // default to a standard file marker
                }
                catch (Exception ex)
                {
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex.Message + argFileName, ex);
                    throw;
                }
            }
            else
            {
                // TODO Handle this
            }
        }
    }
}