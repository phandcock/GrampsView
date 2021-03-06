﻿namespace GrampsView.Data
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    using Xamarin.Essentials;

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
                DataStore.Instance.CN.NotifyError(new ErrorInfo("Relative folder path names are not allowed.") { { "Path", path } });
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
                        new FilePickerFileType(
                            new Dictionary<DevicePlatform,
                            IEnumerable<string>>
                                {
                                    //{ DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // TODO add these or general UTType values
                                    { DevicePlatform.Android, new[] { "application/octet-stream" } },
                                    { DevicePlatform.UWP, new[] { ".gpkg",".gramps" } },
                                    //{ DevicePlatform.macOS, new[] { "cbr" } }, // TODO add these or general UTType values
                                }
                            );

                var options = new PickOptions
                {
                    PickerTitle = "Please select a Gramps input file",
                    FileTypes = customFileType,
                };

                FileResult result = await FilePicker.PickAsync(options);

                if (result == null)
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
                DataStore.Instance.CN.NotifyException("Exception in PickCurrentInputFile", ex);

                throw;
            }

            return true;
        }
    }
}