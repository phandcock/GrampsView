//-----------------------------------------------------------------------
//
// Handles the loading of GrampsData
//
// <copyright file="DataRepositoryManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Repository
{
    using FFImageLoading;
    using FFImageLoading.Cache;

    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.External.StoreSerial;
    using GrampsView.Data.ExternalStorageNS;
    using GrampsView.Events;

    using Microsoft.AppCenter.Analytics;

    using Prism.Events;

    using System;
    using System.Collections.Generic;
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
        /// Injected External Storage.
        /// </summary>
        private readonly IGrampsStoreXML localExternalStorage;

        /// <summary>
        /// The local post load.
        /// </summary>
        private readonly IStorePostLoad localPostLoad;

        /// <summary>
        /// The local store file.
        /// </summary>
        private IStoreFile localStoreFile;

        /// <summary>
        /// The local gramps store serial.
        /// </summary>
        private IGrampsStoreSerial localStoreSerial;

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
        public DataRepositoryManager(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, IGrampsStoreXML iocExternalStorage, IStorePostLoad iocGrampsStorePostLoad, IGrampsStoreSerial iocGrampsStoreSerial, IStoreFile iocStoreFile)
        {
            _CL = iocCommonLogging ?? throw new ArgumentNullException(nameof(iocCommonLogging));
            localExternalStorage = iocExternalStorage ?? throw new ArgumentNullException(nameof(iocExternalStorage));
            localPostLoad = iocGrampsStorePostLoad;
            localStoreSerial = iocGrampsStoreSerial;
            localStoreFile = iocStoreFile;
            _EventAggregator = iocEventAggregator;

            // Event Handlers
            //_EventAggregator.GetEvent<AppStartLoadDataEvent>().Subscribe(StartDataLoad, ThreadOption.BackgroundThread);
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
            DataStore.DS.BookMarkCollection.Clear(); ;
            DataStore.DS.CitationData.Clear();
            DataStore.DS.EventData.Clear();
            DataStore.DS.FamilyData.Clear();
            DataStore.DS.HeaderData.Clear();
            DataStore.DS.MediaData.Clear();
            DataStore.DS.NameMapData.Clear();
            DataStore.DS.NoteData.Clear();
            DataStore.DS.PersonData.Clear();
            DataStore.DS.PlaceData.Clear();
            DataStore.DS.RepositoryData.Clear();
            DataStore.DS.SourceData.Clear();
            DataStore.DS.TagData.Clear();
        }

        /// <summary>
        /// Sets the loaded.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void DataLoadedSetTrue(object value)
        {
            DataStore.DS.IsDataLoaded = true;
        }

        /// <summary>
        /// Serializes the repositories asynchronous.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public async void SerializeRepositoriesAsync(object value)
        {
            await DataStore.CN.MajorStatusAdd("Serialising Data").ConfigureAwait(false);

            localStoreSerial.SerializeObject(DataStore.DS);

            CommonLocalSettings.DataSerialised = true;
        }

        /// <summary>
        /// Starts the data load.
        /// </summary>
        /// <param name="unUsed">
        /// if set to <c>true</c> [un used].
        /// </param>
        public void StartDataLoad(bool unUsed)
        {
            Task<bool> t = Task.Run(() => StartDataLoadAsync());

            if (!t.Result)
            {
                DataStore.CN.NotifyError("Failed to load existing data...");
            }
        }

        public void StartDataLoad()
        {
            Task<bool> t = Task.Run(() => StartDataLoadAsync());

            if (!t.Result)
            {
                DataStore.CN.NotifyError("Failed to load existing data...");
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
            await DataStore.CN.ChangeLoadingMessage("Loading Data...").ConfigureAwait(false);

            if (DataStore.DS.IsDataLoaded)
            {
                return true;
            }

            // Clear the repositories in case we had to restart after being interupted.
            ClearRepositories();

            DataStore.AD.LoadDataStore();

            if (DataStore.AD.CurrentDataFolderValid)
            {
                // 1) UnTar *.GPKG
                if (DataStore.AD.CurrentInputStreamValid)
                {
                    await DataStore.CN.ChangeLoadingMessage("Later version of Gramps XML data plus Media  compressed file found. Loading it into the program").ConfigureAwait(false);

                    // Clear the file system
                    await localStoreFile.DataStorageInitialiseAsync().ConfigureAwait(false);

                    await TriggerLoadGPKGFileAsync().ConfigureAwait(false);
                }

                // 2) UnZip new data.GRAMPS file
                FileInfoEx GrampsFile = StoreFolder.FolderGetFile(DataStore.AD.CurrentDataFolder, CommonConstants.StorageGRAMPSFileName);

                if (GrampsFile.Valid)
                {
                    if (StoreFileNames.FileModifiedSinceLastSaveAsync(CommonConstants.SettingsGPRAMPSFileLastDateTimeModified, GrampsFile))
                    {
                        await DataStore.CN.ChangeLoadingMessage("Later version of Gramps data file found. Loading it into the program").ConfigureAwait(false);

                        await TriggerLoadGRAMPSFileAsync(false).ConfigureAwait(false);
                    }
                }

                // 3) Load new data.XML file
                FileInfoEx dataXML = StoreFolder.FolderGetFile(DataStore.AD.CurrentDataFolder, CommonConstants.StorageXMLFileName);

                if (dataXML.Valid)
                {
                    if (StoreFileNames.FileModifiedSinceLastSaveAsync(CommonConstants.SettingsXMLFileLastDateTimeModified, dataXML))
                    {
                        await DataStore.CN.ChangeLoadingMessage("Later version of Gramps XML data file found. Loading it into the program").ConfigureAwait(false);

                        // Load the new data
                        await TriggerLoadGrampsUnZippedFolderAsync().ConfigureAwait(false);

                        return true;
                    }
                }

                if (CommonLocalSettings.DataSerialised)
                {
                    // 4) ELSE load Serial file
                    await TriggerLoadSerialDataAsync().ConfigureAwait(false);
                }

                await DataStore.CN.ChangeLoadingMessage(null).ConfigureAwait(false);

                return true;
            }
            else
            {
                //if (!(DataStore.AD.CurrentDataFolder.Status.LatestException is null))
                //{
                //    DataStore.CN.NotifyException("StartDataLoadAsync", DataStore.AD.CurrentDataFolder.Status.LatestException);
                //}

                DataStore.CN.NotifyError("DataStorageFolder not valid.  It will need to be reloaded...");

                CommonLocalSettings.SetReloadDatabase();
            }

            // TODO Handle special messages if there is a problem
            await DataStore.CN.ChangeLoadingMessage("Unable to load Datafolder").ConfigureAwait(false);
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
                { "FileName", DataStore.AD.CurrentInputStreamPath },
                });

            if (!DataStore.AD.CurrentInputStreamValid)
            {
                return false;
            }

            await DataStore.CN.ChangeLoadingMessage("Loading GPKG data").ConfigureAwait(false);

            if (DataStore.AD.CurrentInputStreamValid)
            {
                // TODO create data folder await localStoreFile.SetDataFolderLocalStorage();

                // Clear image cache
                await ImageService.Instance.InvalidateCacheAsync(CacheType.All).ConfigureAwait(false);

                // TODO work out how to delte excess files based on keepign the ones in the GPKG file
                //// Delete directories of files. Assume files in root are ok
                // IReadOnlyList<StorageFolder> t = await DataStore.AD.CurrentDataFolder.GetFoldersAsync();

                // foreach (StorageFolder item in t) { await item.DeleteAsync(); }
                await localStoreFile.DecompressTAR().ConfigureAwait(false);

                // Save the current Index File modified date for later checking TODO How doe sthis
                // work if only loading picked file?
                // StoreFileNames.SaveFileModifiedSinceLastSave(CommonConstants.SettingsGPKGFileLastDateTimeModified, DataStore.AD.CurrentInputFile);
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);

            await DataStore.CN.ChangeLoadingMessage(null).ConfigureAwait(false);

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
            FileInfoEx fileGrampsDataInput = StoreFolder.FolderGetFile(DataStore.AD.CurrentDataFolder, CommonConstants.StorageGRAMPSFileName);

            if (fileGrampsDataInput != null)
            {
                if (deleteOld)
                {
                    // TODO fix this
                    //await localStoreFile.DataStorageInitialiseAsync(DataStore.AD.CurrentDataFolder).ConfigureAwait(false);
                }

                await localStoreFile.DecompressGZIP(fileGrampsDataInput).ConfigureAwait(false);

                // Save the current Index File modified date for later checking
                //StoreFileNames.SaveFileModifiedSinceLastSave(CommonConstants.SettingsGPRAMPSFileLastDateTimeModified, CommonConstants.StorageGRAMPSFileName);
                StoreFileNames.SaveFileModifiedSinceLastSave(CommonConstants.SettingsGPRAMPSFileLastDateTimeModified, fileGrampsDataInput);
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);

            await DataStore.CN.ChangeLoadingMessage(null).ConfigureAwait(false);

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
            // HockeyClient.Current.TrackEvent("TriggerLoadGrampsUnZippedFolderAsync");
            await DataStore.CN.ChangeLoadingMessage("Loading GRAMPS XML unzipped data").ConfigureAwait(false);
            {
                ClearRepositories();

                bool tt = await localExternalStorage.DataStorageLoadXML().ConfigureAwait(false);

                if (tt)
                {
                    await localExternalStorage.LoadXMLDataAsync().ConfigureAwait(false);

                    await DataStore.CN.MajorStatusAdd("Finished loading GRAMPS XML data").ConfigureAwait(false);

                    FileInfoEx t = StoreFolder.FolderGetFile(DataStore.AD.CurrentDataFolder, CommonConstants.StorageXMLFileName);

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

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);

            await DataStore.CN.ChangeLoadingMessage(null).ConfigureAwait(false);

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
                _CL.LogRoutineEntry("TriggerLoadSerialDataAsync");

                // await DataStore.CN.ChangeLoadingMessage("Checking for Serialised GRAMPS data").ConfigureAwait(false);
                if (DataStore.DS.IsDataLoaded == false)
                {
                    if (CommonLocalSettings.DataSerialised)
                    {
                        await DataStore.CN.ChangeLoadingMessage("Loading GRAMPS Serial data").ConfigureAwait(false);

                        localStoreSerial.DeSerializeRepository();

                        await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);

                        UpdateSettings();

                        await localPostLoad.LoadSerialUiItems().ConfigureAwait(false);

                        await DataStore.CN.ChangeLoadingMessage("GRAMPS Serial data load complete").ConfigureAwait(false);

                        // let everybody know we have finished loading data
                        _EventAggregator.GetEvent<DataLoadCompleteEvent>().Publish(null);
                    }
                    else
                    {
                        await DataStore.CN.ChangeLoadingMessage("GRAMPS Serial data load failed.").ConfigureAwait(false);

                        CommonLocalSettings.SetReloadDatabase();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonLocalSettings.DataSerialised = false;

                DataStore.CN.NotifyException("Trying to load existing serialised data", ex);

                CommonLocalSettings.SetReloadDatabase();

                throw;
            }

            _CL.LogRoutineExit("");

            return false;
        }

        private void UpdateSettings()
        {
            // save the database version
            CommonLocalSettings.DatabaseVersion = CommonConstants.GrampsViewDatabaseVersion;

            //// set FirstRun to false as we have loaded something
            //CommonLocalSettings.FirstRun = false;
        }
    }
}