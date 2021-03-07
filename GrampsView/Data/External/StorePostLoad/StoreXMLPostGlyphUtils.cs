namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System.Threading.Tasks;

    public partial class StorePostLoad : CommonBindableBase, IStorePostLoad
    {
        public async Task<ItemGlyph> GetThumbImageFromPDF(MediaModel argMediaModel)
        {
            ItemGlyph returnItemGlyph = argMediaModel.ModelItemGlyph;

            MediaModel newMediaModel = UtilCreateNewMediaObject(argMediaModel, "~imagepdf", ".jpg");

            //MediaModel newMediaModel = argMediaModel.Copy();
            //newMediaModel.HLinkKey = argMediaModel.HLinkKey + "~imagepdf";
            //newMediaModel.OriginalFilePath = System.IO.Path.Combine(CommonConstants.fileToImageSubDirectory, newMediaModel.HLinkKey + ".jpg");
            //newMediaModel.MediaStorageFile = StoreFolder.FolderGetFile(DataStore.Instance.AD.CurrentDataFolder, newMediaModel.OriginalFilePath);

            //string outFilePath = System.IO.Path.Combine(DataStore.Instance.AD.CurrentDataFolder.FullName, newMediaModel.OriginalFilePath);

            // TODO Having an issue where Gramps XML content type is not always correct
            if (argMediaModel.MediaStorageFile.FInfo.Extension != ".pdf")
            {
                return new ItemGlyph();
            }

            //// create folder if required
            //DataStore.Instance.AD.CurrentDataFolder.CreateSubdirectory(CommonConstants.fileToImageSubDirectory);

            MediaModel pdfimage;

            // Check if new pdf image file already exists
            IMediaModel fileExists = DV.MediaDV.GetModelFromHLinkString(newMediaModel.HLinkKey);

            if ((!fileExists.Valid) && (argMediaModel.IsMediaStorageFileValid))
            {
                // check if we can get an image for the first page of the PDF
                pdfimage = await _iocPlatformSpecific.GenerateThumbImageFromPDF(DataStore.Instance.AD.CurrentDataFolder, argMediaModel, newMediaModel);

                returnItemGlyph = UtilSaveNewMediaObject(returnItemGlyph, pdfimage, CommonFontNamesFAS.FilePdf);
            }
            else
            {
                ErrorInfo t = UtilGetPostGlyphErrorInfo("File not found when trying to create image from PDF file", argMediaModel);

                DataStore.Instance.CN.NotifyError(t);
            }

            return returnItemGlyph;
        }

        public async Task<ItemGlyph> GetThumbImageFromVideo(MediaModel argMediaModel)
        {
            ItemGlyph returnItemGlyph = argMediaModel.ModelItemGlyph;

            MediaModel newMediaModel = UtilCreateNewMediaObject(argMediaModel, "~imagevideo", ".jpg");

            //MediaModel newMediaModel = argMediaModel.Copy();
            //newMediaModel.HLinkKey = argMediaModel.HLinkKey + "~imagevideo";
            //newMediaModel.OriginalFilePath = System.IO.Path.Combine(CommonConstants.fileToImageSubDirectory, newMediaModel.HLinkKey + ".jpg");
            //newMediaModel.MediaStorageFile = StoreFolder.FolderGetFile(DataStore.Instance.AD.CurrentDataFolder, newMediaModel.OriginalFilePath);

            //// create folder if required
            //DataStore.Instance.AD.CurrentDataFolder.CreateSubdirectory(CommonConstants.fileToImageSubDirectory);

            MediaModel videoImage;

            // Check if new image file already exists
            IMediaModel fileExists = DV.MediaDV.GetModelFromHLinkString(newMediaModel.HLinkKey);

            if ((!fileExists.Valid) && (argMediaModel.IsMediaStorageFileValid))
            {
                // check if we can get an image for the video
                videoImage = await _iocPlatformSpecific.GenerateThumbImageFromVideo(DataStore.Instance.AD.CurrentDataFolder, argMediaModel, newMediaModel);

                returnItemGlyph = UtilSaveNewMediaObject(returnItemGlyph, videoImage, CommonFontNamesFAS.FileArchive);
            }
            else
            {
                ErrorInfo t = UtilGetPostGlyphErrorInfo("File not found when trying to create image from video file", argMediaModel);

                DataStore.Instance.CN.NotifyError(t);
            }

            return returnItemGlyph;
        }

        public async Task<ItemGlyph> GetThumbImageFromZip(MediaModel argMediaModel)
        {
            ItemGlyph returnItemGlyph = argMediaModel.ModelItemGlyph;

            MediaModel newMediaModel = UtilCreateNewMediaObject(argMediaModel, "~zipimage", ".jpg");

            //MediaModel newMediaModel = argMediaModel.Copy();
            //newMediaModel.HLinkKey = argMediaModel.HLinkKey + "~zipimage";
            //newMediaModel.OriginalFilePath = System.IO.Path.Combine(CommonConstants.fileToImageSubDirectory, newMediaModel.HLinkKey + ".jpg");
            //newMediaModel.MediaStorageFile = StoreFolder.FolderGetFile(DataStore.Instance.AD.CurrentDataFolder, newMediaModel.OriginalFilePath);

            // TODO Having an issue where Gramps XML content type is not always correct
            if (argMediaModel.MediaStorageFile.FInfo.Extension != ".zip")
            {
                return new ItemGlyph();
            }

            //// create folder if required
            //DataStore.Instance.AD.CurrentDataFolder.CreateSubdirectory(CommonConstants.fileToImageSubDirectory);

            MediaModel zipimage;

            // Check if new zip image file already exists
            IMediaModel fileExists = DV.MediaDV.GetModelFromHLinkString(newMediaModel.HLinkKey);

            if ((!fileExists.Valid) && (argMediaModel.IsMediaStorageFileValid))
            {
                // check if we can get an image for the first page of the PDF

                zipimage = await StoreFile.ExtractZipFileFirstImage(DataStore.Instance.AD.CurrentDataFolder, argMediaModel, newMediaModel);

                returnItemGlyph = UtilSaveNewMediaObject(returnItemGlyph, zipimage, CommonFontNamesFAS.FileArchive);
            }
            else
            {
                ErrorInfo t = UtilGetPostGlyphErrorInfo("File not found when trying to create image form PDF file", argMediaModel);

                DataStore.Instance.CN.NotifyError(t);
            }

            return returnItemGlyph;
        }

        public ErrorInfo UtilGetPostGlyphErrorInfo(string argErrorText, MediaModel argMediaModel)
        {
            return new ErrorInfo(argErrorText)
                                 {
                                     { "Original ID", argMediaModel.Id },
                                     { "Original File", argMediaModel.MediaStorageFilePath },
                                     { "Clipped Id", argMediaModel.DeRef.Id }
                                 };
        }

        private MediaModel UtilCreateNewMediaObject(MediaModel argSourceMediaModel, string argNewMediaHLPrefix, string argNewMediaFileExtension)
        {
            // create folder if required
            DataStore.Instance.AD.CurrentDataFolder.CreateSubdirectory(CommonConstants.fileToImageSubDirectory);

            MediaModel newMediaModel = argSourceMediaModel.Copy();
            newMediaModel.HLinkKey = argSourceMediaModel.HLinkKey + argNewMediaHLPrefix;
            newMediaModel.OriginalFilePath = System.IO.Path.Combine(CommonConstants.fileToImageSubDirectory, newMediaModel.HLinkKey + argNewMediaFileExtension);

            return newMediaModel;
        }

        private ItemGlyph UtilSaveNewMediaObject(ItemGlyph argNewGlyph, MediaModel argNewMediaModel, string argDefaultSymbol)
        {
            // ------------ Save new MediaObject
            if (argNewMediaModel.Valid)
            {
                argNewMediaModel.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Image;
                argNewMediaModel.IsInternalMediaFile = true; // Do not show in media list as it is internal
                argNewMediaModel.MediaStorageFile = StoreFolder.FolderGetCreateFile(DataStore.Instance.AD.CurrentDataFolder, argNewMediaModel.OriginalFilePath);

                addLater.Add(argNewMediaModel);

                argNewGlyph.ImageType = CommonEnums.HLinkGlyphType.Image;
                argNewGlyph.HLinkMediHLink = argNewMediaModel.HLinkKey;

                return argNewGlyph;
            }

            // Else

            argNewGlyph.ImageType = CommonEnums.HLinkGlyphType.Symbol;
            argNewGlyph.ImageSymbol = argDefaultSymbol;

            return argNewGlyph;
        }
    }
}