namespace GrampsView.Data
{
    using Microsoft.Extensions.DependencyInjection;

    using SharedSharp.Interfaces;

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

        public static FileStream FolderCreateFile(DirectoryInfo argBaseFolder, string argFileName)
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
                    App.Current.Services.GetService<IErrorNotifications>().DataLogEntryAdd(ex.Message + ex.FileName);

                    // default to a standard file marker
                }
                catch (Exception ex)
                {
                    App.Current.Services.GetService<IErrorNotifications>().NotifyException(ex.Message + argFileName, ex);
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
                    App.Current.Services.GetService<IErrorNotifications>().DataLogEntryAdd(ex.Message + ex.FileName);

                    // default to a standard file marker
                }
                catch (Exception ex)
                {
                    App.Current.Services.GetService<IErrorNotifications>().NotifyException(ex.Message + argFileName, ex);
                    throw;
                }
            }

            return false;
        }
    }
}