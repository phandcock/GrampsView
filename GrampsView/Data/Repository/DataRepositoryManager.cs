namespace GrampsView.Data.Repository
{
    using FFImageLoading;
    using FFImageLoading.Cache;

    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.External.StoreSerial;
    using GrampsView.Data.ExternalStorage;
    using GrampsView.Events;

    using Microsoft.AppCenter.Analytics;

    using Prism.Events;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    [DataContract]
    public sealed class DataRepositoryManager : CommonBindableBase, IDataRepositoryManager
    {
        /// <summary>
        /// The local common logging.
        /// </summary>
        private readonly ICommonLogging _CL;

        /// <summary>
        /// Injected Event Aggregator.
        /// </summary>
        private readonly IEventAggregator _EventAggregator;

        /// <summary>
        /// The local gramps store serial.
        /// </summary>
        private readonly IGrampsStoreSerial _StoreSerial;

        /// <summary>
        /// Injected External Storage.
        /// </summary>
        private readonly IStoreXML localExternalStorage;

        /// <summary>
        /// The local post load.
        /// </summary>
        private readonly IStorePostLoad localPostLoad;

        /// <summary>
        /// The local store file.
        /// </summary>
        private readonly IStoreFile localStoreFile;

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
        public DataRepositoryManager(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, IStoreXML iocExternalStorage, IStorePostLoad iocGrampsStorePostLoad, IGrampsStoreSerial iocGrampsStoreSerial, IStoreFile iocStoreFile)
        {
            _CL = iocCommonLogging ?? throw new ArgumentNullException(nameof(iocCommonLogging));

            localExternalStorage = iocExternalStorage ?? throw new ArgumentNullException(nameof(iocExternalStorage));

            localPostLoad = iocGrampsStorePostLoad;
            _StoreSerial = iocGrampsStoreSerial;
            localStoreFile = iocStoreFile;
            _EventAggregator = iocEventAggregator;

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
                return localStoreFile;
            }
        }

        /// <summary>
        /// Clears the repositories.
        /// </summary>
        public static void ClearRepositories()
        {
            // clear existing data TODO this.iocHeaderDataSource.DataClear();
            DataStore.Instance.DS.BookMarkCollection.Clear();

            DataStore.Instance.DS.CitationData.Clear();
            DataStore.Instance.DS.EventData.Clear();
            DataStore.Instance.DS.FamilyData.Clear();
            DataStore.Instance.DS.HeaderData.Clear();
            DataStore.Instance.DS.MediaData.Clear();
            DataStore.Instance.DS.NameMapData.Clear();
            DataStore.Instance.DS.NoteData.Clear();
            DataStore.Instance.DS.PersonData.Clear();
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
            await DataStore.Instance.CN.DataLogEntryAdd("Serialising Data").ConfigureAwait(false);

            _StoreSerial.SerializeObject(DataStore.Instance.DS);

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
                DataStore.Instance.CN.NotifyError(new ErrorInfo("Failed to load existing data..."));
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
            await DataStore.Instance.CN.DataLogEntryAdd("Loading Data...").ConfigureAwait(false);

            if (DataStore.Instance.DS.IsDataLoaded)
            {
                return true;
            }

            DataStore.Instance.CN.DataLogShow();

            // Clear the repositories in case we had to restart after being interupted.
            ClearRepositories();

            if (DataStore.Instance.AD.CurrentDataFolder.Valid)
            {
                // 1) UnTar *.GPKG
                if (DataStore.Instance.AD.CurrentInputStreamValid)
                {
                    // Clear the file system
                    await localStoreFile.DataStorageInitialiseAsync().ConfigureAwait(false);

                    await DataStore.Instance.CN.DataLogEntryAdd("Later version of Gramps XML data plus Media  compressed file found. Loading it into the program").ConfigureAwait(false);

                    await TriggerLoadGPKGFileAsync().ConfigureAwait(false);
                }

                // 2) UnZip new data.GRAMPS file
                FileInfoEx GrampsFile = StoreFolder.FolderGetFile(CommonConstants.StorageGRAMPSFileName);

                if (GrampsFile.Valid)
                {
                    if (StoreFileNames.FileModifiedSinceLastSaveAsync(CommonConstants.SettingsGPRAMPSFileLastDateTimeModified, GrampsFile))
                    {
                        await DataStore.Instance.CN.DataLogEntryAdd("Later version of Gramps data file found. Loading it into the program").ConfigureAwait(false);

                        await TriggerLoadGRAMPSFileAsync(false).ConfigureAwait(false);
                    }
                }

                // 3) Load new data.XML file
                FileInfoEx dataXML = StoreFolder.FolderGetFile(CommonConstants.StorageXMLFileName);

                if (dataXML.Valid)
                {
                    if (StoreFileNames.FileModifiedSinceLastSaveAsync(CommonConstants.SettingsXMLFileLastDateTimeModified, dataXML))
                    {
                        await DataStore.Instance.CN.DataLogEntryAdd("Later version of Gramps XML data file found. Loading it into the program").ConfigureAwait(false);

                        // Load the new data
                        await TriggerLoadGrampsUnZippedFolderAsync().ConfigureAwait(false);

                        DataStore.Instance.CN.DataLogHide();

                        return true;
                    }
                }

                if (CommonLocalSettings.DataSerialised)
                {
                    // 4) ELSE load Serial file
                    await TriggerLoadSerialDataAsync().ConfigureAwait(false);

                    DataStore.Instance.CN.DataLogHide();
                }

                return true;
            }
            else
            {
                DataStore.Instance.CN.NotifyError(new ErrorInfo("DataStorageFolder not valid.  It will need to be reloaded..."));

                CommonLocalSettings.SetReloadDatabase();
            }

            // TODO Handle special messages if there is a problem

            await DataStore.Instance.CN.DataLogEntryAdd("Unable to load Datafolder").ConfigureAwait(false);
            return false;
        }

        /// <summary>
        /// Triggers the load GPKG file asynchronous.
        /// </summary>
        /// <param name="deleteOld">
        /// if set to <c>true</c> [delete old].
        /// </param>
        /// <param name="gpkgFileName">
        /// Name of the GPKG file.
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<bool> TriggerLoadGPKGFileAsync()
        {
            Analytics.TrackEvent("TriggerLoadGPKGFileAsync",
                new Dictionary<string, string> {
                { "FileName", DataStore.Instance.AD.CurrentInputStreamPath },
                });

            if (!DataStore.Instance.AD.CurrentInputStreamValid)
            {
                return false;
            }

            await DataStore.Instance.CN.DataLogEntryAdd("Loading GPKG data").ConfigureAwait(false);

            if (DataStore.Instance.AD.CurrentInputStreamValid)
            {
                // TODO create data folder await localStoreFile.SetDataFolderLocalStorage();

                // Clear image cache
                try
                {
                    await ImageService.Instance.InvalidateCacheAsync(CacheType.All).ConfigureAwait(false);
                }
                catch (NotImplementedException ex)
                {
                    // TODO Ignore as part of Unit Tests
                }

                // TODO work out how to delete excess files based on keeping the ones in the GPKG file
                //// Delete directories of files. Assume files in root are ok
                // IReadOnlyList<StorageFolder> t = await DataStore.Instance.AD.CurrentDataFolder.GetFoldersAsync();

                // foreach (StorageFolder item in t) { await item.DeleteAsync(); }
                await localStoreFile.DecompressTAR().ConfigureAwait(false);

                // Save the current Index File modified date for later checking TODO How doe sthis
                // work if only loading picked file?
                // StoreFileNames.SaveFileModifiedSinceLastSave(CommonConstants.SettingsGPKGFileLastDateTimeModified, DataStore.Instance.AD.CurrentInputFile);
            }

            return false;
        }

        /// <summary>
        /// Load the the new GRAMPS data.
        /// </summary>
        /// <returns>
        /// True if the load is successful or False if not.
        /// </returns>
        public async Task<bool> TriggerLoadGRAMPSFileAsync(bool deleteOld)
        {
            FileInfoEx fileGrampsDataInput = StoreFolder.FolderGetFile(CommonConstants.StorageGRAMPSFileName);

            if (fileGrampsDataInput != null)
            {
                if (deleteOld)
                {
                    // TODO fix this
                    //await localStoreFile.DataStorageInitialiseAsync(DataStore.Instance.AD.CurrentDataFolder).ConfigureAwait(false);
                }

                await localStoreFile.DecompressGZIP(fileGrampsDataInput).ConfigureAwait(false);

                // Save the current Index File modified date for later checking
                StoreFileNames.SaveFileModifiedSinceLastSave(CommonConstants.SettingsGPRAMPSFileLastDateTimeModified, fileGrampsDataInput);
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
            await DataStore.Instance.CN.DataLogEntryAdd("Loading GRAMPS XML unzipped data").ConfigureAwait(false);
            {
                ClearRepositories();

                bool tt = await localExternalStorage.DataStorageLoadXML().ConfigureAwait(false);

                if (tt)
                {
                    await localExternalStorage.LoadXMLDataAsync().ConfigureAwait(false);

                    await DataStore.Instance.CN.DataLogEntryAdd("Finished loading GRAMPS XML data").ConfigureAwait(false);

                    FileInfoEx t = StoreFolder.FolderGetFile(CommonConstants.StorageXMLFileName);

                    if (t.Valid)
                    {
                        // Save the current Index File modified date for later checking
                        StoreFileNames.SaveFileModifiedSinceLastSave(CommonConstants.SettingsXMLFileLastDateTimeModified, t);
                    }

                    UpdateSettings();

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

                await DataStore.Instance.CN.DataLogEntryAdd("Checking for Serialised GRAMPS data").ConfigureAwait(false);
                if (DataStore.Instance.DS.IsDataLoaded == false)
                {
                    if (CommonLocalSettings.DataSerialised)
                    {
                        await DataStore.Instance.CN.DataLogEntryAdd("Loading GRAMPS Serial data").ConfigureAwait(false);

                        _StoreSerial.DeSerializeRepository();

                        UpdateSettings();

                        await localPostLoad.LoadSerialUiItems().ConfigureAwait(false);

                        await DataStore.Instance.CN.DataLogEntryReplace("GRAMPS Serial data load complete").ConfigureAwait(false);

                        // let everybody know we have finished loading data
                        _EventAggregator.GetEvent<DataLoadCompleteEvent>().Publish();
                    }
                    else
                    {
                        await DataStore.Instance.CN.DataLogEntryAdd("GRAMPS Serial data load failed.").ConfigureAwait(false);

                        CommonLocalSettings.SetReloadDatabase();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonLocalSettings.DataSerialised = false;

                DataStore.Instance.CN.NotifyException("Trying to load existing serialised data", ex);

                CommonLocalSettings.SetReloadDatabase();

                throw;
            }

            _CL.RoutineExit("");

            return false;
        }

        private void UpdateSettings()
        {
            // save the database version
            CommonLocalSettings.DatabaseVersion = CommonConstants.GrampsViewDatabaseVersion;
        }
    }
}