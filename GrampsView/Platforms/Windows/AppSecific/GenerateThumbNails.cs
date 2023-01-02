using GrampsView.Common.Interfaces;
using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Interfaces;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using Windows.Data.Pdf;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;

using Buffer = Windows.Storage.Streams.Buffer;

namespace GrampsView.Common.CustomClasses
{
    /// <summary>
    /// UWP Platform specific code
    /// </summary>
    public partial class GenerateThumbNails : IGenerateThumbnails
    {
        public async partial Task<IMediaModel> GenerateThumbImageFromPDF(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel)
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
                    PdfPage pdfPage = pdfDocument.GetPage(0);

                    if (pdfPage != null)
                    {
                        // Create image file.
                        StorageFile destinationFile = await currentFolder.CreateFileAsync(argNewMediaModel.OriginalFilePath, CreationCollisionOption.ReplaceExisting);

                        if (destinationFile != null)
                        {
                            IRandomAccessStream randomStream = await destinationFile.OpenAsync(FileAccessMode.ReadWrite);

                            //Crerate PDF rendering options
                            PdfPageRenderOptions pdfPageRenderOptions = new()
                            {
                                DestinationWidth = 300
                            };

                            // Render the PDF's page as stream.
                            await pdfPage.RenderToStreamAsync(randomStream, pdfPageRenderOptions);

                            _ = await randomStream.FlushAsync();

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
                ErrorInfo t = new("Directory not found when trying to create image from PDF file")
                                 {
                                     { "Original ID", argExistingMediaModel.Id },
                                     { "Original File", argExistingMediaModel.MediaStorageFilePath },
                                     { "Clipped Id", argNewMediaModel.Id },
                                     { "New path", "pdfimage" }
                                 };

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, t);

                return new MediaModel();
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

                return new MediaModel();
            }
        }

        public async partial Task<IMediaModel> GenerateThumbImageFromVideo(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel)
        {
            StorageFolder currentFolder = await StorageFolder.GetFolderFromPathAsync(argCurrentDataFolder.FullName);

            StorageFile outfile = await currentFolder.CreateFileAsync(argNewMediaModel.OriginalFilePath, CreationCollisionOption.ReplaceExisting);

            //if (outfile.Name == "_e9e27fbe8ed34e9b554a0ba93aa~imagevideo.jpg")
            //{
            //}

            StorageFile videoFile = await currentFolder.GetFileAsync(argExistingMediaModel.OriginalFilePath);

            StorageItemThumbnail thumbnail = await videoFile.GetThumbnailAsync(ThumbnailMode.SingleItem);

            Buffer MyBuffer = new(Convert.ToUInt32(thumbnail.Size));
            IBuffer iBuf = await thumbnail.ReadAsync(MyBuffer, MyBuffer.Capacity, InputStreamOptions.None);

            IRandomAccessStream strm = await outfile.OpenAsync(FileAccessMode.ReadWrite);

            _ = await strm.WriteAsync(iBuf);

            _ = await strm.FlushAsync();

            strm.Dispose();

            // check size
            BasicProperties outProperties = await outfile.GetBasicPropertiesAsync();
            return outProperties.Size == 0 ? new MediaModel() : argNewMediaModel;
        }
    }
}