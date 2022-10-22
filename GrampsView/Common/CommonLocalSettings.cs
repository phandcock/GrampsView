using GrampsView.Data;
using GrampsView.Data.DataView;
using GrampsView.Data.Repository;

using Microsoft.Extensions.DependencyInjection;

using SharedSharp.Common;
using SharedSharp.Errors.Interfaces;

using System;
using System.Diagnostics.Contracts;

using Xamarin.Essentials.Interfaces;

namespace GrampsView.Common
{
    public class CommonLocalSettings : SharedSharpSettings
    {
        public static bool BirthdayShowOnlyLivingFlag
        {
            get => App.Current.Services.GetService<IPreferences>().Get("BirthdayShowOnlyLivingFlag", false);

            set => App.Current.Services.GetService<IPreferences>().Set("BirthdayShowOnlyLivingFlag", value);
        }

        public static string NoteEmailAddress
        {
            get => App.Current.Services.GetService<IPreferences>().Get("NoteEmailAddress", DV.HeaderDV.HeaderDataModel.GResearcherEmail);

            set => App.Current.Services.GetService<IPreferences>().Set("NoteEmailAddress", value);
        }

        public static bool SortHLinkCollections
        {
            get => App.Current.Services.GetService<IPreferences>().Get("SortHLinkCollections", false);

            set => App.Current.Services.GetService<IPreferences>().Set("SortHLinkCollections", value);
        }

        public static bool UseFirstImageFlag
        {
            get => App.Current.Services.GetService<IPreferences>().Get("UseFirstImageFlag", false);

            set => App.Current.Services.GetService<IPreferences>().Set("UseFirstImageFlag", value);
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
                string oldDateTime = App.Current.Services.GetService<IPreferences>().Get(argSettingsKey, string.Empty);

                if (string.IsNullOrEmpty(oldDateTime))
                {
                    App.Current.Services.GetService<IPreferences>().Set(argSettingsKey, fileDateTime.ToString(System.Globalization.CultureInfo.CurrentCulture));

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
                App.Current.Services.GetService<IPreferences>().Remove(argSettingsKey);

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

            App.Current.Services.GetService<IPreferences>().Set(argSettingsKey, argFileInfoEx.FInfo.LastWriteTimeUtc.ToString(System.Globalization.CultureInfo.CurrentCulture));
        }

        public static void SetReloadDatabase()
        {
            // Remove the old dateTime stamps so the files get reloaded even if they have been seen before
            App.Current.Services.GetService<IPreferences>().Remove(Constants.SettingsGPKGFileLastDateTimeModified);
            App.Current.Services.GetService<IPreferences>().Remove(Constants.SettingsGPRAMPSFileLastDateTimeModified);
            App.Current.Services.GetService<IPreferences>().Remove(Constants.SettingsXMLFileLastDateTimeModified);

            DataStore.Instance.DS.IsDataLoaded = false;

            CommonLocalSettings.DataSerialised = false;
        }
    }
}