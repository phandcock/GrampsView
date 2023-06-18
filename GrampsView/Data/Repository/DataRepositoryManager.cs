// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Common.Interfaces;
using GrampsView.Data.StoreFile;
using GrampsView.Data.StorePostLoad;
using GrampsView.Data.StoreSerial;
using GrampsView.Data.StoreXML;
using GrampsView.Events;

using Microsoft.AppCenter.Analytics;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using System.Diagnostics.Contracts;

namespace GrampsView.Data.Repository
{
    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>

    public sealed class DataRepositoryManager : ObservableObject, IDataRepositoryManager
    {
        /// <summary>
        /// The local common logging.
        /// </summary>
        private readonly ILog _CL;

        private readonly IErrorNotifications _commonNotifications;

        /// <summary>
        /// Injected Event Aggregator.
        /// </summary>
        private readonly IMessenger _EventAggregator;

        /// <summary>
        /// Injected External Storage.
        /// </summary>
        private readonly IStoreXML _ExternalStorage;

        /// <summary>
        /// The local post load.
        /// </summary>
        private readonly IStorePostLoad _PostLoad;

        /// <summary>
        /// The local gramps store serial.
        /// </summary>
        private readonly IGrampsStoreSerial _StoreSerial;

        /// <summary>Initializes a new instance of the <see cref="DataRepositoryManager" /> class.</summary>
        /// <param name="iocCommonLogging">The ioc common logging.</param>
        /// <param name="iocCommonNotifications"></param>
        /// <param name="iocExternalStorage">The ioc external storage.</param>
        /// <param name="iocMessenger"></param>
        /// <param name="iocStoreFileTar"></param>
        /// <param name="iocStorePostLoad"></param>
        /// <param name="iocStoreSerial"></param>
        /// <param name="iocStoreFile">The ioc store file.</param>
        /// <param name="iocStoreZip"></param>
        public DataRepositoryManager(ILog iocCommonLogging,
                                     IErrorNotifications iocCommonNotifications,
                                     IStoreXML iocExternalStorage,
                                     IMessenger iocMessenger,
                                     IStoreFileTar iocStoreFileTar,
                                     IStorePostLoad iocStorePostLoad,
                                     IGrampsStoreSerial iocStoreSerial,
                                     IStoreFile iocStoreFile,
                                     IStoreFileZip iocStoreZip)
        {
            _CL = iocCommonLogging ?? throw new ArgumentNullException(nameof(iocCommonLogging));

            _ExternalStorage = iocExternalStorage ?? throw new ArgumentNullException(nameof(iocExternalStorage));

            _PostLoad = iocStorePostLoad;
            _StoreSerial = iocStoreSerial;
            LocalStoreZip = iocStoreZip;
            LocalStoreTar = iocStoreFileTar;
            LocalStoreFile = iocStoreFile;
            _EventAggregator = iocMessenger;
            _commonNotifications = iocCommonNotifications;

            // Event Handlers
            Contract.Assert(_EventAggregator != null);

            Ioc.Default.GetRequiredService<IMessenger>().Register<AppStartLoadDataEvent>(this, async (r, m) =>
            {
                if (!m.Value)
                {
                    return;
                }

                //     Ioc.Default.GetRequiredService<IMessenger>().Send(new NavigationPushEvent(new SharedSharp.Views.SharedSharpMessageLogPage()));

                await StartDataLoadAsync();

                Ioc.Default.GetRequiredService<IMessenger>().Send(new NavigationPopRootEvent(true));
            });

            Ioc.Default.GetRequiredService<IMessenger>().Register<DataSaveSerialEvent>(this, (r, m) =>
            {
                if (!m.Value)
                {
                    return;
                }

                SerializeRepositoriesAsync(true);
            });

            Ioc.Default.GetRequiredService<IMessenger>().Register<DataLoadCompleteEvent>(this, (r, m) =>
            {
                if (!m.Value)
                {
                    return;
                }

                DataLoadedSetTrue();
            });
        }

        public IStoreFile LocalStoreFile { get; }

        public IStoreFileTar LocalStoreTar { get; }

        /// <summary>
        /// Gets the storage.
        /// </summary>
        /// <value>
        /// The storage.
        /// </value>
        public IStoreFileZip LocalStoreZip { get; }

        /// <summary>
        /// Clears the repositories.
        /// </summary>
        public static void ClearRepositories()
        {
            // clear existing data TODO this.iocHeaderDataSource.DataClear();
            DataStore.Instance.DS.BookMarkCollection.Clear();

            DataStore.Instance.DS.AddressData.Clear();
            DataStore.Instance.DS.CitationData.Clear();
            DataStore.Instance.DS.EventData.Clear();
            DataStore.Instance.DS.FamilyData.Clear();
            DataStore.Instance.DS.HeaderData.Clear();
            DataStore.Instance.DS.MediaData.Clear();
            DataStore.Instance.DS.NameMapData.Clear();
            DataStore.Instance.DS.NoteData.Clear();
            DataStore.Instance.DS.PersonData.Clear();
            DataStore.Instance.DS.PersonNameData.Clear();
            DataStore.Instance.DS.PlaceData.Clear();
            DataStore.Instance.DS.RepositoryData.Clear();
            DataStore.Instance.DS.SourceData.Clear();
            DataStore.Instance.DS.TagData.Clear();
        }

