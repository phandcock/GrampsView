using GrampsView.Data;
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
            get => Ioc.Default.GetService<IPreferences>().Get("BirthdayShowOnlyLivingFlag", false);

            set => Ioc.Default.GetService<IPreferences>().Set("BirthdayShowOnlyLivingFlag", value);
        }

        public static string NoteEmailAddress
        {
            get => Ioc.Default.GetService<IPreferences>().Get("NoteEmailAddress", DV.HeaderDV.HeaderDataModel.GResearcherEmail);

            set => Ioc.Default.GetService<IPreferences>().Set("NoteEmailAddress", value);
        }

        public static bool SortHLinkCollections
        {
            get => Ioc.Default.GetService<IPreferences>().Get("SortHLinkCollections", false);

            set => Ioc.Default.GetService<IPreferences>().Set("SortHLinkCollections", value);
        }

        public static bool UseFirstImageFlag
        {
            get => Ioc.Default.GetService<IPreferences>().Get("UseFirstImageFlag", false);

            set => Ioc.Default.GetService<IPreferences>().Set("UseFirstImageFlag", value);
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
                string oldDateTime = Ioc.Default.GetService<IPreferences>().Get(argSettingsKey, string.Empty);

                if (string.IsNullOrEmpty(oldDateTime))
                {
                    Ioc.Default.GetService<IPreferences>().Set(argSettingsKey, fileDateTime.ToString(System.Globalization.CultureInfo.CurrentCulture));

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
                Ioc.Default.GetService<IPreferences>().Remove(argSettingsKey);

                Ioc.Default.GetService<IErrorNotifications>().NotifyException("FileModifiedSinceLastSaveAsync",ex,null);
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

            Ioc.Default.GetService<IPreferences>().Set(argSettingsKey, argFileInfoEx.FInfo.LastWriteTimeUtc.ToString(System.Globalization.CultureInfo.CurrentCulture));
        }

        public static void SetReloadDatabase()
        {
            // Remove the old dateTime stamps so the files get reloaded even if they have been seen before
            Ioc.Default.GetService<IPreferences>().Remove(Constants.SettingsGPKGFileLastDateTimeModified);
            Ioc.Default.GetService<IPreferences>().Remove(Constants.SettingsGPRAMPSFileLastDateTimeModified);
            Ioc.Default.GetService<IPreferences>().Remove(Constants.SettingsXMLFileLastDateTimeModified);

            DataStore.Instance.DS.IsDataLoaded = false;

            CommonLocalSettings.DataSerialised = false;
        }
    }
}