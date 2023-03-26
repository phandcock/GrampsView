// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common.Interfaces;
using GrampsView.Data.DataView;
using GrampsView.Data.Repository;

using SharedSharp.Errors.Interfaces;

using System.Diagnostics.Contracts;

namespace GrampsView.Common
{
    public class CommonLocalSettings : SharedSharpSettings
    {
        public static bool BirthdayShowOnlyLivingFlag
        {
            get => Preferences.Default.Get("BirthdayShowOnlyLivingFlag", false);

            set => Preferences.Default.Set("BirthdayShowOnlyLivingFlag", value);
        }

        public static string NoteEmailAddress
        {
            get => Preferences.Default.Get("NoteEmailAddress", DV.HeaderDV.HeaderDataModel.GResearcherEmail);

            set => Preferences.Default.Set("NoteEmailAddress", value);
        }

        public static bool SortHLinkCollections
        {
            get => Preferences.Default.Get("SortHLinkCollections", false);

            set => Preferences.Default.Set("SortHLinkCollections", value);
        }

        public static bool UseFirstImageFlag
        {
            get => Preferences.Default.Get("UseFirstImageFlag", false);

            set => Preferences.Default.Set("UseFirstImageFlag", value);
        }

        /// <summary>
        /// Was the file modified since the last datetime saved?
        /// </summary>
        /// <returns>
        /// True if the file was modified since last time.
        /// </returns>
        public static bool ModifiedComparedToSettings(IFileInfoEx argFileInfoEx, string argSettingsKey)
        {
            Contract.Assert(argFileInfoEx != null);

            Contract.Assert(argSettingsKey != string.Empty);

            // Check for file exists
            if (!argFileInfoEx.Valid)
            {
                return false;
            }

            try
            {
                DateTime fileDateTime = argFileInfoEx.FileGetDateTimeModified();

                // Need to reparse it so the ticks are the same
                fileDateTime = DateTime.Parse(fileDateTime.ToString(System.Globalization.CultureInfo.CurrentCulture), System.Globalization.CultureInfo.CurrentCulture);

                // Save a fresh copy if null so we can load next time
                string oldDateTime = Preferences.Default.Get(argSettingsKey, string.Empty);

                if (string.IsNullOrEmpty(oldDateTime))
                {
                    Preferences.Default.Set(argSettingsKey, fileDateTime.ToString(System.Globalization.CultureInfo.CurrentCulture));

                    // No previous settings entry so do the load (it might be the FirstRun)
                    return true;
                }
                else
                {
                    DateTime settingsStoredDateTime;
                    settingsStoredDateTime = DateTime.Parse(oldDateTime, System.Globalization.CultureInfo.CurrentCulture);

                    int t = fileDateTime.CompareTo(settingsStoredDateTime);
                    return t > 0;
                }
            }
            catch (Exception ex)
            {
                Preferences.Default.Remove(argSettingsKey);

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("FileModifiedSinceLastSaveAsync", ex);
            }
        }

        /// <summary>
        /// Saves the datetime the file was last modified in System Settings.
        /// </summary>
        public static void SaveLastWriteToSettings(IFileInfoEx argFileInfoEx, string argSettingsKey)
        {
            Contract.Assert(argFileInfoEx != null);

            Contract.Assert(argSettingsKey != string.Empty);

            Preferences.Default.Set(argSettingsKey, argFileInfoEx.FInfo.LastWriteTimeUtc.ToString(System.Globalization.CultureInfo.CurrentCulture));
        }

        public static void SetReloadDatabase()
        {
            // Remove the old dateTime stamps so the files get reloaded even if they have been seen before
            Preferences.Default.Remove(Constants.SettingsGPKGFileLastDateTimeModified);
            Preferences.Default.Remove(Constants.SettingsGPRAMPSFileLastDateTimeModified);
            Preferences.Default.Remove(Constants.SettingsXMLFileLastDateTimeModified);

            DataStore.Instance.DS.IsDataLoaded = false;

            DataSerialised = false;
        }
    }
}