        /// <summary>Sets the loaded.</summary>
        public static void DataLoadedSetTrue()
        {
            DataStore.Instance.DS.IsDataLoaded = true;
        }

        /// <summary>
        /// Serializes the repositories asynchronous.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public async void SerializeRepositoriesAsync(object value)
        {
            _CL.DataLogEntryAdd("Serialising Data");

            await _StoreSerial.SerializeRepository();

            SharedSharpSettings.DataSerialised = true;
        }

        /// <summary>
        /// Starts the data load asynchronous. Order is:
        /// 1) UnTar new *.GPKG file
        /// 2) UnZip new data.GRAMPS file
        /// 3) Load new data.XML file
        /// 4) ELSE load Serial file.
        /// </summary>
        /// <returns>
        /// Returns a empty task.
        /// </returns>
        public async Task StartDataLoadAsync()
        {
            IFileInfoEx GrampsFile = new FileInfoEx();

            _CL.DataLogEntryAdd("Loading Data...");

            if (DataStore.Instance.DS.IsDataLoaded)
            {
                return;
            }

            //await _commonNotifications.DataLogShow();

            // Clear the repositories in case we had to restart after being interupted.
            ClearRepositories();

            // Create the DataStorage Folder
            DataStore.Instance.AD.CurrentDataFolder = new CurrentDataFolder();

            if (DataStore.Instance.AD.CurrentDataFolder.Valid)
            {
                // 1) Init Data Storage
                if (DataStore.Instance.AD.CurrentInputStreamValid)
                {
                    // Clear the file system
                    _ = await LocalStoreFile.DataStorageInitialiseAsync().ConfigureAwait(false);
                }

                // 1a) UnTar *.GPKG
                if (DataStore.Instance.AD.CurrentInputStreamFileType == ".gpkg")
                {
                    GrampsFile = await TriggerLoadGPKGFileAsync().ConfigureAwait(false);
                }

                // 1b) UnTar *.GRAMPS
                if (DataStore.Instance.AD.CurrentInputStreamFileType == ".gramps")
                {
                    _CL.DataLogEntryAdd("Later version of Gramps XML data compressed file found. Loading it into the program");

                    File.Copy(DataStore.Instance.AD.CurrentInputStreamPath, Path.Combine(DataStore.Instance.AD.CurrentDataFolder.FolderAsString, Constants.StorageXMLFileName));

                    GrampsFile = new FileInfoEx();  // Mark as invalid as do not need to unzip
                }

                // 2) UnZip new data.GRAMPS file
                if (GrampsFile.Valid)
                {
                    if (CommonLocalSettings.ModifiedComparedToSettings(GrampsFile, Constants.SettingsGPRAMPSFileLastDateTimeModified))
                    {
                        _CL.DataLogEntryAdd("Later version of Gramps data file found. Loading it into the program");

                        _ = await TriggerLoadGRAMPSFileAsync(false).ConfigureAwait(false);
                    }
                }

                // 3) Load new data.XML file
                IFileInfoEx dataXML = new FileInfoEx(argFileName: Constants.StorageXMLFileName);

                if (dataXML.Valid)
                {
                    if (CommonLocalSettings.ModifiedComparedToSettings(dataXML, Constants.SettingsXMLFileLastDateTimeModified))
                    {
                        _CL.DataLogEntryAdd("Later version of Gramps XML data file found. Loading it into the program");

                        // Load the new data
                        _ = await TriggerLoadGrampsUnZippedFolderAsync().ConfigureAwait(false);

                        return;
                    }
                }

                if (SharedSharpSettings.DataSerialised)
                {
                    // 4) ELSE load Serial file
                    _ = await TriggerLoadSerialDataAsync().ConfigureAwait(false);

                    // await _commonNotifications.DataLogHide();
                }

                return;
            }
            else
            {
                _commonNotifications.NotifyError(new ErrorInfo("DataStorageFolder not valid.  It will need to be reloaded..."));

                CommonLocalSettings.SetReloadDatabase();
            }

            // TODO Handle special messages if there is a problem

            _CL.DataLogEntryAdd("Unable to load Datafolder");
            return;
        }

