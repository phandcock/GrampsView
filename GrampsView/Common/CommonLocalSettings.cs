namespace GrampsView.Common
{
    using GrampsView.Data.Repository;

    using Xamarin.Forms;

    public static class CommonLocalSettings
    {
        private const string SettingsAppTheme = "ApplicationTheme";

        public static OSAppTheme ApplicationTheme
        {
            get
            {
                int _settingsAppTheme = DataStore.Instance.ES.PreferencesGet(SettingsAppTheme, 0);

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
                            DataStore.Instance.ES.PreferencesSet(SettingsAppTheme, 1);
                            break;
                        }

                    case OSAppTheme.Light:
                        {
                            DataStore.Instance.ES.PreferencesSet(SettingsAppTheme, 2);
                            break;
                        }

                    default:
                        {
                            DataStore.Instance.ES.PreferencesSet(SettingsAppTheme, 3);
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
                    // ClearPreferences();
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
                int localGrampsViewDatabaseVersion = DataStore.Instance.ES.PreferencesGet(CommonConstants.SettingsGrampsViewDatabaseVersion, int.MinValue);

                if (localGrampsViewDatabaseVersion == int.MinValue)
                {
                    // If the Setting is not defined then assume the database has not been loaded so
                    // the version number is set to MinValue to force load
                    return int.MinValue;
                }

                return localGrampsViewDatabaseVersion;
            }

            set
            {
                DataStore.Instance.ES.PreferencesSet("GrampsViewDatabaseVersion", value);
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
                return DataStore.Instance.ES.PreferencesGet("SerialisedData", false);
            }

            set
            {
                DataStore.Instance.ES.PreferencesSet("SerialisedData", value);
            }
        }

        public static bool FirstRunDisplay
        {
            get
            {
                return DataStore.Instance.ES.PreferencesGet("FirstRunDisplay", false);
            }

            set
            {
                DataStore.Instance.ES.PreferencesSet("FirstRunDisplay", value);
            }
        }

        public static string NoteEmailAddress
        {
            get
            {
                return DataStore.Instance.ES.PreferencesGet("NoteEmailAddress", string.Empty);
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

        public static void SetReloadDatabase()
        {
            // Remove the old dateTime stamps so the files get reloaded even if they have been seen before
            DataStore.Instance.ES.PreferencesRemove(CommonConstants.SettingsGPKGFileLastDateTimeModified);
            DataStore.Instance.ES.PreferencesRemove(CommonConstants.SettingsGPRAMPSFileLastDateTimeModified);
            DataStore.Instance.ES.PreferencesRemove(CommonConstants.SettingsXMLFileLastDateTimeModified);

            DataStore.Instance.DS.IsDataLoaded = false;

            DataSerialised = false;
        }
    }
}