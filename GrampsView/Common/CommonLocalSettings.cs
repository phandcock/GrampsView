namespace GrampsView.Common
{
    using GrampsView.Data;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Repository;

    using Microsoft.Extensions.DependencyInjection;

    using SharedSharp.Common;

    using SharedSharpNu.Interfaces;

    using System;
    using System.Diagnostics.Contracts;

    public class CommonLocalSettings : SharedSharpSettings
    {
        public static bool BirthdayShowOnlyLivingFlag
        {
            get
            {
                return DataStore.Instance.ES.PreferencesGet("BirthdayShowOnlyLivingFlag", false);
            }

            set
            {
                DataStore.Instance.ES.PreferencesSet("BirthdayShowOnlyLivingFlag", value);
            }
        }

        public static string NoteEmailAddress
        {
            get
            {
                return DataStore.Instance.ES.PreferencesGet("NoteEmailAddress", DV.HeaderDV.HeaderDataModel.GResearcherEmail);
            }

            set
            {
                DataStore.Instance.ES.PreferencesSet("NoteEmailAddress", value);
            }
        }

        public static bool SortHLinkCollections
        {
            get
            {
                return DataStore.Instance.ES.PreferencesGet("SortHLinkCollections", false);
            }

            set
            {
                DataStore.Instance.ES.PreferencesSet("SortHLinkCollections", value);
            }
        }

        public static bool UseFirstImageFlag
        {
            get
            {
                return DataStore.Instance.ES.PreferencesGet("UseFirstImageFlag", false);
            }

            set
            {
                DataStore.Instance.ES.PreferencesSet("UseFirstImageFlag", value);
            }
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
                string oldDateTime = DataStore.Instance.ES.PreferencesGet(argSettingsKey, string.Empty);

                if (string.IsNullOrEmpty(oldDateTime))
                {
                    DataStore.Instance.ES.PreferencesSet(argSettingsKey, fileDateTime.ToString(System.Globalization.CultureInfo.CurrentCulture));

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
                DataStore.Instance.ES.PreferencesRemove(argSettingsKey);

                App.Current.Services.GetService<IErrorNotifications>().NotifyException("FileModifiedSinceLastSaveAsync", ex);
                throw;
            }
        }

        /// <summary>
        /// Saves the datetime the file was last modified in System Settings.
        /// </summary>
        public static void SaveLastWriteToSettings(IFileInfoEx argFileInfoEx, string argSettingsKey)
        {
            Contract.Assert(argFileInfoEx != null);

            Contract.Assert(argSettingsKey != string.Empty);

            DataStore.Instance.ES.PreferencesSet(argSettingsKey, argFileInfoEx.FInfo.LastWriteTimeUtc.ToString(System.Globalization.CultureInfo.CurrentCulture));
        }

        public static void SetReloadDatabase()
        {
            // Remove the old dateTime stamps so the files get reloaded even if they have been seen before
            DataStore.Instance.ES.PreferencesRemove(Constants.SettingsGPKGFileLastDateTimeModified);
            DataStore.Instance.ES.PreferencesRemove(Constants.SettingsGPRAMPSFileLastDateTimeModified);
            DataStore.Instance.ES.PreferencesRemove(Constants.SettingsXMLFileLastDateTimeModified);

            DataStore.Instance.DS.IsDataLoaded = false;

            CommonLocalSettings.DataSerialised = false;
        }
    }
}