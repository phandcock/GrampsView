namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Assets.Fonts;
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System.Threading.Tasks;

    public partial class StorePostLoad : CommonBindableBase, IStorePostLoad
    {
        public static ErrorInfo UtilGetPostGlyphErrorInfo(string argErrorText, MediaModel argMediaModel)
        {
            return new ErrorInfo(argErrorText)
                                 {
                                     { "Original ID", argMediaModel.Id },
                                     { "Original File", argMediaModel.MediaStorageFilePath },
                                     { "Clipped Id", argMediaModel.DeRef.Id }
                                 };
        }

        public async Task<ItemGlyph> GetThumbImageFromPDF(MediaModel argMediaModel)
        {
            ItemGlyph returnItemGlyph = argMediaModel.ModelItemGlyph;

            MediaModel newMediaModel = UtilCreateNewMediaObject(argMediaModel, "~imagepdf", ".jpg");

            // TODO Having an issue where Gramps XML content type is not always correct
            if (argMediaModel.MediaStorageFile.FInfo.Extension != ".pdf")
            {
                return new ItemGlyph();
            }

            MediaModel pdfimage;

            // Check if new pdf image file already exists
            IMediaModel fileExists = DV.MediaDV.GetModelFromHLinkKey(newMediaModel.HLinkKey);

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
            if (argMediaModel.Id == "O0354")
            {
            }

            ItemGlyph returnItemGlyph = argMediaModel.ModelItemGlyph;

            MediaModel newMediaModel = UtilCreateNewMediaObject(argMediaModel, "~imagevideo", ".jpg");

            MediaModel videoImage;

            // Check if new image file already exists
            IMediaModel fileExists = DV.MediaDV.GetModelFromHLinkKey(newMediaModel.HLinkKey);

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

        public ItemGlyph GetThumbImageFromZip(MediaModel argMediaModel)
        {
            ItemGlyph returnItemGlyph = argMediaModel.ModelItemGlyph;

            MediaModel newMediaModel = UtilCreateNewMediaObject(argMediaModel, "~zipimage", ".jpg");

            // TODO Having an issue where Gramps XML content type is not always correct
            if (argMediaModel.MediaStorageFile.FInfo.Extension != ".zip")
            {
                return new ItemGlyph();
            }

            MediaModel zipimage;

            // Check if new zip image file already exists
            IMediaModel fileExists = DV.MediaDV.GetModelFromHLinkKey(newMediaModel.HLinkKey);

            if ((!fileExists.Valid) && (argMediaModel.IsMediaStorageFileValid))
            {
                // check if we can get an image for the first page of the PDF

                zipimage = StoreFile.ExtractZipFileFirstImage(DataStore.Instance.AD.CurrentDataFolder, argMediaModel, newMediaModel);

                returnItemGlyph = UtilSaveNewMediaObject(returnItemGlyph, zipimage, CommonFontNamesFAS.FileArchive);
            }
            else
            {
                ErrorInfo t = UtilGetPostGlyphErrorInfo("File not found when trying to create image form PDF file", argMediaModel);

                DataStore.Instance.CN.NotifyError(t);
            }

            return returnItemGlyph;
        }

        private MediaModel UtilCreateNewMediaObject(MediaModel argSourceMediaModel, string argNewMediaHLPrefix, string argNewMediaFileExtension)
        {
            // create folder if required
            DataStore.Instance.AD.CurrentDataFolder.CreateSubdirectory(CommonConstants.fileToImageSubDirectory);

            MediaModel newMediaModel = argSourceMediaModel.Copy();

            newMediaModel.InternalMediaFileOriginalHLink = argSourceMediaModel.HLinkKey;

            newMediaModel.HLinkKey.Value = argSourceMediaModel.HLinkKey.Value + argNewMediaHLPrefix;
            newMediaModel.OriginalFilePath = System.IO.Path.Combine(CommonConstants.fileToImageSubDirectory, newMediaModel.HLinkKey.Value + argNewMediaFileExtension);

            return newMediaModel;
        }

        private ItemGlyph UtilSaveNewMediaObject(ItemGlyph argNewGlyph, MediaModel argNewMediaModel, string argDefaultSymbol)
        {
            // Save new MediaObject
            if (argNewMediaModel.Valid)
            {
                argNewMediaModel.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Image;
                argNewMediaModel.IsInternalMediaFile = true; // Do not show in media list as it is internal
                argNewMediaModel.MediaStorageFile = StoreFolder.FolderGetCreateFile(DataStore.Instance.AD.CurrentDataFolder, argNewMediaModel.OriginalFilePath);

                addLater.Add(argNewMediaModel);

                argNewGlyph.ImageType = CommonEnums.HLinkGlyphType.Image;
                argNewGlyph.ImageHLink = argNewMediaModel.HLinkKey;

                return argNewGlyph;
            }

            // Else

            argNewGlyph.ImageType = CommonEnums.HLinkGlyphType.Symbol;
            argNewGlyph.ImageSymbol = argDefaultSymbol;

            return argNewGlyph;
        }
    }
}