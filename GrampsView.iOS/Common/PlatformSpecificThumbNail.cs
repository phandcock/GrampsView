namespace GrampsView.iOS.Common
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;

    using System.IO;
    using System.Threading.Tasks;

    internal partial class PlatformSpecific : IPlatformSpecific
    {
        public async Task<MediaModel> GenerateThumbImageFromPDF(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel)
        {
            //MemoryStream stream = new MemoryStream();
            //// Create memory stream from file stream.
            //fileStream.CopyTo(stream);
            //// Create data provider from bytes.
            //CGDataProvider provider = new CGDataProvider(stream.ToArray());
            //try
            //{
            //    //Load a PDF file.
            //    m_pdfDcument = new CGPDFDocument(provider);
            //}
            //catch (Exception)
            //{
            //}
            ////Get PDF's page and convert as image.
            //using (CGPDFPage pdfPage = m_pdfDcument.GetPage(2))
            //{
            //    //initialise image context.
            //    UIGraphics.BeginImageContext(pdfPage.GetBoxRect(CGPDFBox.Media).Size);
            //    // get current context.
            //    CGContext context = UIGraphics.GetCurrentContext();
            //    context.SetFillColor(1.0f, 1.0f, 1.0f, 1.0f);
            //    // Gets page's bounds.
            //    CGRect bounds = new CGRect(pdfPage.GetBoxRect(CGPDFBox.Media).X, pdfPage.GetBoxRect(CGPDFBox.Media).Y, pdfPage.GetBoxRect(CGPDFBox.Media).Width, pdfPage.GetBoxRect(CGPDFBox.Media).Height);
            //    if (pdfPage != null)
            //    {
            //        context.FillRect(bounds);
            //        context.TranslateCTM(0, bounds.Height);
            //        context.ScaleCTM(1.0f, -1.0f);
            //        context.ConcatCTM(pdfPage.GetDrawingTransform(CGPDFBox.Crop, bounds, 0, true));
            //        context.SetRenderingIntent(CGColorRenderingIntent.Default);
            //        context.InterpolationQuality = CGInterpolationQuality.Default;
            //        // Draw PDF page in the context.
            //        context.DrawPDFPage(pdfPage);
            //        // Get image from current context.
            //        pdfImage = UIGraphics.GetImageFromCurrentImageContext();
            //        UIGraphics.EndImageContext();
            //    }
            //}

            //// Get bytes from UIImage object.
            //using (var imageData = pdfImage.AsPNG())
            //{
            //    imageBytes = new byte[imageData.Length];
            //    System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, imageBytes, 0, Convert.ToInt32(imageData.Length));
            //    //return bytes;
            //}
            ////Create image from bytes.
            //imageStream = new MemoryStream(imageBytes);
            ////Save the image. It is a custom method to save the image
            //Save("PDFtoImage.png", "image/png", imageStream);

            return new MediaModel();
        }

        public async Task<MediaModel> GenerateThumbImageFromVideo(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, MediaModel argNewMediaModel)
        {
            //var asset = AVAsset.FromUrl(NSUrl.FromFilename(filePath));
            //var imageGenerator = AVAssetImageGenerator.FromAsset(asset);
            //imageGenerator.AppliesPreferredTrackTransform = true;

            //var actualTime = asset.Duration;

            //CoreMedia.CMTime cmTime = new CoreMedia.CMTime(millisecond, 1000000);

            //NSError error;

            //var cgImage = imageGenerator.CopyCGImageAtTime(cmTime, out actualTime, out error);
            //return new UIImage(cgImage).AsJPEG().AsStream();

            return new MediaModel();
        }
    }
}