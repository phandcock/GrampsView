namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using SharedSharp.Errors;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    public partial class StorePostLoad : ObservableObject, IStorePostLoad
    {
        private List<IMediaModel> addLater = new List<IMediaModel>();

        public static void SetEventImages()
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
                    argModel.ModelItemGlyph.ImageHLink = hlink.ImageHLink;
                    argModel.ModelItemGlyph.ImageSymbol = hlink.ImageSymbol;
                    argModel.ModelItemGlyph.ImageSymbolColour = hlink.ImageSymbolColour;
                }
            }
        }

        public static void SetFamilyImages()
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
                    argModel.ModelItemGlyph.ImageHLink = hlink.ImageHLink;
                    argModel.ModelItemGlyph.ImageSymbol = hlink.ImageSymbol;
                    argModel.ModelItemGlyph.ImageSymbolColour = hlink.ImageSymbolColour;
                }

                //var tt = (argModel.ModelItemGlyph.Symbol == CommonConstants.IconFamilies);
            }
        }

        public static void SetHeaderImages()
        {
        }

        public static void SetNameMapImages()
        {
        }

        public static void SetNotesImages()
        {
        }

        // People last as they pretty much depend on everything else
        public static void SetPersonImages()
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
                    argModel.ModelItemGlyph.ImageHLink = hlink.ImageHLink;
                    argModel.ModelItemGlyph.ImageSymbol = hlink.ImageSymbol;
                    argModel.ModelItemGlyph.ImageSymbolColour = hlink.ImageSymbolColour;
                }
            }
        }

        public static void SetPersonNameImages()
        {
        }

        public static void SetPlaceImages()
        {
        }

        public static void SetRepositoryImages()
        {
        }

        public static void SetSourceImages()
        {
            foreach (SourceModel argModel in DataStore.Instance.DS.SourceData.Values)
            {
                if (argModel.Id == "S0010")
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
                    argModel.ModelItemGlyph.ImageHLink = hlink.ImageHLink;
                    argModel.ModelItemGlyph.ImageSymbol = hlink.ImageSymbol;
                    argModel.ModelItemGlyph.ImageSymbolColour = hlink.ImageSymbolColour;
                }
            }
        }

        public static void SetTagImages()
        {
            //foreach (TagModel argModel in DataStore.Instance.DS.TagData.Values)
            //{
            //}
        }

        public void SetAddressImages()
        {
            foreach (AddressModel argModel in DataStore.Instance.DS.AddressData.Values)
            {
                if (argModel is null)
                {
                    throw new ArgumentNullException(nameof(argModel));
                }

                ItemGlyph hlink = argModel.ModelItemGlyph;

                // Try citation reference collection first
                ItemGlyph t = argModel.GCitationRefCollection.FirstHLinkHomeImage;
                if ((!hlink.ValidImage) && t.ValidImage)
                {
                    hlink = t;
                }

                // Handle the link if we can
                if (hlink.Valid)
                {
                    argModel.ModelItemGlyph.ImageType = hlink.ImageType;
                    argModel.ModelItemGlyph.ImageHLink = hlink.ImageHLink;
                    argModel.ModelItemGlyph.ImageSymbol = hlink.ImageSymbol;
                    argModel.ModelItemGlyph.ImageSymbolColour = hlink.ImageSymbolColour;
                }
                else
                {
                    ErrorInfo NotifyError = new ErrorInfo("HLink Invalid")
                    {
                        { "Address Model Image", argModel.Id }
                    };

                    _commonNotifications.NotifyError(NotifyError);
                }
            }
        }

        public void SetCitationImages()
        {
            foreach (CitationModel argModel in DataStore.Instance.DS.CitationData.Values)
            {
                if (argModel is null)
                {
                    throw new ArgumentNullException(nameof(argModel));
                }

                //if (argModel.ModelItemGlyph.ImageType == CommonEnums.HLinkGlyphType.Image)
                //{
                //}

                ItemGlyph hlink = argModel.ModelItemGlyph;

                if (argModel.Id == "C0772")
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
                    argModel.ModelItemGlyph.ImageHLink = hlink.ImageHLink;
                    argModel.ModelItemGlyph.ImageSymbol = hlink.ImageSymbol;
                    argModel.ModelItemGlyph.ImageSymbolColour = hlink.ImageSymbolColour;
                }
                else
                {
                    ErrorInfo NotifyError = new ErrorInfo("HLink Invalid")
                    {
                        { "Citation Model Image", argModel.Id }
                    };

                    _commonNotifications.NotifyError(NotifyError);
                }
            }
        }

        public async Task<bool> SetMediaImages()
        {
            // Save new mediaModels for later as we can not modify a list in the middle of a foreach loop
            addLater = new List<IMediaModel>();

            foreach (MediaModel argModel in DataStore.Instance.DS.MediaData.Values)
            {
                Contract.Requires(argModel != null);

                var t = from item in DataStore.Instance.DS.MediaData.Values

                        group item by (item.Id) into g

                        select new
                        {
                            GroupName = g.Key,
                            Items = g
                        };

                foreach (var item in t)
                {
                    if (item.Items.Count() > 1)
                    {
                    }
                }

                // Setup HomeImage
                argModel.ModelItemGlyph.ImageHLink = argModel.HLinkKey;

                if (argModel.Id == "O0629")
                {
                }

                switch (argModel.FileMimeType)
                {
                    case "application":
                        {
                            argModel.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Symbol;

                            switch (argModel.FileMimeSubType)
                            {
                                case "msword":
                                case "rtf":
                                    {
                                        argModel.ModelItemGlyph.Symbol = IconFont.FileWord;
                                        argModel.ModelItemGlyph.ImageSymbol = IconFont.FileWord;
                                        //argModel.ModelItemGlyph = await GetThumbImageFromPDF(argModel);
                                        break;
                                    }

                                case "pdf":
                                    {
                                        if (argModel.Id == "O0193")
                                        {
                                        }

                                        argModel.ModelItemGlyph.Symbol = IconFont.FilePdf;
                                        argModel.ModelItemGlyph.ImageSymbol = IconFont.FilePdf;

                                        argModel.ModelItemGlyph = await GetThumbImageFromPDF(argModel);
                                        break;
                                    }

                                case "vnd.ms-outlook":
                                    {
                                        argModel.ModelItemGlyph.Symbol = IconFont.MailBulk;
                                        argModel.ModelItemGlyph.ImageSymbol = IconFont.MailBulk;
                                        break;
                                    }

                                case "x-zip-compressed":
                                    {
                                        argModel.ModelItemGlyph.Symbol = IconFont.FileArchive;
                                        argModel.ModelItemGlyph.ImageSymbol = IconFont.FileArchive;

                                        argModel.ModelItemGlyph = GetThumbImageFromZip(argModel);
                                        break;
                                    }

                                case "zip":
                                    {
                                        argModel.ModelItemGlyph.Symbol = IconFont.FileArchive;
                                        argModel.ModelItemGlyph.ImageSymbol = IconFont.FileArchive;

                                        argModel.ModelItemGlyph = GetThumbImageFromZip(argModel);
                                        break;
                                    }

                                default:
                                    break;
                            }

                            break;
                        }

                    case "audio":
                        {
                            argModel.ModelItemGlyph.Symbol = IconFont.FileAudio;
                            argModel.ModelItemGlyph.ImageSymbol = IconFont.FileAudio;
                            break;
                        }

                    case "image":
                        {
                            argModel.ModelItemGlyph.Symbol = IconFont.FileImage;
                            argModel.ModelItemGlyph.ImageSymbol = IconFont.FileImage;

                            argModel.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Image;
                            argModel.ModelItemGlyph.ImageHLink = argModel.HLinkKey;

                            break;
                        }

                    case "text":
                        {
                            argModel.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Symbol;

                            switch (argModel.FileMimeSubType)
                            {
                                case "html":
                                    {
                                        argModel.ModelItemGlyph.Symbol = IconFont.Link;
                                        argModel.ModelItemGlyph.ImageSymbol = IconFont.Link;

                                        break;
                                    }

                                default:
                                    break;
                            }

                            break;
                        }

                    case "video":
                        {
                            argModel.ModelItemGlyph.Symbol = IconFont.FileVideo;
                            argModel.ModelItemGlyph.ImageSymbol = IconFont.FileVideo;

                            argModel.ModelItemGlyph = await GetThumbImageFromVideo(argModel);

                            // Override Glyph type to be media
                            argModel.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Media;
                            argModel.ModelItemGlyph.MediaHLink = argModel.HLinkKey;
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