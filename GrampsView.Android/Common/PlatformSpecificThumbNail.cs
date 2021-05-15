using Android.Graphics;
using Android.Graphics.Pdf;
using Android.Media;
using Android.OS;

using GrampsView.Common.CustomClasses;
using GrampsView.Data.Model;
using GrampsView.Data.Repository;

using System;
using System.IO;
using System.Threading.Tasks;

using static Android.Graphics.Pdf.PdfRenderer;

namespace GrampsView.Droid.Common
{
    internal partial class PlatformSpecific : IPlatformSpecific
    {
        public async Task<MediaModel> GenerateThumbImageFromPDF(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, MediaModel argNewMediaModel)
        {
            try
            {
                string outFilePath = System.IO.Path.Combine(argCurrentDataFolder.FullName, argNewMediaModel.OriginalFilePath);

                // Initialize PDFRenderer by passing PDF file from location.
                PdfRenderer renderer = new PdfRenderer(GetSeekableFileDescriptor(argCurrentDataFolder, argExistingMediaModel));
                int pageCount = renderer.PageCount;

                // Use `openPage` to open a specific page in PDF.
                Page page = renderer.OpenPage(0);

                //Creates bitmap
                Bitmap bmp = Bitmap.CreateBitmap(page.Width, page.Height, Bitmap.Config.Argb8888);

                //renderes page as bitmap, to use portion of the page use second and third parameter
                page.Render(bmp, null, null, PdfRenderMode.ForDisplay);

                //Save the bitmap
                var stream = new FileStream(outFilePath, FileMode.Create);
                bmp.Compress(Bitmap.CompressFormat.Png, 100, stream);
                stream.Close();

                page.Close();

                return argNewMediaModel;
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                ErrorInfo t = new ErrorInfo("Directory not found when trying to create image from PDF file")
                                 {
                                     { "Original ID", argExistingMediaModel.Id },
                                     { "Original File", argExistingMediaModel.MediaStorageFilePath },
                                     { "Clipped Id", argExistingMediaModel.DeRef.Id },
                                     { "New path", "pdfimage" }
                                 };

                DataStore.CN.NotifyException("PDF to Image", ex, t);

                return new MediaModel();
            }
            catch (System.Exception ex)
            {
                ErrorInfo t = new ErrorInfo("Exception when trying to create image from PDF file")
                                 {
                                     { "Original ID", argExistingMediaModel.Id },
                                     { "Original File", argExistingMediaModel.MediaStorageFilePath },
                                     { "Clipped Id", argExistingMediaModel.DeRef.Id }
                                 };

                DataStore.CN.NotifyException("PDF to Image", ex, t);

                return new MediaModel();
            }
        }

        public async Task<MediaModel> GenerateThumbImageFromVideo(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, MediaModel argNewMediaModel)
        {
            string outFilePath = System.IO.Path.Combine(argCurrentDataFolder.FullName, argNewMediaModel.OriginalFilePath);

            MediaMetadataRetriever retriever = new MediaMetadataRetriever();
            retriever.SetDataSource(argExistingMediaModel.MediaStorageFilePath);
            Android.Graphics.Bitmap bitmap = retriever.GetFrameAtTime(1000);  // try at 1 second as this is often a more flattering image

            if (bitmap != null)
            {
                // MemoryStream stream = new MemoryStream();
                // bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 80, stream);

                //Save the bitmap
                var outStream = new FileStream(outFilePath, FileMode.Create);
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, outStream);
                outStream.Close();

                return argNewMediaModel;
            }

            return new MediaModel();
        }

        //Method to retrieve PDF file from the location
        private ParcelFileDescriptor GetSeekableFileDescriptor(DirectoryInfo argCurrentDataFolder, MediaModel argFile)
        {
            ParcelFileDescriptor fileDescriptor = null;
            try
            {
                // string root = System.IO.Path.Combine(argCurrentDataFolder.FullName, argFile.OriginalFilePath);
                fileDescriptor = ParcelFileDescriptor.Open(new Java.IO.File(argFile.MediaStorageFilePath), ParcelFileMode.ReadOnly);
            }
            catch (FileNotFoundException ex)
            {
                ErrorInfo t = new ErrorInfo("File not found exception when trying to create ParcelFileDescriptor")
                                 {
                                    { "Original ID", argFile.Id },
                                    { "Original File", argFile.OriginalFilePath },
                                    { "Storage Folder", argCurrentDataFolder.FullName },
                                    { "Root", System.IO.Path.Combine(argCurrentDataFolder.FullName, argFile.OriginalFilePath) }
                                 };

                DataStore.CN.NotifyException("GetSeekableFileDescriptor", ex, t);
            }
            catch (Exception ex)
            {
                ErrorInfo t = new ErrorInfo("Exception when trying to create ParcelFileDescriptor")
                                 {
                                    { "Original ID", argFile.Id },
                                    { "Original File", argFile.OriginalFilePath },
                                    { "Storage Folder", argCurrentDataFolder.FullName },
                                    { "Root", System.IO.Path.Combine(argCurrentDataFolder.FullName, argFile.OriginalFilePath) }
                                 };

                DataStore.CN.NotifyException("GetSeekableFileDescriptor", ex, t);
            }
            return fileDescriptor;
        }
    }
}