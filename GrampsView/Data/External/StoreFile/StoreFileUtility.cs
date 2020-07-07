//-----------------------------------------------------------------------
//
// Common routines for the CommonRoutines
//
// <copyright file="StoreFileUtility.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    using GrampsView.Data.Repository;
    using Plugin.FilePicker;

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
                DataStore.CN.NotifyError("Relative folder path names are not allowed." + path + ".");
                returnValue = false;
            }

            return returnValue;
        }

        // TODO WHen NetStandard 2.3 out then Path.MakeRelative
        public static string MakeRelativePath(string argPath)
        {
            if (DataStore.AD.CurrentDataFolder.FullName == argPath)
            {
                return string.Empty;
            }

            if (argPath.Length < DataStore.AD.CurrentDataFolder.FullName.Length)
            {
                return argPath;
            }

            if (argPath.Substring(0, DataStore.AD.CurrentDataFolder.FullName.Length) == DataStore.AD.CurrentDataFolder.FullName)
            {
                return argPath.Substring(DataStore.AD.CurrentDataFolder.FullName.Length, argPath.Length - DataStore.AD.CurrentDataFolder.FullName.Length);
            }

            return argPath;
        }

        /// <summary>
        /// Gets the CurrentDataFolder folder and sets the CurrentThumbnailFolder.
        /// </summary>
        /// <returns>
        /// True if a file was picked. False if not.
        /// </returns>
        public static async Task<bool> PickCurrentInputFile()
        {
            try
            {
                DataStore.AD.CurrentInputFile = await CrossFilePicker.Current.PickFile().ConfigureAwait(false);
                if (DataStore.AD.CurrentInputFile == null)
                {
                    return false; // user canceled file picking
                }

                Debug.WriteLine("Picked file name is: " + DataStore.AD.CurrentInputFile.FileName);
                // TODO Add platform specific fiel type checking
            }

            // TODO fix this. Fail and force reload next time.
            catch (Exception ex)
            {
                DataStore.CN.NotifyException("Exception in GetInputFolder", ex);

                throw;
            }

            return true;
        }
    }
}