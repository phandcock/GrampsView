namespace GrampsView.UWP
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Windows.Data.Pdf;
    using Windows.Storage;
    using Windows.Storage.FileProperties;
    using Windows.Storage.Streams;

    /// <summary>
    /// UWP Platform specific code
    /// </summary>
    internal partial class PlatformSpecific : IPlatformSpecific
    {
        public async Task<MediaModel> GenerateThumbImageFromPDF(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, MediaModel argNewMediaModel)
        {
            try
            {
                StorageFolder currentFolder = await StorageFolder.GetFolderFromPathAsync(argCurrentDataFolder.FullName);

                StorageFile file = await currentFolder.GetFileAsync(argExistingMediaModel.OriginalFilePath);

                // Load selected PDF file from the file picker.
                PdfDocument pdfDocument = await PdfDocument.LoadFromFileAsync(file);

                if (pdfDocument != null && pdfDocument.PageCount > 0)
                {
                    // Get page from a PDF file.
                    var pdfPage = pdfDocument.GetPage((uint)0);

                    if (pdfPage != null)
                    {
                        // Create image file.
                        StorageFile destinationFile = await currentFolder.CreateFileAsync(argNewMediaModel.OriginalFilePath);

                        if (destinationFile != null)
                        {
                            IRandomAccessStream randomStream = await destinationFile.OpenAsync(FileAccessMode.ReadWrite);

                            //Crerate PDF rendering options
                            PdfPageRenderOptions pdfPageRenderOptions = new PdfPageRenderOptions
                            {
                                DestinationWidth = (uint)(300)
                            };

                            // Render the PDF's page as stream.
                            await pdfPage.RenderToStreamAsync(randomStream, pdfPageRenderOptions);

                            await randomStream.FlushAsync();

                            //Dispose the random stream
                            randomStream.Dispose();

                            //Dispose the PDF's page.
                            pdfPage.Dispose();
                        }
                    }
                }

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
                ErrorInfo t = new ErrorInfo("Exception when trying to create image form PDF file")
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
            StorageFolder storageFolder = await StorageFolder.GetFolderFromPathAsync(argFile.MediaStorageFilePath);
            StorageFile videoFile = await storageFolder.GetFileAsync(argFile.MediaStorageFilePath);
            StorageItemThumbnail thumbnail = await videoFile.GetThumbnailAsync(ThumbnailMode.SingleItem);

            Stream stream = thumbnail.AsStream();
            byte[] bytes = new byte[Convert.ToUInt32(thumbnail.Size)];
            stream.Position = 0;
            await stream.ReadAsync(bytes, 0, bytes.Length);
            // return new MemoryStream(bytes);

            return new HLinkMediaModel();
        }
    }
}