namespace GrampsView.Common
{
    using GrampsView.Data;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Repository;

    using Microsoft.Extensions.DependencyInjection;

    using SharedSharp.Errors;

    using System;
    using System.Diagnostics.Contracts;

    public static class CommonLocalSettings
    {
        // private const string SettingsAppTheme = "ApplicationTheme";

        //public static OSAppTheme ApplicationTheme
        //{
        //    get
        //    {
        //        int _settingsAppTheme = DataStore.Instance.ES.PreferencesGet(SettingsAppTheme, 0);

        // switch (_settingsAppTheme) { case 1: { return OSAppTheme.Dark; }

        // case 2: { return OSAppTheme.Light; }

        // default: { return OSAppTheme.Unspecified; } } }

        // set { switch (value) { case OSAppTheme.Dark: {
        // DataStore.Instance.ES.PreferencesSet(SettingsAppTheme, 1); break; }

        // case OSAppTheme.Light: { DataStore.Instance.ES.PreferencesSet(SettingsAppTheme, 2);
        // break; }

        //            default:
        //                {
        //                    DataStore.Instance.ES.PreferencesSet(SettingsAppTheme, 3);
        //                    break;
        //                }
        //        }
        //    }
        //}

        /// <summary>
        /// Gets a value indicating whether needs the database reload.
        /// </summary>
        /// <returns>
        /// </returns>
        //public static bool DatabaseReloadNeeded
        //{
        //    get
        //    {
        //        if (DatabaseVersion < CommonConstants.GrampsViewDatabaseVersion)
        //        {
        //            // ClearPreferences();
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Gets or sets the database version.
        ///// </summary>
        ///// <value>
        ///// The database version.
        ///// </value>
        //public static int DatabaseVersion
        //{
        //    get
        //    {
        //        int localGrampsViewDatabaseVersion = DataStore.Instance.ES.PreferencesGet(CommonConstants.SettingsGrampsViewDatabaseVersion, int.MinValue);

        // if (localGrampsViewDatabaseVersion == int.MinValue) { // If the Setting is not defined
        // then assume the database has not been loaded so // the version number is set to MinValue
        // to force load return int.MinValue; }

        // return localGrampsViewDatabaseVersion; }

        //    set
        //    {
        //        DataStore.Instance.ES.PreferencesSet("GrampsViewDatabaseVersion", value);
        //    }
        //}

        ///// <summary>
        ///// Gets or sets a value indicating whether [data serialised].
        ///// </summary>
        ///// <value>
        ///// <c> true </c> if [data serialised]; otherwise, <c> false </c>.
        ///// </value>
        //public static bool DataSerialised
        //{
        //    get
        //    {
        //        return DataStore.Instance.ES.PreferencesGet("SerialisedData", false);
        //    }

        //    set
        //    {
        //        DataStore.Instance.ES.PreferencesSet("SerialisedData", value);
        //    }
        //}

        //public static bool FirstRunDisplay
        //{
        //    get
        //    {
        //        return DataStore.Instance.ES.PreferencesGet("FirstRunDisplay", false);
        //    }

        //    set
        //    {
        //        DataStore.Instance.ES.PreferencesSet("FirstRunDisplay", value);
        //    }
        //}

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
            DataStore.Instance.ES.PreferencesRemove(CommonConstants.SettingsGPKGFileLastDateTimeModified);
            DataStore.Instance.ES.PreferencesRemove(CommonConstants.SettingsGPRAMPSFileLastDateTimeModified);
            DataStore.Instance.ES.PreferencesRemove(CommonConstants.SettingsXMLFileLastDateTimeModified);

            DataStore.Instance.DS.IsDataLoaded = false;

            SharedSharp.Misc.LocalSettings.DataSerialised = false;
        }
    }
}