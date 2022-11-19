using GrampsView.Data.Repository;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using System.Diagnostics;



namespace GrampsView.Data.External.StoreFile
{
    /// <summary>
    /// Various common routines.
    /// </summary>
    public static class StoreFileUtility
    {
        /// <summary>
        /// Splits a string by the path. it assumes that the folder markers is a "/" symbol and
        /// replaces '/' with '\'.
        /// </summary>
        /// <param name="inputFile">
        /// The file path.
        /// </param>
        /// <returns>
        /// filename and the name of the next folder up.
        /// </returns>
        public static string CleanFilePath(string inputFile)
        {
            return Path.Combine(Path.GetDirectoryName(inputFile), Path.GetFileName(inputFile));
        }

        /// <summary>
        /// Checks the path filename to see if it is valid.
        /// </summary>
        /// <param name="path">
        /// the relative path to the file.
        /// </param>
        /// returnValue = false;
        public static bool IsRelativeFilePathValid(string path)
        {
            bool returnValue = true;

            // Check for null path
            if (string.IsNullOrEmpty(path))
            {
                returnValue = false;
            }

            // Is the path a relative path? loop through path and create folders as we go
            string[] folderNames = Path.GetDirectoryName(path).Split('\\');

            bool aFail = false;
            for (int i = 0; i < folderNames.Length; i++)
            {
                // check for relative paths
                if (folderNames[i] == "..")
                {
                    aFail = true;
                }
            }

            if (aFail)
            {
                Ioc.Default.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("Relative folder path names are not allowed.") { { "Path", path } });
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary>
        /// Gets the CurrentDataFolder folder
        /// </summary>
        /// <returns>
        /// True if a file was picked. False if not.
        /// </returns>
        public static async Task<bool> PickCurrentInputFile()
        {
            try
            {
                FilePickerFileType customFileType =
                        new(
                            new Dictionary<DevicePlatform,
                            IEnumerable<string>>
                                {
                                    //{ DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // TODO add these or general UTType values
                                    { DevicePlatform.Android, new[] { "application/octet-stream" } },
                                    { DevicePlatform.WinUI, new[] { ".gpkg",".gramps" } },
                                    //{ DevicePlatform.macOS, new[] { "cbr" } }, // TODO add these or general UTType values
                                }
                            );

                PickOptions options = new()
                {
                    PickerTitle = "Please select a Gramps input file",
                    FileTypes = customFileType,
                };

                FileResult result = await FilePicker.PickAsync(options);

                if (result is null)
                {
                    return false; // user canceled file picking
                }

                Debug.WriteLine("Picked file name is: " + result.FileName);

                DataStore.Instance.AD.CurrentInputStream = await result.OpenReadAsync();

                DataStore.Instance.AD.CurrentInputStreamPath = result.FullPath;
            }

            // TODO fix this. Fail and force reload next time.
            catch (Exception ex)
            {
                Ioc.Default.GetService<IErrorNotifications>().NotifyException("Exception in PickCurrentInputFile", ex, null);

                throw;
            }

            return true;
        }
    }
}