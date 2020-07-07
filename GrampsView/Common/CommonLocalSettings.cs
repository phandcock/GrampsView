//-----------------------------------------------------------------------
//
// Common routines for the CommonRoutines
//
// <copyright file="CommonLocalSettings.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using GrampsView.Data.Repository;

    using System;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// Various common routines.
    /// </summary>
    public static class CommonLocalSettings
    {
        private const string SettingsAppTheme = "ApplicationTheme";

        public static OSAppTheme ApplicationTheme
        {
            get
            {
                int _settingsAppTheme = Preferences.Get(SettingsAppTheme, 0);

                switch (_settingsAppTheme)
                {
                    case 1:
                        {
                            return OSAppTheme.Dark;
                        }

                    case 2:
                        {
                            return OSAppTheme.Light;
                        }

                    default:
                        {
                            return OSAppTheme.Unspecified;
                        }
                }
            }

            set
            {
                switch (value)
                {
                    case OSAppTheme.Dark:
                        {
                            SetInt(SettingsAppTheme, 1);
                            break;
                        }

                    case OSAppTheme.Light:
                        {
                            SetInt(SettingsAppTheme, 2);
                            break;
                        }

                    default:
                        {
                            SetInt(SettingsAppTheme, 3);
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether needs the database reload.
        /// </summary>
        /// <returns>
        /// </returns>
        public static bool DatabaseReloadNeeded
        {
            get
            {
                if (DatabaseVersion < CommonConstants.GrampsViewDatabaseVersion)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the database version.
        /// </summary>
        /// <value>
        /// The database version.
        /// </value>
        public static int DatabaseVersion
        {
            get
            {
                int localGrampsViewDatabaseVersion = Preferences.Get(CommonConstants.SettingsGrampsViewDatabaseVersion, int.MinValue);

                if (localGrampsViewDatabaseVersion == int.MinValue)
                {
                    // If the Setting is not defined then assume the database has not been loaded so
                    // the version number is set to MinValue to force load
                    return int.MinValue;
                }

                // bool validDatabaseReloadFlag = int.TryParse(localGrampsViewDatabaseVersion, out
                // int grampsViewDatabaseVersion);

                return localGrampsViewDatabaseVersion;
            }

            set
            {
                SetInt("GrampsViewDatabaseVersion", value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [data serialised].
        /// </summary>
        /// <value>
        /// <c>true</c> if [data serialised]; otherwise, <c>false</c>.
        /// </value>
        public static bool DataSerialised
        {
            get
            {
                return GetBool("SerialisedData");
            }

            set
            {
                SetBool("SerialisedData", value);
            }
        }

        public static bool FirstRunDisplay
        {
            get
            {
                return GetBool("FirstRunDisplay");
            }

            set
            {
                SetBool("FirstRunDisplay", value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [logging enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [logging enabled]; otherwise, <c>false</c>.
        /// </value>
        public static bool LoggingEnabled
        {
            get
            {
                return GetBool("LoggingEnabled");
            }

            set
            {
                SetBool("LoggingEnabled", value);
            }
        }

        public static bool WhatsNewDisplayed
        {
            get
            {
                return GetBool("WhatsNewDisplayed");
            }

            set
            {
                SetBool("WhatsNewDisplayed", value);
            }
        }

        public static void SetReloadDatabase()
        {
            // Remove the old dateTime stamps so the files get reloaded even if they have been seen before
            Preferences.Remove(CommonConstants.SettingsGPKGFileLastDateTimeModified);
            Preferences.Remove(CommonConstants.SettingsGPRAMPSFileLastDateTimeModified);
            Preferences.Remove(CommonConstants.SettingsXMLFileLastDateTimeModified);

            DataStore.DS.IsDataLoaded = false;

            DataSerialised = false;
        }

        /// <summary>
        /// Gets the bool local setting data.
        /// </summary>
        /// <param name="setting">
        /// The setting.
        /// </param>
        /// <returns>
        /// true if the value is Y.
        /// </returns>
        private static bool GetBool(string setting)
        {
            string settingFlag = Preferences.Get(setting, string.Empty);

            if (string.IsNullOrEmpty(settingFlag))
            {
                SetBool(setting, false);
                return false;
            }

            if (settingFlag == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static Int32 GetInt(string setting, Int32 argDefault)
        {
            Int32 settingFlag = Preferences.Get(setting, argDefault);

            return settingFlag;
        }

        /// <summary>
        /// Sets the bool.
        /// </summary>
        /// <param name="setting">
        /// The setting.
        /// </param>
        /// <param name="value">
        /// if set to <c>true</c> [value].
        /// </param>
        private static void SetBool(string setting, bool value)
        {
            if (value)
            {
                Preferences.Set(setting, "Y");
            }
            else
            {
                Preferences.Set(setting, "N");
            }
        }

        /// <summary>
        /// Sets the int.
        /// </summary>
        /// <param name="setting">
        /// The setting.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        private static void SetInt(string setting, Int32 value)
        {
            Preferences.Set(setting, value);
        }
    }
}