// <copyright file="StoreCache.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.StoreCache
{
    using System;
    using System.Threading.Tasks;

    using GrampsView.Common;

    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using Xamarin.Forms;

    /// <summary>
    /// Cache of bitmaps of Media Items.
    /// </summary>
    public static class StoreCache
    {
        /// <summary>
        /// Check if the item exists in the cache.
        /// </summary>
        /// <param name="theMediaModel">
        /// HLink media model.
        /// </param>
        /// <returns>
        /// True or False if the Cache Item exists.
        /// </returns>
        public static async Task<bool> CacheItemExistsAsync(HLinkMediaModel theMediaModel)
        {
            return await DataStore.DS.CurrentThumbNailFolder.FileExists(ThumbNailGetFileName(theMediaModel));
        }

        /// <summary>
        /// Clears the Cache of Items.
        /// </summary>
        /// <returns>
        /// True if there Cache was deleted.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// Not implementated yet.
        /// </exception>
        public static bool ClearTheCache() => throw new NotImplementedException();

        /// <summary>
        /// Gets the cache item.
        /// </summary>
        /// <param name="theMediaModel">
        /// The media model.
        /// </param>
        /// <returns>
        /// Bitmaps of the Cache item.
        /// </returns>
        public static async Task<Image> GetCacheItem(HLinkMediaModel theMediaModel)
        {
            bool result = await CacheItemExistsAsync(theMediaModel);

            if (!result)
            {
                await SaveThumbNailAsync(theMediaModel);
            }

            Image t = await GetThumbNail(theMediaModel);

            return t;
        }

        // public static async Task<DateTime> getCacheFileDateTime(HLinkMediaModel theMediaModel) {
        // IStorageItem item = await DataStore.DS.CurrentThumbNailFolder.TryGetItemAsync(ThumbNailGetFileName(theMediaModel));

        // if (item == null) { return null; }

        // return item.DateCreated; }

        /// <summary>
        /// Gets the cache item.
        /// </summary>
        /// <param name="theMediaModel">
        /// The media model.
        /// </param>
        /// <returns>
        /// Bitmap of the media model.
        /// </returns>
        public static async Task<Image> GetCacheItem(MediaModel theMediaModel)
        {
            Image t = await GetCacheItem(theMediaModel.GetHLink);

            return t;
        }

        ///// <summary>
        ///// Saves the media object thumb nail.
        ///// </summary>
        ///// <param name="theHlinkMediaModel">
        ///// HLink to the media model.
        ///// </param>
        ///// <returns>
        ///// Nothing.
        ///// </returns>
        // public static async Task SaveThumbNailAsync(HLinkMediaModel theHlinkMediaModel) {
        // StorageFolder tt = DataStore.DS.CurrentThumbNailFolder; Stream pixelStream = new MemoryStream();

        // if (tt != null) { string newFile = ThumbNailGetFileName(theHlinkMediaModel);

        // try { bool fileFound = await StoreFileUtility.StorageItemExists(tt, newFile);

        // if (fileFound == false) { StorageFile t = await tt.CreateFileAsync(newFile, CreationCollisionOption.FailIfExists);

        // if (!theHlinkMediaModel.DeRef.IsThumbnailValid) { // Get the thumbnail var
        // storageItemThumbnail = await
        // theHlinkMediaModel.DeRef.MediaStorageFile.GetScaledImageAsThumbnailAsync(ThumbnailMode.SingleItem, 100);

        // StorageItemThumbnail tempThumb = storageItemThumbnail;

        // if (tempThumb != null) { Image image = new Image(100, 100); image.SetSource(tempThumb);
        // pixelStream = image.PixelBuffer.AsStream(); } else { // TODO Handle failure
        // DataStore.CN.NotifyError("Thumbnail not valid for " +
        // theHlinkMediaModel.DeRef.MediaStorageFile.DisplayName); } } else { pixelStream =
        // theHlinkMediaModel.DeRef.ImageThumbNail.PixelBuffer.AsStream(); }

        // // Save the file using (IRandomAccessStream stream = await
        // t.OpenAsync(FileAccessMode.ReadWrite)) { BitmapEncoder encoder = await
        // BitmapEncoder.CreateAsync(BitmapEncoder.BmpEncoderId, stream);

        // byte[] pixels = new byte[pixelStream.Length]; await pixelStream.ReadAsync(pixels, 0,
        // pixels.Length); encoder.SetPixelData( BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
        // (uint)theHlinkMediaModel.DeRef.ImageThumbNail.PixelWidth,
        // (uint)theHlinkMediaModel.DeRef.ImageThumbNail.PixelHeight, 96.0, 96.0, pixels); await
        // encoder.FlushAsync(); } } } catch (Exception ex) { // TODO Fix this
        // DataStore.CN.NotifyException(ex.Message + newFile, ex); } } }

        /// <summary>
        /// Saves the media object thumb nail.
        /// </summary>
        /// <param name="theHlinkMediaModel">
        /// HLink to the media model.
        /// </param>
        /// <returns>
        /// Nothing.
        /// </returns>
        public static async Task SaveThumbNailAsync(HLinkMediaModel theHlinkMediaModel)
        {
            //StorageFolder tt = DataStore.DS.CurrentThumbNailFolder;
            //Stream pixelStream = new MemoryStream();

            //await DispatcherHelper.ExecuteOnUIThreadAsync(async () =>
            // {
            //     Image t = new Image(100, 100);

            // if (tt != null) { string newFile = ThumbNailGetFileName(theHlinkMediaModel);

            // StorageFile newFileSF = await tt.CreateFileAsync(newFile, CreationCollisionOption.FailIfExists);

            // try { switch (theHlinkMediaModel.HomeImageType) { case
            // CommonConstants.HomeImageTypeClippedBitmap: { t = await
            // Common.CommonCropBitmap.GetClippedBitMapAsync(theHlinkMediaModel); break; }

            // case CommonConstants.HomeImageTypeSymbol: { break; }

            // case CommonConstants.HomeImageTypeThumbNail: { t = await
            // Common.CommonCropBitmap.GetThumbNailAsync(theHlinkMediaModel); break; }

            // default: break; }

            // pixelStream = t.PixelBuffer.AsStream();

            // // Save the file using (IRandomAccessStream stream = await
            // newFileSF.OpenAsync(FileAccessMode.ReadWrite)) { BitmapEncoder encoder = await
            // BitmapEncoder.CreateAsync(BitmapEncoder.BmpEncoderId, stream);

            // byte[] pixels = new byte[4 * t.PixelHeight * t.PixelWidth];

            // await pixelStream.ReadAsync(pixels, 0, pixels.Length);

            // encoder.SetPixelData( BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
            // (uint)t.PixelWidth, (uint)t.PixelHeight, 96.0, 96.0, pixels);

            // await encoder.FlushAsync(); } } catch (Exception ex) { // TODO Fix this
            // DataStore.CN.NotifyException(ex.Message + newFile, ex); } } } );
        }

        /// <summary>
        /// Thumbs the name of the nail get file.
        /// </summary>
        /// <param name="theHLinkMediaModel">
        /// The h link media model.
        /// </param>
        /// <returns>
        /// </returns>
        public static string ThumbNailGetFileName(HLinkMediaModel theHLinkMediaModel)
        {
            switch (theHLinkMediaModel.HomeImageType)
            {
                case CommonConstants.HomeImageTypeClippedBitmap:
                    {
                        return theHLinkMediaModel.HLinkKey + "-" + theHLinkMediaModel.GCorner1X + theHLinkMediaModel.GCorner1Y + theHLinkMediaModel.GCorner2X + theHLinkMediaModel.GCorner2Y + ".bmp";
                    }

                case CommonConstants.HomeImageTypeSymbol:
                    {
                        return null; // TODO show error
                    }

                case CommonConstants.HomeImageTypeThumbNail:
                    {
                        return theHLinkMediaModel.HLinkKey + ".bmp";
                    }

                default:
                    break;
            }

            return null;    // TODO Should never get here
        }

        /// <summary>
        /// Gets the thumb nail.
        /// </summary>
        /// <param name="theMediaModel">
        /// The media ViewModel.
        /// </param>
        /// <returns>
        /// </returns>
        private static async Task<Image> GetThumbNail(HLinkMediaModel theHLinkMediaModel)
        {
            var webImage = new Image { Aspect = Aspect.AspectFit };

            webImage.Source = ImageSource.FromFile(theHLinkMediaModel.DeRef.MediaStorageFile.FullPath);

            return webImage;

            // return await DispatcherHelper.ExecuteOnUIThreadAsync(async () => { Image resultValue =
            // new Image(1, 1);

            // string newFile = ThumbNailGetFileName(theHLinkMediaModel);

            // StorageFile bitMapFile = await DataStore.DS.CurrentThumbNailFolder.GetFileAsync(newFile);

            // BasicProperties ttt = await bitMapFile.GetBasicPropertiesAsync();

            // ulong tttt = ttt.Size;

            // if (tttt > 0) { using (IRandomAccessStream stream = await
            // bitMapFile.OpenAsync(FileAccessMode.Read)) { stream.Seek(0);

            // await resultValue.SetSourceAsync(stream); } }

            // // TODO Handle zero size if (resultValue.PixelHeight == 0 || resultValue.PixelWidth ==
            // 0) { DataStore.CN.NotifyError("Result bitmap is zero for file " +
            // bitMapFile.DisplayName); }

            //    return resultValue;
            //});

            // return resultValue; return new Image();
        }
    }
}