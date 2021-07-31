namespace GrampsView.Data
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;

    using System;
    using System.Diagnostics.Contracts;
    using System.IO;

    using Xamarin.CommunityToolkit.ObjectModel;

    public class FileInfoEx : ObservableObject, IFileInfoEx
    {
        private FileInfo _FInfo;

        public FileInfoEx(FileInfo argFInfo = null, string argSystemSettingsKey = null, DirectoryInfo argRelativeFolder = null, string argFileName = null)
        {
            FInfo = argFInfo;

            if (argSystemSettingsKey != null)
            {
                SystemSettingsKey = argSystemSettingsKey;
            }

            if ((argRelativeFolder != null) || (argFileName != null))
            {
             

                MakeGetFile(argRelativeFolder, argFileName);
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

        public string RelativeDirectory
        {
            get; set;
        }

        public bool Valid
        {
            get
            {
                return (!(FInfo == null) && (FInfo.Exists));
            }
        }

        private string SystemSettingsKey
        {
            get; set;
        } = string.Empty;

        /// <summary>
        /// Was the file modified since the last datetime saved?
        /// </summary>
        /// <returns>
        /// True if the file was modified since last time.
        /// </returns>
        public bool ModifiedComparedToSettings()
        {
            Contract.Assert(FInfo != null);

            Contract.Assert(SystemSettingsKey != string.Empty);

            // Check for file exists
            if (!this.Valid)
            {
                return false;
            }

            try
            {
                DateTime fileDateTime = FileGetDateTimeModified();

                // Need to reparse it so the ticks are the same
                fileDateTime = DateTime.Parse(fileDateTime.ToString(System.Globalization.CultureInfo.CurrentCulture), System.Globalization.CultureInfo.CurrentCulture);

                // Save a fresh copy if null so we can load next time
                string oldDateTime = DataStore.Instance.ES.PreferencesGet(SystemSettingsKey, string.Empty);

                if (string.IsNullOrEmpty(oldDateTime))
                {
                    DataStore.Instance.ES.PreferencesSet(SystemSettingsKey, fileDateTime.ToString(System.Globalization.CultureInfo.CurrentCulture));

                    // No previous settings entry so do the load (it might be the FirstRun)
                    return true;
                }
                else
                {
                    DateTime settingsStoredDateTime;
                    settingsStoredDateTime = DateTime.Parse(oldDateTime, System.Globalization.CultureInfo.CurrentCulture);

                    int t = fileDateTime.CompareTo(settingsStoredDateTime);
                    if (t > 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                DataStore.Instance.ES.PreferencesRemove(SystemSettingsKey);

                DataStore.Instance.CN.NotifyException("FileModifiedSinceLastSaveAsync", ex);
                throw;
            }
        }

        /// <summary>
        /// Saves the datetime the file was last modified in System Settings.
        /// </summary>
        public void SaveLastWriteToSettings()
        {
            Contract.Assert(FInfo != null);

            Contract.Assert(SystemSettingsKey != string.Empty);

            DataStore.Instance.ES.PreferencesSet(SystemSettingsKey, FInfo.LastWriteTimeUtc.ToString(System.Globalization.CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// Indexes the file get date time modified.
        /// </summary>
        /// <returns>
        /// </returns>
        private DateTime FileGetDateTimeModified()
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

                    foreach (FileInfo item in t)
                    {
                        if (item.Name == Path.GetFileName(argFileName))
                        {
                            this.FInfo = item;
                        }
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