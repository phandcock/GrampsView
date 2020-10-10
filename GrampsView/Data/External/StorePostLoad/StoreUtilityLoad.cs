// <copyright file="GrampsStorePostLoad.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class StorePostLoad : CommonBindableBase, IStorePostLoad
    {
        public async static Task<bool> FixSingleMediaFile(IMediaModel argMediaModel)
        {
            try
            {
                if (argMediaModel.IsOriginalFilePathValid)
                {
                    //_CL.LogVariable("tt.OriginalFilePath", argMediaModel.OriginalFilePath); //
                    //_CL.LogVariable("localMediaFolder.path", DataStore.AD.CurrentDataFolder.FullName); //
                    //_CL.LogVariable("path", DataStore.AD.CurrentDataFolder.FullName + "\\" + argMediaModel.OriginalFilePath);

                    DataStore.DS.MediaData[argMediaModel.HLinkKey].MediaStorageFile = StoreFolder.FolderGetFile(DataStore.AD.CurrentDataFolder, argMediaModel.OriginalFilePath);
                }
            }
            catch (FileNotFoundException ex)
            {
                DataStore.CN.NotifyError("File (" + argMediaModel.OriginalFilePath + ") not found while   loading media. Has the GRAMPS database been verified ? " + ex.ToString());

                DataStore.CN.NotifyException("Trying to  add media file pointer", ex);
            }
            catch (Exception ex)
            {
                CommonLocalSettings.DataSerialised = false;
                DataStore.CN.NotifyException("Trying to add media file pointer", ex);

           
                throw;
            }

            return false;
        }

        /// <summary>
        /// Fixes the media files.
        /// </summary>
        /// <returns>
        /// true.
        /// </returns>
        public async Task<bool> FixMediaFiles()
        {
            _CommonLogging.LogRoutineEntry("FixMediaFiles");

            DirectoryInfo localMediaFolder = DataStore.AD.CurrentDataFolder;

            if (localMediaFolder != null)
            {
                await DataStore.CN.DataLogEntryAdd("Loading media file pointers").ConfigureAwait(false);

                foreach (IMediaModel item in DV.MediaDV.DataViewData)
                {
                    await FixSingleMediaFile(item).ConfigureAwait(false);
                }
            }

            _CommonLogging.LogRoutineExit(string.Empty);

            return true;
        }
    }
}