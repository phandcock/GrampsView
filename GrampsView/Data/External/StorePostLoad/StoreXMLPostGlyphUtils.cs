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
                pdfimage.MediaStorageFile = StoreFolder.FolderGetFile(DataStore.Instance.AD.CurrentDataFolder, newMediaModel.OriginalFilePath);
                pdfimage.IsClippedFile = true; // Do not show in media list as it is internal

                if (pdfimage.Valid)
                {
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
    }
}