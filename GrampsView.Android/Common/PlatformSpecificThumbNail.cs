using Android.Graphics;
using Android.Graphics.Pdf;
using Android.OS;

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Model;
using GrampsView.Data.Repository;

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
                // Initialize PDFRenderer by passing PDF file from location.
                PdfRenderer renderer = new PdfRenderer(GetSeekableFileDescriptor(argCurrentDataFolder, argNewMediaModel));
                int pageCount = renderer.PageCount;

                // Use `openPage` to open a specific page in PDF.
                Page page = renderer.OpenPage(0);

                //Creates bitmap
                Bitmap bmp = Bitmap.CreateBitmap(page.Width, page.Height, Bitmap.Config.Argb8888);

                //renderes page as bitmap, to use portion of the page use second and third parameter
                page.Render(bmp, null, null, PdfRenderMode.ForDisplay);

                //Save the bitmap
                var stream = new FileStream(argNewMediaModel.OriginalFilePath, FileMode.Create);
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

                DataStore.Instance.CN.NotifyException("PDF to Image", ex, t);

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

                DataStore.Instance.CN.NotifyException("PDF to Image", ex, t);

                return new MediaModel();
            }
        }

        public async Task<HLinkMediaModel> GenerateThumbImageFromVideo(DirectoryInfo argCurrentDataFolder, MediaModel argFile, long millisecond)
        {
            //MediaMetadataRetriever retriever = new MediaMetadataRetriever();
            //retriever.SetDataSource(argFile.MediaStorageFilePath);
            //Android.Graphics.Bitmap bitmap = retriever.GetFrameAtTime(millisecond);

            //if (bitmap != null)
            //{
            //    MemoryStream stream = new MemoryStream();
            //    bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 80, stream);
            //    return stream;
            //}

            return new HLinkMediaModel();
        }

        //Method to retrieve PDF file from the location
        private ParcelFileDescriptor GetSeekableFileDescriptor(DirectoryInfo argCurrentDataFolder, MediaModel argFile)
        {
            ParcelFileDescriptor fileDescriptor = null;
            try
            {
                string root = System.IO.Path.Combine(argCurrentDataFolder.FullName, argFile.OriginalFilePath);
                fileDescriptor = ParcelFileDescriptor.Open(new Java.IO.File(root), ParcelFileMode.ReadOnly
                );
            }
            catch (FileNotFoundException e)
            {
            }
            return fileDescriptor;
        }
    }
}