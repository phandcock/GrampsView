namespace GrampsView.Data
{
    using GrampsView.Data.Repository;

    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    public partial class StoreFolder : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreFolder"/> class.
        /// </summary>
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

            // Load the real file
            if (argBaseFolder != null)
            {
                try
                {
                    FileStream tt = File.Create(Path.Combine(argBaseFolder.FullName, argFileName));

                    return tt;
                }
                catch (FileNotFoundException ex)
                {
                    await DataStore.Instance.CN.DataLogEntryAdd(ex.Message + ex.FileName).ConfigureAwait(false);

                    // default to a standard file marker
                }
                catch (Exception ex)
                {
                    DataStore.Instance.CN.NotifyException(ex.Message + argFileName, ex);
                    throw;
                }
            }

            return null;
        }

        /// <summary>
        /// Get the StorageFile of the file.
        /// </summary>
        /// <param name="argBaseFolder">
        /// Base Folder to start checking
        /// </param>
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

            // Load the real file
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
                    await DataStore.Instance.CN.DataLogEntryAdd(ex.Message + ex.FileName).ConfigureAwait(false);

                    // default to a standard file marker
                }
                catch (Exception ex)
                {
                    DataStore.Instance.CN.NotifyException(ex.Message + argFileName, ex);
                    throw;
                }
            }

            return false;
        }
    }
}