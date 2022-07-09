namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Assets.Fonts;
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using Microsoft.Extensions.DependencyInjection;

    using SharedSharp.Errors;

    using SharedSharp.Interfaces;

    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    public partial class StorePostLoad : ObservableObject, IStorePostLoad
    {
        public static ErrorInfo UtilGetPostGlyphErrorInfo(string argErrorText, MediaModel argMediaModel)
        {
            return new ErrorInfo(argErrorText)
                                 {
                                     { "Original ID", argMediaModel.Id },
                                     { "Original File", argMediaModel.MediaStorageFilePath },
                                 };
        }

        public async Task<ItemGlyph> GetThumbImageFromPDF(MediaModel argMediaModel)
        {
            ItemGlyph returnItemGlyph = argMediaModel.ModelItemGlyph;

            IMediaModel newMediaModel = UtilCreateNewMediaObject(argMediaModel, "~imagepdf", ".jpg");

            // TODO Having an issue where Gramps XML content type is not always correct
            if (argMediaModel.MediaStorageFile.FInfo.Extension != ".pdf")
            {
                _commonNotifications.DataLogEntryAdd($"??? {argMediaModel.Id} Inconsistant File Extension ({argMediaModel.MediaStorageFile.FInfo.Extension}) and MIME type ({argMediaModel.FileMimeType}/{argMediaModel.FileMimeSubType})");
                return argMediaModel.ModelItemGlyph;
            }

            IMediaModel pdfimage;

            // Check if new pdf image file already exists
            IMediaModel fileExists = DV.MediaDV.GetModelFromHLinkKey(newMediaModel.HLinkKey);

            if ((!fileExists.Valid) && (argMediaModel.IsMediaStorageFileValid))
            {
                // check if we can get an image for the first page of the PDF
                pdfimage = await _iocPlatformSpecific.GenerateThumbImageFromPDF(DataStore.Instance.AD.CurrentDataFolder.Value, argMediaModel, newMediaModel);

                returnItemGlyph = UtilSaveNewMediaObject(returnItemGlyph, pdfimage, IconFont.FilePdf);
            }
            else
            {
                ErrorInfo t = UtilGetPostGlyphErrorInfo("File not found when trying to create image from PDF file", argMediaModel);

                _commonNotifications.NotifyError(t);
            }

            return returnItemGlyph;
        }

        public async Task<ItemGlyph> GetThumbImageFromVideo(MediaModel argMediaModel)
        {
            ItemGlyph returnItemGlyph = argMediaModel.ModelItemGlyph;

            IMediaModel newMediaModel = UtilCreateNewMediaObject(argMediaModel, "~imagevideo", ".jpg");

            IMediaModel videoImage;

            // Check if new image file already exists
            IMediaModel fileExists = DV.MediaDV.GetModelFromHLinkKey(newMediaModel.HLinkKey);

            if ((!fileExists.Valid) && argMediaModel.IsMediaStorageFileValid)
            {
                // check if we can get an image for the video
                videoImage = await _iocPlatformSpecific.GenerateThumbImageFromVideo(DataStore.Instance.AD.CurrentDataFolder.Value, argMediaModel, newMediaModel);

                returnItemGlyph = UtilSaveNewMediaObject(returnItemGlyph, videoImage, IconFont.FileArchive);
            }
            else
            {
                ErrorInfo t = UtilGetPostGlyphErrorInfo("File not found when trying to create image from video file", argMediaModel);

                _commonNotifications.NotifyError(t);
            }

            return returnItemGlyph;
        }

        public ItemGlyph GetThumbImageFromZip(MediaModel argMediaModel)
        {
            try
            {
                ItemGlyph returnItemGlyph = argMediaModel.ModelItemGlyph;

                IMediaModel newMediaModel = UtilCreateNewMediaObject(argMediaModel, "~zipimage", ".jpg");

                // TODO Having an issue where Gramps XML content type is not always correct
                if (argMediaModel.MediaStorageFile.FInfo.Extension != ".zip")
                {
                    _commonNotifications.DataLogEntryAdd($"??? {argMediaModel.Id} Inconsistant File Extension ({argMediaModel.MediaStorageFile.FInfo.Extension}) and MIME type ({argMediaModel.FileMimeType}/{argMediaModel.FileMimeSubType})");
                    return argMediaModel.ModelItemGlyph;
                }

                IMediaModel zipimage;

                // Check if new zip image file already exists
                IMediaModel fileExists = DV.MediaDV.GetModelFromHLinkKey(newMediaModel.HLinkKey);

                if ((!fileExists.Valid) && (argMediaModel.IsMediaStorageFileValid))
                {
                    // check if we can get an image for the first page of the PDF

                    zipimage = StoreFile.ExtractZipFileFirstImage(DataStore.Instance.AD.CurrentDataFolder.Value, argMediaModel, newMediaModel);

                    returnItemGlyph = UtilSaveNewMediaObject(returnItemGlyph, zipimage, IconFont.FileArchive);
                }
                else
                {
                    ErrorInfo t = UtilGetPostGlyphErrorInfo("File not found when trying to create image from PDF file", argMediaModel);

                    _commonNotifications.NotifyError(t);
                }

                return returnItemGlyph;
            }
            catch (System.Exception ex)
            {
                ErrorInfo t = new ErrorInfo("Directory not found when trying to create image from PDF file")
                  {
                      { "Original ID", argMediaModel.Id },
                      { "Original File", argMediaModel.MediaStorageFilePath },
                      { "Clipped Id", argMediaModel.Id },
                      { "New path", "pdfimage" }
                  };

                App.Current.Services.GetService<IErrorNotifications>().NotifyException("PDF to Image", ex, t);

                return new ItemGlyph();
            }
        }

        private IMediaModel UtilCreateNewMediaObject(MediaModel argSourceMediaModel, string argNewMediaHLPrefix, string argNewMediaFileExtension)
        {
            IMediaModel newMediaModel = argSourceMediaModel.Clone();

            newMediaModel.InternalMediaFileOriginalHLink = argSourceMediaModel.HLinkKey;

            newMediaModel.HLinkKey.Value = argSourceMediaModel.HLinkKey.Value + argNewMediaHLPrefix;
            newMediaModel.OriginalFilePath = System.IO.Path.Combine(Constants.DirectoryImageCache, newMediaModel.HLinkKey.Value + argNewMediaFileExtension);

            return newMediaModel;
        }

        private ItemGlyph UtilSaveNewMediaObject(ItemGlyph argNewGlyph, IMediaModel argNewMediaModel, string argDefaultSymbol)
        {
            try
            {
                // Save new MediaObject
                if (argNewMediaModel.Valid)
                {
                    argNewMediaModel.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Image;
                    argNewMediaModel.IsInternalMediaFile = true; // Do not show in media list as it is internal
                    argNewMediaModel.MediaStorageFile = new FileInfoEx(argUseCurrentDataFolder: true, argFileName: argNewMediaModel.OriginalFilePath);

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
            catch (System.Exception ex)
            {
                ErrorInfo t = new ErrorInfo("Directory not found when trying to create image from PDF file")
                  {
                      { "Original ID", argNewMediaModel.Id },
                      { "Original File", argNewMediaModel.MediaStorageFilePath },
                      { "Clipped Id", argNewMediaModel.Id },
                      { "New path", "pdfimage" }
                  };

                App.Current.Services.GetService<IErrorNotifications>().NotifyException("PDF to Image", ex, t);

                return new ItemGlyph();
            }
        }
    }
}