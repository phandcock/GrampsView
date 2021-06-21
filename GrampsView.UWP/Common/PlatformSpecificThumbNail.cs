namespace GrampsView.UWP
{
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
        public async Task<IMediaModel> GenerateThumbImageFromPDF(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel)
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
            catch (DirectoryNotFoundException ex)
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
            catch (Exception ex)
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

        public async Task<IMediaModel> GenerateThumbImageFromVideo(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel)
        {
            StorageFolder currentFolder = await StorageFolder.GetFolderFromPathAsync(argCurrentDataFolder.FullName);

            StorageFile outfile = await currentFolder.CreateFileAsync(argNewMediaModel.OriginalFilePath);

            if (outfile.Name == "_e9e27fbe8ed34e9b554a0ba93aa~imagevideo.jpg")
            {
            }

            StorageFile videoFile = await currentFolder.GetFileAsync(argExistingMediaModel.OriginalFilePath);

            StorageItemThumbnail thumbnail = await videoFile.GetThumbnailAsync(ThumbnailMode.SingleItem);

            //if (thumbnail.Type == ThumbnailType.Image)
            //{
            //BitmapImage bitmap = new BitmapImage();
            //bitmap.SetSource(await videoFile.GetThumbnailAsync(ThumbnailMode.SingleItem));

            //Stream stream = thumbnail.AsStream();
            //byte[] bytes = new byte[Convert.ToUInt32(thumbnail.Size)];
            //stream.Position = 0;

            //await stream.ReadAsync(bytes, 0, bytes.Length);

            Windows.Storage.Streams.Buffer MyBuffer = new Windows.Storage.Streams.Buffer(Convert.ToUInt32(thumbnail.Size));
            IBuffer iBuf = await thumbnail.ReadAsync(MyBuffer, MyBuffer.Capacity, InputStreamOptions.None);

            var strm = await outfile.OpenAsync(FileAccessMode.ReadWrite);

            await strm.WriteAsync(iBuf);

            await strm.FlushAsync();

            strm.Dispose();

            // check size
            BasicProperties outProperties = await outfile.GetBasicPropertiesAsync();
            if (outProperties.Size == 0)
            {
                return new MediaModel();
            }

            return argNewMediaModel;
        }
    }
}