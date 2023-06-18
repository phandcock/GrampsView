// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common.Interfaces;
using GrampsView.Data.Repository;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using System.Diagnostics.Contracts;
using System.Text.Json.Serialization;

namespace GrampsView.Common.CustomClasses
{
    public class FileInfoEx : ObservableObject, IFileInfoEx
    {
        [JsonIgnore]
        private FileInfo _FInfo = null;

        public FileInfoEx()
        {
        }

        public FileInfoEx(string argFileName, string argRelativeFolder = "")
        {
            Contract.Requires(argFileName != null, $"argFileName can not be null");

            if (!string.IsNullOrEmpty(argFileName))
            {
                argFileName = Path.Combine(argRelativeFolder, argFileName);
            }

            // Standard call
            CreateFilePath(argFileName);
        }

        public FileInfoEx(string argFilePath)
        {
            Contract.Requires(argFilePath != null, $"argFilePath can not be null");

            // Standard call
            CreateFilePath(argFilePath);
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

        public FileInfo FInfo
        {
            get
            {
                if (_FInfo != null)
                {
                    return _FInfo;
                }

                if (!string.IsNullOrEmpty(RelativeFilePath))
                {
                    _FInfo = new FileInfo(DataStore.Instance.AD.CurrentDataFolder.GetAbsoluteFilePath(RelativeFilePath));

                    return _FInfo;
                }

                return null;
            }
        }

        public string GetAbsoluteFilePath
        {
            get
            {
                return DataStore.Instance.AD.CurrentDataFolder.GetAbsoluteFilePath(RelativeFilePath);
            }
        }

        public bool Valid => !String.IsNullOrEmpty(RelativeFilePath);
        public string RelativeFilePath { get; set; } = string.Empty;

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

                return new DateTime();
            }
        }

        /// <summary>
        /// Checks and creates the directory path to a file. The filename may be prepended by a path.
        /// </summary>
        /// <param name="argFileName">
        /// Path of the file. May be prepended by a path.
        /// </param>
        private void CreateFilePath(string argFileName)
        {
            Contract.Assert(argFileName != null);

            // load the real file
            DirectoryInfo realPath = new(Path.Combine(DataStore.Instance.AD.CurrentDataFolder.FolderasDirInfo.FullName, Path.GetDirectoryName(argFileName)));

            //FInfo = new FileInfo(Path.Combine(realPath.FullName, Path.GetFileName(argFileName)));

            if (realPath != null)
            {
                try
                {
                    // Create directory if required
                    if (!realPath.Exists)
                    {
                        realPath.Create();
                    }

                    RelativeFilePath = argFileName;
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
                }
            }
            else
            {
                // TODO Handle this
            }
        }
    }
}