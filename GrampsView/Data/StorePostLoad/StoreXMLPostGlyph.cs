// Copyright (c) phandcock. All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.Data.Repository;
using GrampsView.Data.StorePostLoad;
using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Interfaces;
using GrampsView.Models.DataModels.Minor;

using SharedSharp.Errors;

using System.Diagnostics.Contracts;

namespace GrampsView.Data.ExternalStorage
{
    /// <summary>
    /// This code tries to find an image that can be used for each model. It searches the images in
    /// my priority order and chooses the first one it finds.
    /// </summary>
    /// TODO Edit XML Comment Template for StorePostLoad
    public partial class StorePostLoad : ObservableObject, IStorePostLoad
    {
        private List<IMediaModel> addLater = new();

        public static void SetEventImages()
        {
            foreach (EventModel argModel in DL.EventDL.DataAsList)
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
            foreach (FamilyModel argModel in DL.FamilyDL.DataAsList)
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

                //var tt = (argModel.ModelItemGlyph.Symbol == Constants.IconFamilies);
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
                    ErrorInfo NotifyError = new("HLink Invalid")
                    {
                        { "Address Model Image", argModel.Id }
                    };

                    _commonNotifications.NotifyError(NotifyError);
                }
            }
        }

        public void SetCitationImages()
        {
            foreach (CitationModel argModel in DL.CitationDL.DataAsList)
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
                if ((!hlink.ValidImage) && t.ValidImage)
                {
                    hlink = t;
                }

                // Check Source for Image
                t = argModel.GSourceRef.DeRef.ModelItemGlyph;
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
                    ErrorInfo NotifyError = new("HLink Invalid")
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

                        group item by item.Id into g

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

                if (argModel.Id == "O0336")
                {
                }

                switch (argModel.FileMimeType)
                {
                    case "application":
                        {
                            // Default to a symobol then try to find somethig better.
                            argModel.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Symbol;

                            switch (argModel.FileMimeSubType)
                            {
                                case "msword":
                                case "rtf":
                                    {
                                        argModel.ModelItemGlyph.Symbol = IconMaterialIconsOutline.Wordpress;
                                        argModel.ModelItemGlyph.ImageSymbol = IconMaterialIconsOutline.Wordpress;
                                        //argModel.ModelItemGlyph = await GetThumbImageFromPDF(argModel);
                                        break;
                                    }

                                case "pdf":
                                    {
                                        if (argModel.Id == "O0193")
                                        {
                                        }

                                        argModel.ModelItemGlyph.Symbol = IconMaterialIconsOutline.Picture_as_pdf;
                                        argModel.ModelItemGlyph.ImageSymbol = IconMaterialIconsOutline.Picture_as_pdf;

                                        argModel.ModelItemGlyph = await GetThumbImageFromPDF(argModel);
                                        break;
                                    }

                                case "vnd.ms-outlook":
                                    {
                                        argModel.ModelItemGlyph.Symbol = IconMaterialIconsOutline.Voicemail;
                                        argModel.ModelItemGlyph.ImageSymbol = IconMaterialIconsOutline.Voicemail;
                                        break;
                                    }

                                case "x-zip-compressed":
                                    {
                                        argModel.ModelItemGlyph.Symbol = IconMaterialIconsOutline.Compress;
                                        argModel.ModelItemGlyph.ImageSymbol = IconMaterialIconsOutline.Compress;

                                        argModel.ModelItemGlyph = GetThumbImageFromZip(argModel);
                                        break;
                                    }

                                case "zip":
                                    {
                                        argModel.ModelItemGlyph.Symbol = IconMaterialIconsOutline.Folder_zip;
                                        argModel.ModelItemGlyph.ImageSymbol = IconMaterialIconsOutline.Folder_zip;

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
                            argModel.ModelItemGlyph.Symbol = IconMaterialIconsOutline.Audio_file;
                            argModel.ModelItemGlyph.ImageSymbol = IconMaterialIconsOutline.Audio_file;
                            break;
                        }

                    case "image":
                        {
                            argModel.ModelItemGlyph.Symbol = IconMaterialIconsOutline.Image;
                            argModel.ModelItemGlyph.ImageSymbol = IconMaterialIconsOutline.Image;

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
                                        argModel.ModelItemGlyph.Symbol = IconMaterialIconsOutline.Link;
                                        argModel.ModelItemGlyph.ImageSymbol = IconMaterialIconsOutline.Link;

                                        break;
                                    }

                                default:
                                    break;
                            }

                            break;
                        }

                    case "video":
                        {
                            argModel.ModelItemGlyph.Symbol = IconMaterialIconsOutline.Videocam;
                            argModel.ModelItemGlyph.ImageSymbol = IconMaterialIconsOutline.Videocam;

                            argModel.ModelItemGlyph = await GetThumbImageFromVideo(argModel);

                            // Override Glyph type to be media
                            argModel.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Media;
                            argModel.ModelItemGlyph.MediaHLink = argModel.HLinkKey;
                            break;
                        }
                }
            }

            // Add in all of the new media in one go to decrease processing and avoid changing the
            // underlying repository while we are cycling through it.
            foreach (MediaModel item in addLater.Cast<MediaModel>())
            {
                DataStore.Instance.DS.MediaData.Add(item);
            }

            return true;
        }
    }
}