namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;

    public partial class StorePostLoad : CommonBindableBase, IStorePostLoad
    {
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

                ItemGlyph hlink = argModel.ModelItemGlyph;

                if (argModel.Id == "C0496")
                {
                }

                // Try media reference collection first
                ItemGlyph t = argModel.GSourceRef.DeRef.ModelItemGlyph;
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
                    argModel.ModelItemGlyph = hlink;
                }
            }

            return;
        }

        public async static Task SetEventImages()
        {
            foreach (EventModel argModel in DataStore.Instance.DS.EventData.Values)
            {
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
                    argModel.ModelItemGlyph = hlink;
                }
            }
        }

        public async static Task SetFamilyImages()
        {
            foreach (FamilyModel argModel in DataStore.Instance.DS.FamilyData.Values)
            {
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
                    argModel.ModelItemGlyph = hlink;
                }
            }
        }

        public async static Task SetHeaderImages()
        {
        }

        public static void SetMediaImages()
        {
            foreach (MediaModel argModel in DataStore.Instance.DS.MediaData.Values)
            {
                Contract.Requires(argModel != null);

                if (argModel.Id == "O0334")
                {
                }

                // Setup HomeImage
                argModel.ModelItemGlyph.HLinkMediHLink = argModel.HLink.HLinkKey;

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
                                        break;
                                    }

                                case "x-zip-compressed":
                                    {
                                        argModel.ModelItemGlyph.Symbol = CommonFontNamesFAS.FileArchive;
                                        break;
                                    }

                                case "zip":
                                    {
                                        argModel.ModelItemGlyph.Symbol = CommonFontNamesFAS.FileArchive;
                                        break;
                                    }
                            }

                            break;
                        }

                    case "audio":
                        {
                            argModel.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Symbol;
                            argModel.ModelItemGlyph.Symbol = CommonFontNamesFAS.FileAudio;
                            break;
                        }

                    case "image":
                        {
                            argModel.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Image;
                            argModel.ModelItemGlyph.Symbol = CommonFontNamesFAS.FileImage;
                            break;
                        }

                    case "video":
                        {
                            argModel.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Symbol;
                            argModel.ModelItemGlyph.Symbol = CommonFontNamesFAS.FileVideo;
                            break;
                        }
                }
            }
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

        // //await SetPersonNameImages().ConfigureAwait(false); People last as they pretty much
        // depend on everything else
        public async static Task SetPersonImages()
        {
            foreach (PersonModel argModel in DataStore.Instance.DS.PersonData.Values)
            {
                if (argModel.Id == "I0005")
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

                // Action any Link
                if (hlink.Valid)
                {
                    argModel.ModelItemGlyph = hlink;
                }
            }
        }

        // //await SetAddressImages().ConfigureAwait(false);
        public async static Task SetPersonNameImages()
        {
        }

        // //await SetTagImages().ConfigureAwait(false);
        public async static Task SetPlaceImages()
        {
        }

        // //await SetRepositoryImages().ConfigureAwait(false);
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
                    argModel.ModelItemGlyph = hlink;
                }
            }
        }

        //public async Task SetHomeImages()
        //{
        //    await DataStore.Instance.CN.DataLogEntryAdd("Setting Home Images after load").ConfigureAwait(false);
        //    {
        //        await DataStore.Instance.CN.DataLogEntryAdd("This will take a while...").ConfigureAwait(false);
        //        {
        //            // Called in order of media linkages from Media outwards - Media is the start so
        //            // called ahead await SetMediaImages().ConfigureAwait(false);

        // //await SetCitationImages().ConfigureAwait(false);

        // //await SetEventImages().ConfigureAwait(false);

        // //await SetFamilyImages().ConfigureAwait(false);

        // //await SetHeaderImages().ConfigureAwait(false);

        // //await SetNameMapImages().ConfigureAwait(false);

        // //await SetNotesImages().ConfigureAwait(false);

        // //await SetPlaceImages().ConfigureAwait(false);
        public async static Task SetTagImages()
        {
        }
    }
}