        /// <summary>Triggers the load GPKG file asynchronous.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<FileInfoEx> TriggerLoadGPKGFileAsync()
        {
            Analytics.TrackEvent("TriggerLoadGPKGFileAsync",
                new Dictionary<string, string> {
                { "FileName", DataStore.Instance.AD.CurrentInputStreamPath },
                });

            if (!DataStore.Instance.AD.CurrentInputStreamValid)
            {
                return new FileInfoEx();
            }

            _CL.DataLogEntryAdd("Later version of Gramps XML data plus Media compressed file found. Loading it into the program");

            // Only unZip gzip files

            _CL.DataLogEntryAdd("Loading GPKG data");

            if (DataStore.Instance.AD.CurrentInputStreamValid)
            {
                // TODO create data folder await localStoreFile.SetDataFolderLocalStorage();

                //await DataStore.Instance.FFIL.InvalidateCacheAsync(CacheType.All).ConfigureAwait(false);

                // TODO work out how to delete excess files based on keeping the ones in the GPKG file
                //// Delete directories of files. Assume files in root are ok
                // IReadOnlyList<StorageFolder> t = await DataStore.Instance.AD.CurrentDataFolder.GetFoldersAsync();

                // foreach (StorageFolder item in t) { await item.DeleteAsync(); }
                _ = await LocalStoreTar.DecompressTAR().ConfigureAwait(false);

                // Save the current Index File modified date for later checking TODO How doe sthis
                // work if only loading picked file?
                // StoreFileNames.SaveFileModifiedSinceLastSave(Constants.SettingsGPKGFileLastDateTimeModified, DataStore.Instance.AD.CurrentInputFile);
            }

            return new FileInfoEx(argFileName: Constants.StorageGRAMPSFileName);
        }

        /// <summary>
        /// Load the the new GRAMPS data.
        /// </summary>
        /// <returns>
        /// True if the load is successful or False if not.
        /// </returns>
        public Task<bool> TriggerLoadGRAMPSFileAsync(bool deleteOld)
        {
            IFileInfoEx fileGrampsDataInput = new FileInfoEx(argFileName: Constants.StorageGRAMPSFileName);

            if (fileGrampsDataInput != null)
            {
                if (deleteOld)
                {
                    // TODO fix this
                    //await localStoreFile.DataStorageInitialiseAsync(DataStore.Instance.AD.CurrentDataFolder).ConfigureAwait(false);
                }

                _ = LocalStoreZip.DecompressGZIP(fileGrampsDataInput);

                // Save the current Index File modified date for later checking

                CommonLocalSettings.SaveLastWriteToSettings(fileGrampsDataInput, Constants.SettingsGPRAMPSFileLastDateTimeModified);
            }

            return Task.FromResult(false);
        }

        /// <summary>
        /// Load the the new GRAMPS data.
        /// </summary>
        /// <returns>
        /// True if the load is successful or False if not.
        /// </returns>
        public async Task<bool> TriggerLoadGrampsUnZippedFolderAsync()
        {
            _CL.DataLogEntryAdd("Loading GRAMPS XML unzipped data");
            {
                ClearRepositories();

                bool tt = await _ExternalStorage.DataStorageLoadXML().ConfigureAwait(false);

                if (tt)
                {
                    _ = await _ExternalStorage.LoadXMLDataAsync().ConfigureAwait(false);

                    _CL.DataLogEntryAdd("Finished loading GRAMPS XML data");

                    IFileInfoEx t = new FileInfoEx(argFileName: Constants.StorageXMLFileName);

                    if (t.Valid)
                    {
                        // Save the current Index File modified date for later checking
                        CommonLocalSettings.SaveLastWriteToSettings(t, Constants.SettingsXMLFileLastDateTimeModified);
                    }

                    UpdateSavedLocalSettings();

                    // save the data in a serial format for next time localEventAggregator.GetEvent<DataSaveSerialEvent>().Publish(null);

                    // let everybody know we have finished loading data
                    _ = Ioc.Default.GetRequiredService<IMessenger>().Send(new DataLoadXMLEvent(true));

                    return true;
                }
            }

            return true;
        }

        /// <summary>
        /// Loads the data asynchronous.
        /// </summary>
        /// <returns>
        /// Task indicating if the data is loaded successfully.
        /// </returns>
        public async Task<bool> TriggerLoadSerialDataAsync()
        {
            try
            {
                _CL.RoutineEntry("TriggerLoadSerialDataAsync");

                _CL.DataLogEntryAdd("Checking for Serialised GRAMPS data");
                if (DataStore.Instance.DS.IsDataLoaded == false)
                {
                    if (SharedSharpSettings.DataSerialised)
                    {
                        _CL.DataLogEntryAdd("Loading GRAMPS Serial data");

                        await _StoreSerial.DeSerializeRepository();

                        UpdateSavedLocalSettings();

                        //await _PostLoad.LoadSerialUiItems().ConfigureAwait(false);

                        _CL.DataLogEntryReplace("GRAMPS Serial data load complete");

                        // let everybody know we have finished loading data
                        _ = Ioc.Default.GetRequiredService<IMessenger>().Send(new DataLoadCompleteEvent(true));

                        //Ioc.Default.GetRequiredService<IMessenger>().Send(new NavigationPopRootEvent(true));
                    }
                    else
                    {
                        _CL.DataLogEntryAdd("GRAMPS Serial data load failed.");

                        CommonLocalSettings.SetReloadDatabase();
                    }
                }
            }
            catch (Exception ex)
            {
                SharedSharpSettings.DataSerialised = false;

                _commonNotifications.NotifyException("Trying to load existing serialised data", ex);

                CommonLocalSettings.SetReloadDatabase();
            }

            _CL.RoutineExit("");

            return false;
        }

        private static void UpdateSavedLocalSettings()
        {
            // save the database version
            SharedSharpSettings.DatabaseVersion = Constants.GrampsViewDatabaseVersion;
        }
    }
}