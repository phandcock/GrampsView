using Android.OS;

using GrampsView.Common.CustomClasses;
using GrampsView.Data.Model;

using System.IO;
using System.Threading.Tasks;

namespace GrampsView.Droid.Common
{
    internal partial class PlatformSpecific : IPlatformSpecific
    {
        public async Task<HLinkMediaModel> GenerateThumbImageFromPDF(DirectoryInfo argCurrentDataFolder, MediaModel argFile)
        {
            ////initialize PDFRenderer by passing PDF file from location.
            //PdfRenderer renderer = new PdfRenderer(GetSeekableFileDescriptor());
            //int pageCount = renderer.PageCount;
            //for (int i = 0; i < pageCount; i++)
            //{
            //    // Use `openPage` to open a specific page in PDF.
            //    Page page = renderer.OpenPage(i);

            // //Creates bitmap Bitmap bmp = Bitmap.CreateBitmap(page.Width, page.Height, Bitmap.Config.Argb8888);

            // //renderes page as bitmap, to use portion of the page use second and third parameter
            // page.Render(bmp, null, null, PdfRenderMode.ForDisplay);

            //    //Save the bitmap
            //    SaveImage(bmp);
            //    page.Close();
            //}

            return new HLinkMediaModel();
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
        private ParcelFileDescriptor GetSeekableFileDescriptor()
        {
            ParcelFileDescriptor fileDescriptor = null;
            try
            {
                string root = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/Syncfusion/sample.pdf";
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