namespace GrampsView.Data.Repository
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.External.StoreSerial;
    using GrampsView.Data.ExternalStorage;
    using GrampsView.Events;

    using Microsoft.AppCenter.Analytics;
    using Microsoft.Toolkit.Mvvm.Messaging;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>

    public sealed class DataRepositoryManager : ObservableObject, IDataRepositoryManager
    {
        /// <summary>
        /// The local common logging.
        /// </summary>
        private readonly ICommonLogging _CL;

        private readonly ICommonNotifications _commonNotifications;

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
        /// The local store file.
        /// </summary>
        private readonly IStoreFile _StoreFile;

        /// <summary>
        /// The local gramps store serial.
        /// </summary>
        private readonly IGrampsStoreSerial _StoreSerial;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRepositoryManager"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        /// <param name="iocExternalStorage">
        /// The ioc external storage.
        /// </param>
        /// <param name="iocGrampsStorePostLoad">
        /// The ioc gramps store post load.
        /// </param>
        /// <param name="iocGrampsStoreSerial">
        /// The ioc gramps store serial.
        /// </param>
        /// <param name="iocStoreFile">
        /// The ioc store file.
        /// </param>
        public DataRepositoryManager(ICommonLogging iocCommonLogging, ICommonNotifications iocCommonNotifications, IMessenger iocEventAggregator, IStoreXML iocExternalStorage, IStorePostLoad iocGrampsStorePostLoad, IGrampsStoreSerial iocGrampsStoreSerial, IStoreFile iocStoreFile)
        {
            _CL = iocCommonLogging ?? throw new ArgumentNullException(nameof(iocCommonLogging));

            _ExternalStorage = iocExternalStorage ?? throw new ArgumentNullException(nameof(iocExternalStorage));

            _PostLoad = iocGrampsStorePostLoad;
            _StoreSerial = iocGrampsStoreSerial;
            _StoreFile = iocStoreFile;
            _EventAggregator = iocEventAggregator;
            _commonNotifications = iocCommonNotifications;

            // Event Handlers
            Contract.Assert(_EventAggregator != null);
            _EventAggregator.GetEvent<DataLoadStartEvent>().Subscribe(StartDataLoad, ThreadOption.BackgroundThread);
            _EventAggregator.GetEvent<DataSaveSerialEvent>().Subscribe(SerializeRepositoriesAsync, ThreadOption.BackgroundThread);
            _EventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(DataLoadedSetTrue, ThreadOption.BackgroundThread);
        }

        /// <summary>
        /// Gets the storage.
        /// </summary>
        /// <value>
        /// The storage.
        /// </value>
        public IStoreFile Storage
        {
            get
            {
                return _StoreFile;
            }
        }

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

        /// <summary>
        /// Sets the loaded.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
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
            await _commonNotifications.DataLogEntryAdd("Serialising Data").ConfigureAwait(false);

            await _StoreSerial.SerializeObject(DataStore.Instance.DS);

            CommonLocalSettings.DataSerialised = true;
        }

        /// <summary>
        /// Starts the data load.
        /// </summary>
        public void StartDataLoad()
        {
            Task<bool> t = Task.Run(() => StartDataLoadAsync());

            if (!t.Result)
            {
                _commonNotifications.NotifyError(new ErrorInfo("Failed to load existing data..."));
            }
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
        public async Task<bool> StartDataLoadAsync()
        {
            IFileInfoEx GrampsFile = new FileInfoEx();

            await _commonNotifications.DataLogEntryAdd("Loading Data...").ConfigureAwait(false);

            if (DataStore.Instance.DS.IsDataLoaded)
            {
                return true;
            }

            await _commonNotifications.DataLogShow();

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
                    await _StoreFile.DataStorageInitialiseAsync().ConfigureAwait(false);
                }

                // 1a) UnTar *.GPKG
                if (DataStore.Instance.AD.CurrentInputStreamFileType == ".gpkg")
                {
                    GrampsFile = await TriggerLoadGPKGFileAsync().ConfigureAwait(false);
                }

                // 1b) UnTar *.GRAMPS
                if (DataStore.Instance.AD.CurrentInputStreamFileType == ".gramps")
                {
                    await _commonNotifications.DataLogEntryAdd("Later version of Gramps XML data compressed file found. Loading it into the program").ConfigureAwait(false);

                    File.Copy(DataStore.Instance.AD.CurrentInputStreamPath, Path.Combine(DataStore.Instance.AD.CurrentDataFolder.Path, CommonConstants.StorageXMLFileName));

                    GrampsFile = new FileInfoEx();  // Mark as invalid as do not need to unzip
                }

                // 2) UnZip new data.GRAMPS file
                if (GrampsFile.Valid)
                {
                    if (CommonLocalSettings.ModifiedComparedToSettings(GrampsFile, CommonConstants.SettingsGPRAMPSFileLastDateTimeModified))
                    {
                        await _commonNotifications.DataLogEntryAdd("Later version of Gramps data file found. Loading it into the program").ConfigureAwait(false);

                        await TriggerLoadGRAMPSFileAsync(false).ConfigureAwait(false);
                    }
                }

                // 3) Load new data.XML file
                IFileInfoEx dataXML = new FileInfoEx(argFileName: CommonConstants.StorageXMLFileName);

                if (dataXML.Valid)
                {
                    if (CommonLocalSettings.ModifiedComparedToSettings(dataXML, CommonConstants.SettingsXMLFileLastDateTimeModified))
                    {
                        await _commonNotifications.DataLogEntryAdd("Later version of Gramps XML data file found. Loading it into the program").ConfigureAwait(false);

                        // Load the new data
                        await TriggerLoadGrampsUnZippedFolderAsync().ConfigureAwait(false);

                        //_commonNotifications.DataLogHide();

                        return true;
                    }
                }

                if (CommonLocalSettings.DataSerialised)
                {
                    // 4) ELSE load Serial file
                    await TriggerLoadSerialDataAsync().ConfigureAwait(false);

                    await _commonNotifications.DataLogHide();
                }

                return true;
            }
            else
            {
                _commonNotifications.NotifyError(new ErrorInfo("DataStorageFolder not valid.  It will need to be reloaded..."));

                CommonLocalSettings.SetReloadDatabase();
            }

            // TODO Handle special messages if there is a problem

            await _commonNotifications.DataLogEntryAdd("Unable to load Datafolder").ConfigureAwait(false);
            return false;
        }

        /// <summary>
        /// Triggers the load GPKG file asynchronous.
        /// </summary>
        /// <param name="deleteOld">
        /// if set to <c> true </c> [delete old].
        /// </param>
        /// <param name="gpkgFileName">
        /// Name of the GPKG file.
        /// </param>
        /// <returns>
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

            await _commonNotifications.DataLogEntryAdd("Later version of Gramps XML data plus Media compressed file found. Loading it into the program").ConfigureAwait(false);

            // Only unZip gzip files

            await _commonNotifications.DataLogEntryAdd("Loading GPKG data").ConfigureAwait(false);

            if (DataStore.Instance.AD.CurrentInputStreamValid)
            {
                // TODO create data folder await localStoreFile.SetDataFolderLocalStorage();

                //await DataStore.Instance.FFIL.InvalidateCacheAsync(CacheType.All).ConfigureAwait(false);

                // TODO work out how to delete excess files based on keeping the ones in the GPKG file
                //// Delete directories of files. Assume files in root are ok
                // IReadOnlyList<StorageFolder> t = await DataStore.Instance.AD.CurrentDataFolder.GetFoldersAsync();

                // foreach (StorageFolder item in t) { await item.DeleteAsync(); }
                await _StoreFile.DecompressTAR().ConfigureAwait(false);

                // Save the current Index File modified date for later checking TODO How doe sthis
                // work if only loading picked file?
                // StoreFileNames.SaveFileModifiedSinceLastSave(CommonConstants.SettingsGPKGFileLastDateTimeModified, DataStore.Instance.AD.CurrentInputFile);
            }

            return new FileInfoEx(argFileName: CommonConstants.StorageGRAMPSFileName);
        }

        /// <summary>
        /// Load the the new GRAMPS data.
        /// </summary>
        /// <returns>
        /// True if the load is successful or False if not.
        /// </returns>
        public async Task<bool> TriggerLoadGRAMPSFileAsync(bool deleteOld)
        {
            IFileInfoEx fileGrampsDataInput = new FileInfoEx(argFileName: CommonConstants.StorageGRAMPSFileName);

            if (fileGrampsDataInput != null)
            {
                if (deleteOld)
                {
                    // TODO fix this
                    //await localStoreFile.DataStorageInitialiseAsync(DataStore.Instance.AD.CurrentDataFolder).ConfigureAwait(false);
                }

                await _StoreFile.DecompressGZIP(fileGrampsDataInput).ConfigureAwait(false);

                // Save the current Index File modified date for later checking

                CommonLocalSettings.SaveLastWriteToSettings(fileGrampsDataInput, CommonConstants.SettingsGPRAMPSFileLastDateTimeModified);
            }

            return false;
        }

        /// <summary>
        /// Load the the new GRAMPS data.
        /// </summary>
        /// <returns>
        /// True if the load is successful or False if not.
        /// </returns>
        public async Task<bool> TriggerLoadGrampsUnZippedFolderAsync()
        {
            await _commonNotifications.DataLogEntryAdd("Loading GRAMPS XML unzipped data").ConfigureAwait(false);
            {
                ClearRepositories();

                bool tt = await _ExternalStorage.DataStorageLoadXML().ConfigureAwait(false);

                if (tt)
                {
                    await _ExternalStorage.LoadXMLDataAsync().ConfigureAwait(false);

                    await _commonNotifications.DataLogEntryAdd("Finished loading GRAMPS XML data").ConfigureAwait(false);

                    IFileInfoEx t = new FileInfoEx(argFileName: CommonConstants.StorageXMLFileName);

                    if (t.Valid)
                    {
                        // Save the current Index File modified date for later checking
                        CommonLocalSettings.SaveLastWriteToSettings(t, CommonConstants.SettingsXMLFileLastDateTimeModified);
                    }

                    UpdateSavedLocalSettings();

                    // save the data in a serial format for next time localEventAggregator.GetEvent<DataSaveSerialEvent>().Publish(null);

                    // let everybody know we have finished loading data
                    _EventAggregator.GetEvent<DataLoadXMLEvent>().Publish(null);

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

                await _commonNotifications.DataLogEntryAdd("Checking for Serialised GRAMPS data").ConfigureAwait(false);
                if (DataStore.Instance.DS.IsDataLoaded == false)
                {
                    if (CommonLocalSettings.DataSerialised)
                    {
                        await _commonNotifications.DataLogEntryAdd("Loading GRAMPS Serial data").ConfigureAwait(false);

                        await _StoreSerial.DeSerializeRepository();

                        UpdateSavedLocalSettings();

                        await _PostLoad.LoadSerialUiItems().ConfigureAwait(false);

                        _commonNotifications.DataLogEntryReplace("GRAMPS Serial data load complete");

                        // let everybody know we have finished loading data
                        _EventAggregator.GetEvent<DataLoadCompleteEvent>().Publish();
                    }
                    else
                    {
                        await _commonNotifications.DataLogEntryAdd("GRAMPS Serial data load failed.").ConfigureAwait(false);

                        CommonLocalSettings.SetReloadDatabase();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonLocalSettings.DataSerialised = false;

                _commonNotifications.NotifyException("Trying to load existing serialised data", ex);

                CommonLocalSettings.SetReloadDatabase();

                throw;
            }

            _CL.RoutineExit("");

            return false;
        }

        private static void UpdateSavedLocalSettings()
        {
            // save the database version
            CommonLocalSettings.DatabaseVersion = CommonConstants.GrampsViewDatabaseVersion;
        }
    }
}