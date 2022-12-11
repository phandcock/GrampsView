using Android.Graphics;
using Android.Graphics.Pdf;
using Android.Media;
using Android.OS;

using GrampsView.Common.Interfaces;
using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Interfaces;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

namespace GrampsView.Platforms.Android.AppSpecific
{
    internal partial class GenerateThumbnails : IGenerateThumbnails
    {
        public async Task<IMediaModel> GenerateThumbImageFromPDF(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel)
        {
            IMediaModel returnValue = new MediaModel();

            try
            {
                await Task.Run(() =>
                {
                    string outFilePath = System.IO.Path.Combine(argCurrentDataFolder.FullName, argNewMediaModel.OriginalFilePath);

                    // Initialize PDFRenderer by passing PDF file from location.
                    PdfRenderer renderer = new(GetSeekableFileDescriptor(argCurrentDataFolder, argExistingMediaModel));
                    int pageCount = renderer.PageCount;

                    // Use `openPage` to open a specific page in PDF.
                    PdfRenderer.Page page = renderer.OpenPage(0);

                    //Creates bitmap
                    Bitmap bmp = Bitmap.CreateBitmap(page.Width, page.Height, Bitmap.Config.Argb8888);

                    //renderes page as bitmap, to use portion of the page use second and third parameter
                    page.Render(bmp, null, null, PdfRenderMode.ForDisplay);

                    //Save the bitmap
                    FileStream stream = new(outFilePath, FileMode.Create);
                    _ = bmp.Compress(Bitmap.CompressFormat.Png, 100, stream);
                    stream.Close();

                    page.Close();

                    returnValue = argNewMediaModel;
                });
            }
            catch (DirectoryNotFoundException ex)
            {
                ErrorInfo t = new("Directory not found when trying to create image from PDF file")
                                 {
                                     { "Original ID", argExistingMediaModel.Id },
                                     { "Original File", argExistingMediaModel.MediaStorageFilePath },
                                     { "Clipped Id", argNewMediaModel.Id },
                                     { "New path", "pdfimage" }
                                 };

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, t);

                return returnValue;
            }
            catch (Exception ex)
            {
                ErrorInfo t = new("Exception when trying to create image from PDF file")
                                 {
                                     { "Original ID", argExistingMediaModel.Id },
                                     { "Original File", argExistingMediaModel.MediaStorageFilePath },
                                     { "Clipped Id", argNewMediaModel.Id }
                                 };

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, t);

                return returnValue;
            }

            return returnValue;
        }

        public async Task<IMediaModel> GenerateThumbImageFromVideo(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel)
        {
            IMediaModel returnValue = new MediaModel();

            await Task.Run(() =>
            {
                string outFilePath = System.IO.Path.Combine(argCurrentDataFolder.FullName, argNewMediaModel.OriginalFilePath);

                MediaMetadataRetriever retriever = new();
                retriever.SetDataSource(argExistingMediaModel.MediaStorageFilePath);
                Bitmap bitmap = retriever.GetFrameAtTime(1000);  // try at 1 second as this is often a more flattering image

                if (bitmap != null)
                {
                    //Save the bitmap
                    FileStream outStream = new(outFilePath, FileMode.Create);
                    _ = bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, outStream);
                    outStream.Close();

                    returnValue = argNewMediaModel;
                }
            });

            return returnValue;
        }

        /// <summary>
        /// Method to retrieve PDF file from the location. Gets the seekable file descriptor.
        /// </summary>
        /// <param name="argCurrentDataFolder">
        /// The argument current data folder.
        /// </param>
        /// <param name="argFile">
        /// The argument file.
        /// </param>
        /// <returns>
        /// ParcelFileDescriptor
        /// </returns>
        private ParcelFileDescriptor GetSeekableFileDescriptor(DirectoryInfo argCurrentDataFolder, MediaModel argFile)
        {
            ParcelFileDescriptor fileDescriptor = null;
            try
            {
                fileDescriptor = ParcelFileDescriptor.Open(new Java.IO.File(argFile.MediaStorageFilePath), ParcelFileMode.ReadOnly);
            }
            catch (FileNotFoundException ex)
            {
                ErrorInfo t = new("GetSeekableFileDescriptor", "File not found exception when trying to create ParcelFileDescriptor")
                                 {
                                    { "Original ID", argFile.Id },
                                    { "Original File", argFile.OriginalFilePath },
                                    { "Storage Folder", argCurrentDataFolder.FullName },
                                    { "Root", System.IO.Path.Combine(argCurrentDataFolder.FullName, argFile.OriginalFilePath) }
                                 };

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, t);
            }
            catch (Exception ex)
            {
                ErrorInfo t = new("GetSeekableFileDescriptor", "Exception when trying to create ParcelFileDescriptor")
                                 {
                                    { "Original ID", argFile.Id },
                                    { "Original File", argFile.OriginalFilePath },
                                    { "Storage Folder", argCurrentDataFolder.FullName },
                                    { "Root", System.IO.Path.Combine(argCurrentDataFolder.FullName, argFile.OriginalFilePath) }
                                 };

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, t);
            }
            return fileDescriptor;
        }
    }
}