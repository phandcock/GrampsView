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
        public static bool FixSingleMediaFile(IMediaModel argMediaModel)
        {
            try
            {
                if (argMediaModel.IsOriginalFilePathValid)
                {
                    //_CL.LogVariable("tt.OriginalFilePath", argMediaModel.OriginalFilePath); //
                    //_CL.LogVariable("localMediaFolder.path", DataStore.Instance.AD.CurrentDataFolder.FullName); //
                    //_CL.LogVariable("path", DataStore.Instance.AD.CurrentDataFolder.FullName + "\\" + argMediaModel.OriginalFilePath);

                    DataStore.Instance.DS.MediaData[argMediaModel.HLinkKey].MediaStorageFile = StoreFolder.FolderGetFile(DataStore.Instance.AD.CurrentDataFolder, argMediaModel.OriginalFilePath);
                }
            }
            catch (FileNotFoundException ex)
            {
                DataStore.Instance.CN.NotifyError(new ErrorInfo("FixSingleMediaFile", "File not found while loading media.Has the GRAMPS database been verified?") { { "Message", ex.Message }, { "Filename", argMediaModel.OriginalFilePath } });

                DataStore.Instance.CN.NotifyException("Trying to  add media file pointer", ex);
            }
            catch (Exception ex)
            {
                CommonLocalSettings.DataSerialised = false;
                DataStore.Instance.CN.NotifyException("Trying to add media file pointer", ex);

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
            _CommonLogging.RoutineEntry("FixMediaFiles");

            DirectoryInfo localMediaFolder = DataStore.Instance.AD.CurrentDataFolder;

            if (localMediaFolder != null)
            {
                await DataStore.Instance.CN.DataLogEntryAdd("Loading media file pointers").ConfigureAwait(false);

                foreach (IMediaModel item in DV.MediaDV.DataViewData)
                {
                    FixSingleMediaFile(item);
                }
            }

            _CommonLogging.RoutineExit(string.Empty);

            return true;
        }
    }
}