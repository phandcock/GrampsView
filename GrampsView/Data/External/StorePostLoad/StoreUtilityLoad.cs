// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.Data.ExternalStorage
{
    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class StorePostLoad : ObservableObject, IStorePostLoad
    {
        ///// <summary>
        ///// Fixes the media files.
        ///// </summary>
        ///// <returns>
        ///// true.
        ///// </returns>
        //public Task<bool> FixMediaFiles()
        //{
        //    _CommonLogging.RoutineEntry("FixMediaFiles");

        //    if (DataStore.Instance.AD.CurrentDataFolder.Valid && DataStore.Instance.AD.CurrentImageAssetsFolder.Valid)
        //    {
        //        _CommonLogging.DataLogEntryAdd("Loading media file pointers");

        //        foreach (IMediaModel item in DV.MediaDV.DataViewData)
        //        {
        //            _ = FixSingleMediaFile(item);
        //        }
        //    }

        //    _CommonLogging.RoutineExit(string.Empty);

        //    return Task.FromResult(true);
        //}

        //public bool FixSingleMediaFile(IMediaModel argMediaModel)
        //{
        //    try
        //    {
        //        if (argMediaModel.IsOriginalFilePathValid)
        //        {
        //            //_CL.LogVariable("tt.OriginalFilePath", argMediaModel.OriginalFilePath); //
        //            //_CL.LogVariable("localMediaFolder.path", DataStore.Instance.AD.CurrentDataFolder.FullName); //
        //            //_CL.LogVariable("path", DataStore.Instance.AD.CurrentDataFolder.FullName + "\\" + argMediaModel.OriginalFilePath);

        //            DataStore.Instance.DS.MediaData[argMediaModel.HLinkKey.Value].MediaStorageFile = new FileInfoEx(argFileName: argMediaModel.OriginalFilePath, argUseCurrentDataFolder: true);
        //        }
        //    }
        //    catch (FileNotFoundException ex)
        //    {
        //        _commonNotifications.NotifyError(new ErrorInfo("FixSingleMediaFile", "File not found while loading media.Has the GRAMPS database been verified?") { { "Message", ex.Message }, { "Filename", argMediaModel.OriginalFilePath } });

        //        _commonNotifications.NotifyException("Trying to  add media file pointer", ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        SharedSharpSettings.DataSerialised = false;
        //        _commonNotifications.NotifyException("Trying to add media file pointer", ex);

        //      
        //    }

        //    return false;
        //}
    }
}