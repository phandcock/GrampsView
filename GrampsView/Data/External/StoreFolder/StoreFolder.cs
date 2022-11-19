using GrampsView.Data.External.StoreFile;

using SharedSharp.Errors.Interfaces;
using SharedSharp.Logging.Interfaces;

namespace GrampsView.Data.External.StoreFolder
{
    public partial class StoreFolder : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreFolder"/> class.
        /// </summary>
        public StoreFolder()
        {
        }

        public static FileStream? FolderCreateFile(DirectoryInfo argBaseFolder, string argFileName)
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
                    Ioc.Default.GetService<ILog>().DataLogEntryAdd(ex.Message + ex.FileName);

                    // default to a standard file marker
                }
                catch (Exception ex)
                {
                    Ioc.Default.GetService<IErrorNotifications>().NotifyException(ex.Message + argFileName, ex, null);
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
        public static Task<bool> FolderFileExistsAsync(DirectoryInfo argBaseFolder, string argFileName)
        {
            // Check for relative path
            if (!StoreFileUtility.IsRelativeFilePathValid(argFileName))
            {
                return Task.FromResult(false);
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
                            return Task.FromResult(true);
                        }
                    }
                }
                catch (FileNotFoundException ex)
                {
                    Ioc.Default.GetService<ILog>().DataLogEntryAdd(ex.Message + ex.FileName);

                    // default to a standard file marker
                }
                catch (Exception ex)
                {
                    Ioc.Default.GetService<IErrorNotifications>().NotifyException(ex.Message + argFileName, ex, null);
                    throw;
                }
            }

            return Task.FromResult(false);
        }
    }
}