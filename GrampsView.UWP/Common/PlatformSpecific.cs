namespace GrampsView.UWP
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using GrampsView.Events;
    using GrampsView.UWP.Common;

    using Prism.Events;

    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Windows.Storage;
    using Windows.Storage.FileProperties;

    /// <summary>
    /// UWP Platform specific code
    /// </summary>
    internal class PlatformSpecific : IPlatformSpecific
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformSpecific"/> class.
        /// </summary>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public PlatformSpecific(IEventAggregator iocEventAggregator)
        {
            iocEventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(UpdateTile, ThreadOption.UIThread);
        }

        /// <summary>
        /// Add PersonModel to the activities time line.
        /// </summary>
        /// <param name="argPersonModel">
        /// The person model.
        /// </param>
        public async Task ActivityTimeLineAdd(PersonModel argPersonModel)
        {
            try
            {
                await CommonTimeline.AddToTimeLine(CommonConstants.ModelNamePerson, argPersonModel, argPersonModel.GetDefaultText);
            }
            catch (Exception ex)
            {
                DataStore.Instance.CN.NotifyException("Exception when trying to add PersonDetailView to Windows Timneline", ex);
            }
        }

        /// <summary>
        /// Add FamilyModel to the activities time line.
        /// </summary>
        /// <param name="argFamilyModel">
        /// The argument family model.
        /// </param>
        public async Task ActivityTimeLineAdd(FamilyModel argFamilyModel)
        {
            try
            {
                await CommonTimeline.AddToTimeLine(CommonConstants.ModelNameFamily, argFamilyModel, argFamilyModel.FamilyDisplayName);
            }
            catch (Exception ex)
            {
                DataStore.Instance.CN.NotifyException("Exception when trying to add FamilyDetailView to Windows Timneline", ex);
            }
        }

        public async Task<Stream> GenerateThumbImageFromVideo(MediaModel argFile, long millisecond)
        {
            StorageFolder storageFolder = await StorageFolder.GetFolderFromPathAsync(argFile.MediaStorageFilePath);
            StorageFile videoFile = await storageFolder.GetFileAsync(argFile.MediaStorageFilePath);
            StorageItemThumbnail thumbnail = await videoFile.GetThumbnailAsync(ThumbnailMode.SingleItem);

            Stream stream = thumbnail.AsStream();
            byte[] bytes = new byte[Convert.ToUInt32(thumbnail.Size)];
            stream.Position = 0;
            await stream.ReadAsync(bytes, 0, bytes.Length);
            return new MemoryStream(bytes);
        }

        /// <summary>
        /// Updates the Windows Live Tile.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        private void UpdateTile()

        {
            CommonTileUpdate.UpdateTile();
        }
    }
}