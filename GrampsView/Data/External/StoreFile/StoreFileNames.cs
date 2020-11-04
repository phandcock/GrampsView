

namespace GrampsView.Data
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;
    using System.Diagnostics.Contracts;

    using System;

    using Xamarin.Essentials;

    /// <summary>
    /// Common file handling routines.
    /// </summary>
    public static class StoreFileNames 
    {
        /// <summary>
        /// Was the file modified since the last datetime saved?
        /// </summary>
        /// <param name="settingsKey">
        /// The settings key.
        /// </param>
        /// <param name="filenameToCheck">
        /// The filename to check.
        /// </param>
        /// <returns>
        /// True if the file was modified since last time.
        /// </returns>
        public static bool FileModifiedSinceLastSaveAsync(string settingsKey, FileInfoEx fileToCheck)
        {
            if (fileToCheck is null)
            {
                throw new ArgumentNullException(nameof(fileToCheck));
            }

            // Check for file exists
            if (!fileToCheck.Valid)
            {
                return false;
            }

            try
            {
                DateTime fileDateTime = FileGetDateTimeModified(fileToCheck);

                // Need to reparse it so the ticks ar ethe same
                fileDateTime = DateTime.Parse(fileDateTime.ToString(System.Globalization.CultureInfo.CurrentCulture), System.Globalization.CultureInfo.CurrentCulture);

                // Save a fresh copy if null so we can load next time
                string oldDateTime = Preferences.Get(settingsKey, string.Empty);

                if (string.IsNullOrEmpty(oldDateTime))
                {
                    Preferences.Set(settingsKey, fileDateTime.ToString(System.Globalization.CultureInfo.CurrentCulture));

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
                Preferences.Remove(settingsKey);

                DataStore.CN.NotifyException("FileModifiedSinceLastSaveAsync", ex);
                throw;
            }
        }

        /// <summary>
        /// Saves the datetime the file was last modified in System Settings.
        /// </summary>
        /// <param name="settingsKey">
        /// The settings key.
        /// </param>
        /// <param name="filenameToCheck">
        /// The filename to check.
        /// </param>
        public static void SaveFileModifiedSinceLastSave(string settingsKey, FileInfoEx filename)
        {
            Contract.Assert(filename != null);

            Preferences.Set(settingsKey, filename.FInfo.LastWriteTimeUtc.ToString(System.Globalization.CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// Indexes the file get date time modified.
        /// </summary>
        /// <returns>
        /// </returns>
        private static DateTime FileGetDateTimeModified(FileInfoEx fileToCheck)
        {
            try
            {
                if (fileToCheck.Valid)
                {
                    return fileToCheck.FInfo.LastWriteTimeUtc;
                }

                return new DateTime();
            }
            catch (Exception ex)
            {
                DataStore.CN.NotifyException("Exception while checking FileGetDateTimeModified for =" + fileToCheck.FInfo.FullName, ex);

                throw;
            }
        }
    }
}