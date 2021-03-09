namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;

    public partial class StorePostLoad : CommonBindableBase, IStorePostLoad
    {
        private List<MediaModel> addLater = new List<MediaModel>();

        public async static Task SetAddressImages()
        {
        }

        public async static Task SetCitationImages()
        {
            foreach (CitationModel argModel in DataStore.Instance.DS.CitationData.Values)
            {
                if (argModel is null)
                {
                    throw new ArgumentNullException(nameof(argModel));
                }

                if (argModel.ModelItemGlyph.ImageType == CommonEnums.HLinkGlyphType.Image)
                {
                }

                ItemGlyph hlink = argModel.ModelItemGlyph;

                if (argModel.Id == "C0682")
                {
                }

                // Try media reference collection first
                ItemGlyph t = argModel.GMediaRefCollection.FirstHLinkHomeImage;
                if ((!hlink.ValidImage) && (t.ValidImage))
                {
                    hlink = t;
                }

                // Check Source for Image
                t = argModel.GSourceRef.DeRef.ModelItemGlyph;
                if ((!hlink.ValidImage) && (t.ValidImage))
                {
                    hlink = t;
                }

                // Handle the link if we can
                if (hlink.Valid)
                {
                    argModel.ModelItemGlyph.ImageType = hlink.ImageType;
                    argModel.ModelItemGlyph.ImageHLinkMediHLink = hlink.ImageHLinkMediHLink;
                    argModel.ModelItemGlyph.ImageSymbol = hlink.ImageSymbol;
                    argModel.ModelItemGlyph.ImageSymbolColour = hlink.ImageSymbolColour;
                }
            }

            return;
        }

        public async static Task SetEventImages()
        {
            foreach (EventModel argModel in DataStore.Instance.DS.EventData.Values)
            {
                if (argModel.Id == "E0203")
                {
                }

                if (argModel is null)
                {
                    throw new ArgumentNullException(nameof(argModel));
                }

                // Setup home images
                ItemGlyph hlink = argModel.ModelItemGlyph;

                // check media
                ItemGlyph t = argModel.GMediaRefCollection.FirstHLinkHomeImage;
                if (!hlink.ValidImage & t.ValidImage)
                {
                    hlink = t;
                }

                // Check Citation for Images
                t = argModel.GCitationRefCollection.FirstHLinkHomeImage;
                if (!hlink.ValidImage & t.ValidImage)
                {
                    hlink = t;
                }

                // Handle the link if we can
                if (hlink.Valid)
                {
                    argModel.ModelItemGlyph.ImageType = hlink.ImageType;
                    argModel.ModelItemGlyph.ImageHLinkMediHLink = hlink.ImageHLinkMediHLink;
                    argModel.ModelItemGlyph.ImageSymbol = hlink.ImageSymbol;
                    argModel.ModelItemGlyph.ImageSymbolColour = hlink.ImageSymbolColour;
                }
            }
        }

        public async static Task SetFamilyImages()
        {
            foreach (FamilyModel argModel in DataStore.Instance.DS.FamilyData.Values)
            {
                if (argModel.Id == "F0144")
                {
                }

                ItemGlyph hlink = argModel.ModelItemGlyph;

                // check media
                ItemGlyph t = argModel.GMediaRefCollection.FirstHLinkHomeImage;
                if (!hlink.ValidImage & t.ValidImage)
                {
                    hlink = t;
                }

                t = argModel.GCitationRefCollection.FirstHLinkHomeImage;
                if (!hlink.ValidImage & t.ValidImage)
                {
                    hlink = t;
                }

                t = argModel.GEventRefCollection.FirstHLinkHomeImage;
                if (!hlink.ValidImage & t.ValidImage)
                {
                    hlink = t;
                }

                t = argModel.GNoteRefCollection.FirstHLinkHomeImage;
                if (!hlink.ValidImage & t.ValidImage)
                {
                    hlink = t;
                }

                // Set the image if available
                if (hlink.Valid)
                {
                    argModel.ModelItemGlyph.ImageType = hlink.ImageType;
                    argModel.ModelItemGlyph.ImageHLinkMediHLink = hlink.ImageHLinkMediHLink;
                    argModel.ModelItemGlyph.ImageSymbol = hlink.ImageSymbol;
                    argModel.ModelItemGlyph.ImageSymbolColour = hlink.ImageSymbolColour;
                }

                var tt = (argModel.ModelItemGlyph.Symbol == CommonConstants.IconFamilies);
            }
        }

        public async static Task SetHeaderImages()
        {
        }

        public async static Task SetNameMapImages()
        {
        }

        //            //// People last as they pretty much depend on everything else
        //            //await SetPersonImages().ConfigureAwait(false);
        //        }
        //    }
        //}
        public async static Task SetNotesImages()
        {
        }

        // People last as they pretty much depend on everything else
        public async static Task SetPersonImages()
        {
            foreach (PersonModel argModel in DataStore.Instance.DS.PersonData.Values)
            {
                if (argModel.Id == "I0704")
                {
                }

                if (argModel is null)
                {
                    throw new ArgumentNullException(nameof(argModel));
                }

                ItemGlyph hlink = argModel.ModelItemGlyph;

                // check media
                ItemGlyph t = argModel.GMediaRefCollection.FirstHLinkHomeImage;
                if (!hlink.ValidImage & t.ValidImage)
                {
                    hlink = t;
                }

                // Check Citation for Images
                t = argModel.GCitationRefCollection.FirstHLinkHomeImage;
                if (!hlink.ValidImage & t.ValidImage)
                {
                    hlink = t;
                }

                // Check Events for Images
                t = argModel.GEventRefCollection.FirstHLinkHomeImage;
                if (!hlink.ValidImage & t.ValidImage)
                {
                    hlink = t;
                }

                // Action any Link
                if (hlink.Valid)
                {
                    argModel.ModelItemGlyph.ImageType = hlink.ImageType;
                    argModel.ModelItemGlyph.ImageHLinkMediHLink = hlink.ImageHLinkMediHLink;
                    argModel.ModelItemGlyph.ImageSymbol = hlink.ImageSymbol;
                    argModel.ModelItemGlyph.ImageSymbolColour = hlink.ImageSymbolColour;
                }
            }
        }

        public async static Task SetPersonNameImages()
        {
        }

        public async static Task SetPlaceImages()
        {
        }

        public async static Task SetRepositoryImages()
        {
        }

        public static async Task SetSourceImages()
        {
            foreach (SourceModel argModel in DataStore.Instance.DS.SourceData.Values)
            {
                if (argModel.Id == "S0299")
                {
                }

                if (argModel.ModelItemGlyph.SymbolColour != CommonRoutines.ResourceColourGet("CardBackGroundSource"))
                {
                }

                if (argModel is null)
                {
                    throw new ArgumentNullException(nameof(argModel));
                }

                ItemGlyph hlink = argModel.ModelItemGlyph;

                // Get default image if available
                ItemGlyph t = argModel.GMediaRefCollection.FirstHLinkHomeImage;
                if (!hlink.ValidImage && t.ValidImage)
                {
                    hlink = t;
                }

                // Set default
                if (hlink.Valid)
                {
                    argModel.ModelItemGlyph.ImageType = hlink.ImageType;
                    argModel.ModelItemGlyph.ImageHLinkMediHLink = hlink.ImageHLinkMediHLink;
                    argModel.ModelItemGlyph.ImageSymbol = hlink.ImageSymbol;
                    argModel.ModelItemGlyph.ImageSymbolColour = hlink.ImageSymbolColour;
                }
            }
        }

        public async static Task SetTagImages()
        {
            foreach (TagModel argModel in DataStore.Instance.DS.TagData.Values)
            {
            }
        }

        public async Task<bool> SetMediaImages()
        {
            // Save new mediaModels for later as we can not modify a list in the middle of a foreach loop
            addLater = new List<MediaModel>();

            foreach (MediaModel argModel in DataStore.Instance.DS.MediaData.Values)
            {
                Contract.Requires(argModel != null);

                if (argModel.Id == "O0328")
                {
                }

                // Setup HomeImage
                argModel.ModelItemGlyph.ImageHLinkMediHLink = argModel.HLinkKey;

                switch (argModel.FileMimeType)
                {
                    case "application":
                        {
                            argModel.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Symbol;

                            switch (argModel.FileMimeSubType)
                            {
                                case "pdf":
                                    {
                                        argModel.ModelItemGlyph.Symbol = CommonFontNamesFAS.FilePdf;
                                        argModel.ModelItemGlyph.ImageSymbol = CommonFontNamesFAS.FilePdf;
                                        argModel.ModelItemGlyph = await GetThumbImageFromPDF(argModel);
                                        break;
                                    }

                                case "x-zip-compressed":
                                    {
                                        argModel.ModelItemGlyph.Symbol = CommonFontNamesFAS.FileArchive;
                                        argModel.ModelItemGlyph.ImageSymbol = CommonFontNamesFAS.FileArchive;
                                        argModel.ModelItemGlyph = await GetThumbImageFromZip(argModel);
                                        break;
                                    }

                                case "zip":
                                    {
                                        argModel.ModelItemGlyph.Symbol = CommonFontNamesFAS.FileArchive;
                                        argModel.ModelItemGlyph.ImageSymbol = CommonFontNamesFAS.FileArchive;
                                        argModel.ModelItemGlyph = await GetThumbImageFromZip(argModel);
                                        break;
                                    }
                            }

                            break;
                        }

                    case "audio":
                        {
                            argModel.ModelItemGlyph.Symbol = CommonFontNamesFAS.FileAudio;
                            argModel.ModelItemGlyph.ImageSymbol = CommonFontNamesFAS.FileAudio;
                            break;
                        }

                    case "image":
                        {
                            argModel.ModelItemGlyph.Symbol = CommonFontNamesFAS.FileImage;
                            argModel.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Image;
                            argModel.ModelItemGlyph.ImageHLinkMediHLink = argModel.HLinkKey;
                            argModel.ModelItemGlyph.ImageSymbol = CommonFontNamesFAS.FileImage;
                            break;
                        }

                    case "video":
                        {
                            argModel.ModelItemGlyph.Symbol = CommonFontNamesFAS.FileVideo;
                            argModel.ModelItemGlyph.ImageSymbol = CommonFontNamesFAS.FileVideo;
                            argModel.ModelItemGlyph = await GetThumbImageFromVideo(argModel);

                            // Override Glyph type to be media
                            argModel.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Media;
                            argModel.ModelItemGlyph.MediaHLinkMediHLink = argModel.HLinkKey;
                            break;
                        }
                }
            }

            foreach (MediaModel item in addLater)
            {
                DataStore.Instance.DS.MediaData.Add(item);
            }

            return true;
        }
    }
}