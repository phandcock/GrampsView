//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="GrampsStoreXMLPostLoad.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;

    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System.Threading.Tasks;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class StorePostLoad : IStorePostLoad
    {
        ///// <summary>
        ///// Organises the citation repository.
        ///// </summary>
        //private static async Task<HLinkHomeImageModel> OrganiseCitationImage(CitationModel argCitationModel)
        //{
        //    // -- Organsie Default FirstLinks ------------------------------

        // //// Sort media collection and get first link images //DataStore.DS.CitationData[argCitationModel.HLinkKey].GMediaRefCollection.SortAndSetFirst();

        // //// Sort note collection and get first link images //DataStore.DS.CitationData[argCitationModel.HLinkKey].GNoteRefCollection.SortAndSetFirst();

        // // -- Organise Home Images -----------------------

        // // Try media reference collection first HLinkHomeImageModel hlink = argCitationModel.GMediaRefCollection.FirstHLinkHomeImage;

        // // Check Source for Image if (!hlink.Valid) { if
        // (argCitationModel.GSourceRef.DeRef.HomeImageHLink.HomeUseImage) { hlink =
        // argCitationModel.GSourceRef.DeRef.HomeImageHLink; } }

        // // Handle the link if we can if (!hlink.Valid) {
        // argCitationModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
        // argCitationModel.HomeImageHLink.HomeSymbol = CommonConstants.IconCitation; } else {
        // argCitationModel.HomeImageHLink = hlink; }

        //    return argCitationModel.HomeImageHLink;
        //}

        /// <summary>
        /// Organises the event repository.
        /// </summary>
        private static async Task<HLinkHomeImageModel> OrganiseEventImage(EventModel argEventModel)
        {
            // Setup home images

            // Try media reference collection first
            HLinkHomeImageModel hlink = argEventModel.GMediaRefCollection.FirstHLinkHomeImage;

            // Check Media for Images
            if (!hlink.Valid)
            {
                hlink = argEventModel.GMediaRefCollection.FirstHLinkHomeImage;
            }

            // Check Citation for Images
            if (!hlink.Valid)
            {
                hlink = argEventModel.GCitationRefCollection.FirstHLinkHomeImage;

                //hlink = DV.CitationDV.GetFirstImageFromCollection(argModel.GCitationRefCollection);
            }

            // Handle the link if we can
            if (!hlink.Valid)
            {
                argEventModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
            }
            else
            {
                argEventModel.HomeImageHLink = hlink;
            }

            return argEventModel.HomeImageHLink;
        }

        /// <summary>
        /// Organises the family repository.
        /// </summary>
        private static async Task<HLinkHomeImageModel> OrganiseFamilyImage(FamilyModel argFamilyModel)
        {
            // -- Organse First and Sorts --------------------------

            //DataStore.DS.FamilyData[argFamilyModel.HLinkKey].GCitationRefCollection.SortAndSetFirst();

            //DataStore.DS.FamilyData[argFamilyModel.HLinkKey].GEventRefCollection.SortAndSetFirst();

            //DataStore.DS.FamilyData[argFamilyModel.HLinkKey].GMediaRefCollection.SortAndSetFirst();

            //DataStore.DS.FamilyData[argFamilyModel.HLinkKey].GNoteRefCollection.SortAndSetFirst();

            // -- Organse Home Image ---------------------

            // Try media reference collection first
            HLinkHomeImageModel hlink = argFamilyModel.GMediaRefCollection.FirstHLinkHomeImage;

            if (!hlink.Valid)
            {
                hlink = argFamilyModel.GCitationRefCollection.FirstHLinkHomeImage;
            }

            if (!hlink.Valid)
            {
                hlink = argFamilyModel.GEventRefCollection.FirstHLinkHomeImage;
            }

            if (!hlink.Valid)
            {
                hlink = argFamilyModel.GNoteRefCollection.FirstHLinkHomeImage;
            }

            // Set the image if available
            if (hlink.Valid)
            {
                argFamilyModel.HomeImageHLink = hlink;
            }
            else
            {
                // Set to default
                argFamilyModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                argFamilyModel.HomeImageHLink.HomeSymbol = CommonConstants.IconFamilies;
            }

            return argFamilyModel.HomeImageHLink;
        }

        /// <summary>
        /// Organises the media repository.
        /// </summary>
        private static async Task<HLinkHomeImageModel> OrganiseMediaImage(MediaModel argMediaModel)
        {
            // TODO Change to SortAndSetFirst

            // Setup HomeImage
            argMediaModel.HomeImageHLink.HLinkKey = argMediaModel.HLink.HLinkKey;

            switch (argMediaModel.FileMimeType)
            {
                case "application":
                    {
                        switch (argMediaModel.FileMimeSubType)
                        {
                            case "pdf":
                                {
                                    argMediaModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                                    argMediaModel.HomeImageHLink.HomeSymbol = IconFont.FilePdf;
                                    break;
                                }

                            case "x-zip-compressed":
                                {
                                    argMediaModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                                    argMediaModel.HomeImageHLink.HomeSymbol = IconFont.ZipBox;
                                    break;
                                }

                            case "zip":
                                {
                                    argMediaModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                                    argMediaModel.HomeImageHLink.HomeSymbol = IconFont.ZipBox;
                                    break;
                                }

                            default:
                                {
                                    argMediaModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                                    argMediaModel.HomeImageHLink.HomeSymbol = IconFont.FileDocument;
                                    break;
                                }
                        }

                        break;
                    }

                case "image":
                    {
                        argMediaModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeThumbNail;
                        argMediaModel.HomeImageHLink.HomeSymbol = IconFont.Image;
                        break;
                    }

                case "video":
                    {
                        argMediaModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                        argMediaModel.HomeImageHLink.HomeSymbol = IconFont.Video;
                        break;
                    }

                default:
                    {
                        argMediaModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeThumbNail;
                        argMediaModel.HomeImageHLink.HomeSymbol = CommonConstants.IconMedia;
                        break;
                    }
            }

            return argMediaModel.HomeImageHLink;
        }

        /// <summary>
        /// Organises the namemap repository.
        /// </summary>
        private static async Task<bool> OrganiseNameMapImage()
        {
            //foreach (NameMapModel nnameMapObject in DV.NameMapDV.NameMapData)
            //{
            //}

            return true;
        }

        /// <summary>
        /// Organises the note repository.
        /// </summary>
        private static async Task<bool> OrganiseNoteImage()
        {
            return true;
        }

        /// <summary>
        /// Organises the person repository.
        /// </summary>
        private static async Task<HLinkHomeImageModel> OrganisePersonImage(PersonModel arrgPersonModel)
        {
            //// -- Organise First Image and Sorts ------------------------------
            //DataStore.DS.PersonData[arrgPersonModel.HLinkKey].GCitationRefCollection.SortAndSetFirst();

            //DataStore.DS.PersonData[arrgPersonModel.HLinkKey].GEventRefCollection.SortAndSetFirst();

            //DataStore.DS.PersonData[arrgPersonModel.HLinkKey].GMediaRefCollection.SortAndSetFirst();

            //DataStore.DS.PersonData[arrgPersonModel.HLinkKey].GNoteRefCollection.SortAndSetFirst();

            //DataStore.DS.PersonData[arrgPersonModel.HLinkKey].GParentInRefCollection.SortAndSetFirst();

            //DataStore.DS.PersonData[arrgPersonModel.HLinkKey].SiblingRefCollection.SortAndSetFirst();

            // -- Organise Home Image ------------------------------

            //if (argModel.Id == "I0568")
            //{
            //}

            // Get default image if available
            HLinkHomeImageModel hlink = DV.PersonDV.GetDefaultImageFromCollection(arrgPersonModel);

            // Check Media for Images
            if (!hlink.Valid)
            {
                hlink = arrgPersonModel.GMediaRefCollection.FirstHLinkHomeImage;
            }

            // Check Citation for Images
            if (!hlink.Valid)
            {
                hlink = arrgPersonModel.GCitationRefCollection.FirstHLinkHomeImage;
            }

            // Action any Link
            if (!hlink.Valid)
            {
                arrgPersonModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
            }
            else
            {
                arrgPersonModel.HomeImageHLink = hlink;
            }

            return arrgPersonModel.HomeImageHLink;
        }

        /// <summary>
        /// Organises the place repository.
        /// </summary>
        private static async Task<bool> OrganisePlaceImage()
        {
            return true;
        }

        /// <summary>
        /// Organises the repository repository.
        /// </summary>
        private static async Task<bool> OrganiseRepositoryImage()
        {
            return true;
        }

        //private static async Task<HLinkHomeImageModel> OrganiseSourceImage(SourceModel argSourceModel)
        //{
        //    // -- Organse First and Sorts ---------------------

        // //// Sort media collection and get first link images //DataStore.DS.SourceData[argSourceModel.HLinkKey].GMediaRefCollection.SortAndSetFirst();

        // // TODO First and Sort for Notes, Repositories and Tags

        // // Get default image if available HLinkHomeImageModel hlink = argSourceModel.GMediaRefCollection.FirstHLinkHomeImage;

        // // Action default media image if (!hlink.Valid) { // Check for icon hlink =
        // DV.MediaDV.GetFirstImageFromCollection(argSourceModel.GMediaRefCollection); }

        // // Set default if (!hlink.Valid) { argSourceModel.HomeImageHLink.HomeImageType =
        // CommonConstants.HomeImageTypeSymbol; } else { argSourceModel.HomeImageHLink = hlink; }

        //    return argSourceModel.HomeImageHLink;
        //}

        /// <summary>
        /// Organises the tag images.
        /// </summary>
        private static async Task<HLinkHomeImageModel> OrganiseTagImage(TagModel argTagModel)
        {
            if (argTagModel is null)
            {
                throw new System.ArgumentNullException(nameof(argTagModel));
            }

            argTagModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
            argTagModel.HomeImageHLink.HomeSymbolColour = argTagModel.GColor;

            return argTagModel.HomeImageHLink;
        }
    }
}