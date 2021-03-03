namespace GrampsView.UWP
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data;
    using GrampsView.Data.DataView;
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
        public async Task<MediaModel> GenerateThumbImageFromPDF(DirectoryInfo argCurrentDataFolder, MediaModel argFile)
        {
            try
            {
                StorageFolder currentFolder = await StorageFolder.GetFolderFromPathAsync(argCurrentDataFolder.FullName);

                StorageFile file = await currentFolder.GetFileAsync(argFile.OriginalFilePath);

                if (file.ContentType != "application/pdf")
                {
                    return new MediaModel();
                }

                if (file.ContentType != argFile.FileContentType)
                {
                }

                MediaModel newMediaModel = new MediaModel();

                // Load selected PDF file from the file picker.
                PdfDocument pdfDocument = await PdfDocument.LoadFromFileAsync(file);

                if (pdfDocument != null && pdfDocument.PageCount > 0)
                {
                    //  for (int pageIndex = 0; pageIndex < pdfDocument.PageCount; pageIndex++)
                    //  {
                    //Get page from a PDF file.
                    var pdfPage = pdfDocument.GetPage((uint)0); //  pageIndex); ;

                    if (pdfPage != null)
                    {
                        string newHLinkKey = argFile.HLinkKey + "-pdfimage";
                        string outFileName = Path.Combine("pdfimage", newHLinkKey + ".jpg");

                        string outFilePath = Path.Combine(DataStore.Instance.AD.CurrentDataFolder.FullName, outFileName);

                        // Check if already exists
                        IMediaModel fileExists = DV.MediaDV.GetModelFromHLinkString(newHLinkKey);

                        if ((!fileExists.Valid) && (argFile.IsMediaStorageFileValid))
                        {
                            //Create image file.
                            StorageFile destinationFile = await currentFolder.CreateFileAsync(outFileName);

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

                            // ------------ Save new MediaObject
                            newMediaModel = argFile.Copy();
                            newMediaModel.HLinkKey = newHLinkKey;

                            newMediaModel.OriginalFilePath = outFileName;
                            newMediaModel.MediaStorageFile = StoreFolder.FolderGetFile(DataStore.Instance.AD.CurrentDataFolder, outFileName);
                            newMediaModel.IsClippedFile = true; // Do not show in media list as it is internal
                        }
                        else
                        {
                            ErrorInfo t = new ErrorInfo("File not found when trying to create image form PDF file")
                                 {
                                     { "Original ID", argFile.Id },
                                     { "Original File", argFile.MediaStorageFilePath },
                                     { "Clipped Id", argFile.DeRef.Id }
                                 };

                            DataStore.Instance.CN.NotifyError(t);
                        }
                        // }
                    }
                }

                return newMediaModel;
            }
            catch (Exception ex)
            {
                throw;
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