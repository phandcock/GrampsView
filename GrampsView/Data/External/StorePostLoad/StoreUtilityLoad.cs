namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class StorePostLoad : ObservableObject, IStorePostLoad
    {
        public  bool FixSingleMediaFile(IMediaModel argMediaModel)
        {
            try
            {
                if (argMediaModel.IsOriginalFilePathValid)
                {
                    //_CL.LogVariable("tt.OriginalFilePath", argMediaModel.OriginalFilePath); //
                    //_CL.LogVariable("localMediaFolder.path", DataStore.Instance.AD.CurrentDataFolder.FullName); //
                    //_CL.LogVariable("path", DataStore.Instance.AD.CurrentDataFolder.FullName + "\\" + argMediaModel.OriginalFilePath);

                    DataStore.Instance.DS.MediaData[argMediaModel.HLinkKey.Value].MediaStorageFile = new FileInfoEx(argFileName: argMediaModel.OriginalFilePath, argUseCurrentDataFolder: true);
                }
            }
            catch (FileNotFoundException ex)
            {
                _commonNotifications.NotifyError(new ErrorInfo("FixSingleMediaFile", "File not found while loading media.Has the GRAMPS database been verified?") { { "Message", ex.Message }, { "Filename", argMediaModel.OriginalFilePath } });

                _commonNotifications.NotifyException("Trying to  add media file pointer", ex);
            }
            catch (Exception ex)
            {
                CommonLocalSettings.DataSerialised = false;
                _commonNotifications.NotifyException("Trying to add media file pointer", ex);

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

            if (DataStore.Instance.AD.CurrentDataFolder.Valid && DataStore.Instance.AD.CurrentImageAssetsFolder.Valid)
            {
                await _commonNotifications.DataLogEntryAdd("Loading media file pointers").ConfigureAwait(false);

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