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

            MediaModel newMediaModel = argMediaModel.Copy();

            newMediaModel.HLinkKey = argMediaModel.HLinkKey + "-pdfimage";
            newMediaModel.OriginalFilePath = System.IO.Path.Combine("pdfimage", newMediaModel.HLinkKey + ".jpg");
            newMediaModel.IsClippedFile = true; // Do not show in media list as it is internal
            newMediaModel.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Image;

            string outFilePath = System.IO.Path.Combine(DataStore.Instance.AD.CurrentDataFolder.FullName, newMediaModel.OriginalFilePath);

            // TODO Having an issue where Gramps XML content type is not always correct
            if (argMediaModel.MediaStorageFile.FInfo.Extension != ".pdf")
            {
                return new ItemGlyph();
            }

            // create folder if required
            DataStore.Instance.AD.CurrentDataFolder.CreateSubdirectory("pdfimage");

            // Check if new pdf image file already exists
            IMediaModel fileExists = DV.MediaDV.GetModelFromHLinkString(newMediaModel.HLinkKey);

            MediaModel pdfimage;

            if ((!fileExists.Valid) && (argMediaModel.IsMediaStorageFileValid))
            {
                // check if we can get an image for the first page of the PDF
                pdfimage = await _iocPlatformSpecific.GenerateThumbImageFromPDF(DataStore.Instance.AD.CurrentDataFolder, argMediaModel, newMediaModel);

                // ------------ Save new MediaObject

                if (pdfimage.Valid)
                {
                    pdfimage.MediaStorageFile = StoreFolder.FolderGetFile(DataStore.Instance.AD.CurrentDataFolder, pdfimage.OriginalFilePath);

                    addLater.Add(pdfimage);

                    returnItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Image;
                    returnItemGlyph.HLinkMediHLink = pdfimage.HLinkKey;
                }
                else
                {
                    returnItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Symbol;
                    returnItemGlyph.ImageSymbol = CommonFontNamesFAS.FilePdf;
                }
            }
            else
            {
                ErrorInfo t = new ErrorInfo("File not found when trying to create image form PDF file")
                                 {
                                     { "Original ID", argMediaModel.Id },
                                     { "Original File", argMediaModel.MediaStorageFilePath },
                                     { "Clipped Id", argMediaModel.DeRef.Id }
                                 };

                DataStore.Instance.CN.NotifyError(t);
            }

            return returnItemGlyph;
        }

        public async Task<ItemGlyph> GetThumbImageFromZip(MediaModel argMediaModel)
        {
            ItemGlyph returnItemGlyph = argMediaModel.ModelItemGlyph;

            MediaModel newMediaModel = argMediaModel.Copy();

            newMediaModel.HLinkKey = argMediaModel.HLinkKey + "-zipimage";
            newMediaModel.OriginalFilePath = System.IO.Path.Combine("zipimage", newMediaModel.HLinkKey + ".jpg");

            string outFilePath = System.IO.Path.Combine(DataStore.Instance.AD.CurrentDataFolder.FullName, newMediaModel.OriginalFilePath);

            // TODO Having an issue where Gramps XML content type is not always correct
            if (argMediaModel.MediaStorageFile.FInfo.Extension != ".zip")
            {
                return new ItemGlyph();
            }

            // create folder if required
            DataStore.Instance.AD.CurrentDataFolder.CreateSubdirectory("zipimage");

            // Check if new pdf image file already exists
            IMediaModel fileExists = DV.MediaDV.GetModelFromHLinkString(newMediaModel.HLinkKey);

            MediaModel zipimage;

            if ((!fileExists.Valid) && (argMediaModel.IsMediaStorageFileValid))
            {
                // check if we can get an image for the first page of the PDF

                zipimage = await StoreFile.ExtractZipFileFirstImage(DataStore.Instance.AD.CurrentDataFolder, argMediaModel, newMediaModel);

                // ------------ Save new MediaObject
                zipimage.MediaStorageFile = StoreFolder.FolderGetFile(DataStore.Instance.AD.CurrentDataFolder, newMediaModel.OriginalFilePath);
                zipimage.IsClippedFile = true; // Do not show in media list as it is internal

                if (zipimage.Valid)
                {
                    addLater.Add(zipimage);

                    returnItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Image;
                    returnItemGlyph.HLinkMediHLink = zipimage.HLinkKey;
                }
                else
                {
                    returnItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Symbol;
                    returnItemGlyph.ImageSymbol = CommonFontNamesFAS.FilePdf;
                }
            }
            else
            {
                ErrorInfo t = new ErrorInfo("File not found when trying to create image form PDF file")
                                 {
                                     { "Original ID", argMediaModel.Id },
                                     { "Original File", argMediaModel.MediaStorageFilePath },
                                     { "Clipped Id", argMediaModel.DeRef.Id }
                                 };

                DataStore.Instance.CN.NotifyError(t);
            }

            return returnItemGlyph;
        }
    }
}