﻿namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class StorePostLoad : ObservableObject, IStorePostLoad
    {
        /// <summary>
        /// Organises the address repository.
        /// </summary>
        private static async Task<bool> OrganiseAddressRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Address data").ConfigureAwait(false);

            foreach (AddressModel argModel in DV.AddressDV.DataViewData)
            {
                argModel.GCitationRefCollection.SetGlyph();

                // Citation Collection
                foreach (HLinkCitationModel citationRef in argModel.GCitationRefCollection)
                {
                    DataStore.Instance.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }
            }

            SetAddressImages();

            return true;
        }

        private static async Task<bool> OrganiseBookMarkRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising BookMark data").ConfigureAwait(false);

            DV.BookMarkCollection.SetGlyph();

            return true;
        }

        /// <summary>
        /// Organises the Citation Repository.
        /// </summary>
        private static async Task<bool> OrganiseCitationRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Citation data").ConfigureAwait(false);

            foreach (CitationModel argModel in DV.CitationDV.DataViewData)
            {
                argModel.GNoteRefCollection.SetGlyph();

                // Media Collection
                argModel.GMediaRefCollection.SetGlyph();

                // Media Collection - Create backlinks in media models to citation models
                foreach (HLinkMediaModel mediaRef in argModel.GMediaRefCollection)
                {
                    //mediaRef.HLinkGlyphItem = DV.MediaDV.GetGlyph(mediaRef.HLinkKey);

                    DataStore.Instance.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Note Collection
                foreach (HLinkNoteModel noteRef in argModel.GNoteRefCollection)
                {
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Source Link
                argModel.GSourceRef.HLinkGlyphItem = DV.SourceDV.GetGlyph(argModel.GSourceRef.HLinkKey);

                DataStore.Instance.DS.SourceData[argModel.GSourceRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));

                // Tag Collection
                argModel.GTagRef.SetGlyph();

                foreach (HLinkTagModel tagRef in argModel.GTagRef)
                {
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }
            }

            // TODO finish adding the collections to the backlinks
            SetCitationImages();

            return true;
        }

        /// <summary>
        /// Organises the event repository.
        /// </summary>
        private static async Task<bool> OrganiseEventRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Event data").ConfigureAwait(false);

            foreach (EventModel argModel in DV.EventDV.DataViewData)
            {
                argModel.GCitationRefCollection.SetGlyph();

                // Citation Collection
                foreach (HLinkCitationModel citationRef in argModel.GCitationRefCollection)
                {
                    DataStore.Instance.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Event Collection
                if (argModel.GPlace.Valid)
                {
                    DataStore.Instance.DS.PlaceData[argModel.GPlace.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Media Collection
                argModel.GMediaRefCollection.SetGlyph();

                // Media Collection
                foreach (HLinkMediaModel mediaRef in argModel.GMediaRefCollection)
                {
                    //mediaRef.HLinkGlyphItem = DV.MediaDV.GetGlyph(mediaRef.HLinkKey);

                    DataStore.Instance.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // NoteModel Collection
                argModel.GNoteRefCollection.SetGlyph();

                foreach (HLinkNoteModel noteRef in argModel.GNoteRefCollection)
                {
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Tag Collection
                argModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in argModel.GTagRefCollection)
                {
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }
            }

            SetEventImages();

            return true;
        }

        /// <summary>
        /// Organises the family repository.
        /// </summary>
        private static async Task<bool> OrganiseFamilyRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Family data ").ConfigureAwait(false);

            foreach (FamilyModel argModel in DV.FamilyDV.DataViewData)
            {
                // Child Collection
                argModel.GChildRefCollection.SetGlyph();

                foreach (HLinkChildRefModel childRef in argModel.GChildRefCollection)
                {
                    DataStore.Instance.DS.PersonData[childRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Citation Collection
                argModel.GCitationRefCollection.SetGlyph();

                foreach (HLinkCitationModel citationRef in argModel.GCitationRefCollection)
                {
                    DataStore.Instance.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Parents
                argModel.GFather.HLinkGlyphItem = argModel.GFather.DeRef.ModelItemGlyph;
                argModel.GMother.HLinkGlyphItem = argModel.GMother.DeRef.ModelItemGlyph;

                // Refresh the glyphs
                if (argModel.GFather.Valid)
                {
                    argModel.GFather = DataStore.Instance.DS.PersonData[argModel.GFather.HLinkKey.Value].HLink;
                }
                if (argModel.GMother.Valid)
                {
                    argModel.GMother = DataStore.Instance.DS.PersonData[argModel.GMother.HLinkKey.Value].HLink;
                }

                // EventModel Collection
                argModel.GEventRefCollection.SetGlyph();

                foreach (HLinkEventModel eventRef in argModel.GEventRefCollection)
                {
                    DataStore.Instance.DS.EventData[eventRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Media Collection
                argModel.GMediaRefCollection.SetGlyph();

                // Media Collection
                foreach (HLinkMediaModel mediaRef in argModel.GMediaRefCollection)
                {
                    //mediaRef.HLinkGlyphItem = DV.MediaDV.GetGlyph(mediaRef.HLinkKey);

                    DataStore.Instance.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Note Collection
                argModel.GNoteRefCollection.SetGlyph();

                foreach (HLinkNoteModel noteRef in argModel.GNoteRefCollection)
                {
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Tag Collection
                argModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in argModel.GTagRefCollection)
                {
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }
            }

            SetFamilyImages();

            return true;
        }

        /// <summary>
        /// Organises the header repository.
        /// </summary>
        private static async Task<bool> OrganiseHeaderRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Header data").ConfigureAwait(false);

            SetHeaderImages();

            return true;
        }

        /// <summary>
        /// Organises misc items pending use of a dependency graph.
        /// </summary>
        private static async Task<bool> OrganiseMisc()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Misc data").ConfigureAwait(false);

            // Family children
            foreach (FamilyModel argModel in DV.FamilyDV.DataViewData)
            {
                // Children Collection
                argModel.GChildRefCollection.SetGlyph();
            }

            SetAddressImages();

            return true;
        }

        /// <summary>
        /// Organises the namemap repository.
        /// </summary>
        private static async Task<bool> OrganiseNameMapRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising NameMap data").ConfigureAwait(false);

            SetNameMapImages();

            return true;
        }

        /// <summary>
        /// Organises the note repository.
        /// </summary>
        private static async Task<bool> OrganiseNoteRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Note data").ConfigureAwait(false);

            foreach (NoteModel argModel in DV.NoteDV.DataViewData)
            {
                // Note Collection
                argModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in argModel.GTagRefCollection)
                {
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }
            }

            SetNotesImages();

            return true;
        }

        private static async Task<bool> OrganisePersonNameRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Person Name data").ConfigureAwait(false);

            foreach (PersonNameModel argModel in DV.PersonNameDV.DataViewData)
            {
                argModel.GNoteReferenceCollection.SetGlyph();

                // Citation Collection
                argModel.GCitationRefCollection.SetGlyph();

                foreach (HLinkCitationModel citationRef in argModel.GCitationRefCollection)
                {
                    citationRef.HLinkGlyphItem = DV.CitationDV.GetGlyph(citationRef.HLinkKey);

                    DataStore.Instance.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Note Collection
                foreach (HLinkNoteModel noteRef in argModel.GNoteReferenceCollection)
                {
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }
            }

            SetPersonNameImages();

            return true;
        }

        /// <summary>
        /// Organises the person repository.
        /// </summary>
        private static async Task<bool> OrganisePersonRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Person data").ConfigureAwait(false);

            foreach (PersonModel argModel in DV.PersonDV.DataViewData)
            {
                if (argModel.Id == "I0693")
                {
                }

                // Address Collection
                argModel.GAddressCollection.SetGlyph();

                foreach (HLinkAdressModel addressRef in argModel.GAddressCollection)
                {
                    DataStore.Instance.DS.AddressData[addressRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Citation Collection
                argModel.GCitationRefCollection.SetGlyph();

                foreach (HLinkCitationModel citationRef in argModel.GCitationRefCollection)
                {
                    DataStore.Instance.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Event Collection
                argModel.GEventRefCollection.SetGlyph();

                foreach (HLinkEventModel eventRef in argModel.GEventRefCollection)
                {
                    DataStore.Instance.DS.EventData[eventRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Family Collection

                // Media Collection
                argModel.GMediaRefCollection.SetGlyph();

                foreach (HLinkMediaModel mediaRef in argModel.GMediaRefCollection)
                {
                    DataStore.Instance.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Note Collection
                argModel.GNoteRefCollection.SetGlyph();

                foreach (HLinkNoteModel noteRef in argModel.GNoteRefCollection)
                {
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Parent In Collection
                argModel.GParentInRefCollection.SetGlyph();

                // PersonName Collection
                argModel.GPersonNamesCollection.SetGlyph();

                // PersonName Collection
                foreach (HLinkPersonNameModel personNameRef in argModel.GPersonNamesCollection)
                {
                    DataStore.Instance.DS.PersonNameData[personNameRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // --
                if (argModel.GChildOf.Valid)
                {
                    argModel.GChildOf = DataStore.Instance.DS.FamilyData[argModel.GChildOf.HLinkKey.Value].HLink;
                }

                // set Birthdate
                EventModel birthDate = DV.EventDV.GetEventType(argModel.GEventRefCollection, CommonConstants.EventTypeBirth);
                if (birthDate.Valid)
                {
                    argModel.BirthDate = birthDate.GDate;
                }

                // set Is Living
                if (DV.EventDV.GetEventType(argModel.GEventRefCollection, CommonConstants.EventTypeDeath).Valid)
                {
                    argModel.IsLiving = false;
                }
                else
                {
                    argModel.IsLiving = true;
                }

                // Tag Collection
                argModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in argModel.GTagRefCollection)
                {
                    // Set the backlinks
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // DataStore.Instance.DS.PersonData[argModel.HLinkKey.Value] = argModel;
            }

            SetPersonImages();

            return true;
        }

        /// <summary>
        /// Organises the place repository.
        /// </summary>
        private static async Task<bool> OrganisePlaceRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Place data").ConfigureAwait(false);

            foreach (PlaceModel argModel in DV.PlaceDV.DataViewData)
            {
                argModel.GPlaceParentCollection.SetGlyph();

                foreach (HLinkPlaceModel placeRef in argModel.GPlaceParentCollection)
                {
                    DataStore.Instance.DS.PlaceData[placeRef.HLinkKey.Value].PlaceChildCollection.Add(argModel.HLink);
                }

                // Citation Collection
                argModel.GCitationRefCollection.SetGlyph();

                foreach (HLinkCitationModel citationRef in argModel.GCitationRefCollection)
                {
                    DataStore.Instance.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Media Collection
                argModel.GMediaRefCollection.SetGlyph();

                foreach (HLinkMediaModel mediaRef in argModel.GMediaRefCollection)
                {
                    DataStore.Instance.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Note Collection
                argModel.GNoteRefCollection.SetGlyph();

                foreach (HLinkNoteModel noteRef in argModel.GNoteRefCollection)
                {
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Tag Collection
                argModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in argModel.GTagRefCollection)
                {
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }
            }

            // Now that all of the Enclosed Places have been added

            foreach (PlaceModel thePlaceModel in DV.PlaceDV.DataViewData)
            {
                thePlaceModel.PlaceChildCollection.SetGlyph();
                //{
                //    thePlaceModel.PlaceChildCollection.SetFirstImage();
                //    thePlaceModel.PlaceChildCollection.Sort();
                //}
            }

            SetPlaceImages();

            return true;
        }

        /// <summary>
        /// Organises the repository repository.
        /// </summary>
        private static async Task<bool> OrganiseRepositoryRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Repository data").ConfigureAwait(false);

            foreach (RepositoryModel argModel in DV.RepositoryDV.DataViewData)
            {
                // Note Collection
                argModel.GNoteRefCollection.SetGlyph();

                foreach (HLinkNoteModel noteRef in argModel.GNoteRefCollection)
                {
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Tag Collection
                argModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in argModel.GTagRefCollection)
                {
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }
            }

            SetRepositoryImages();

            return true;
        }

        /// <summary>
        /// Organises the source repository backlinks.
        /// - XML 1.71 Completed
        /// </summary>
        /// <returns>
        /// true if the organisation worked.
        /// </returns>
        private static async Task<bool> OrganiseSourceRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Source data").ConfigureAwait(false);

            foreach (SourceModel argModel in DV.SourceDV.DataViewData)
            {
                // Media Collection
                argModel.GMediaRefCollection.SetGlyph();

                foreach (HLinkMediaModel mediaRef in argModel.GMediaRefCollection)
                {
                    DataStore.Instance.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Note Collection
                argModel.GNoteRefCollection.SetGlyph();

                foreach (IHLinkNoteModel noteRef in argModel.GNoteRefCollection)
                {
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Repository Collection
                argModel.GRepositoryRefCollection.SetGlyph();

                foreach (HLinkRepositoryModel repositoryRef in argModel.GRepositoryRefCollection)
                {
                    DataStore.Instance.DS.RepositoryData[repositoryRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }

                // Tag Collection
                argModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in argModel.GTagRefCollection)
                {
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                }
            }

            SetSourceImages();

            return true;
        }

        /// <summary>
        /// Organises the tag repository.
        /// </summary>
        private static async Task<bool> OrganiseTagRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Tag data").ConfigureAwait(false);

            SetTagImages();

            return true;
        }

        /// <summary>
        /// Organises the media repository.
        /// </summary>
        private async Task<bool> OrganiseMediaRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Media data").ConfigureAwait(false);

            try
            {
                foreach (MediaModel argModel in DV.MediaDV.DataViewData)
                {
                    // Citation Collection
                    argModel.GCitationRefCollection.SetGlyph();

                    foreach (HLinkCitationModel citationRef in argModel.GCitationRefCollection)
                    {
                        DataStore.Instance.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                    }

                    // Note Collection
                    argModel.GNoteRefCollection.SetGlyph();

                    // Back Reference Note HLinks
                    foreach (HLinkNoteModel noteRef in argModel.GNoteRefCollection)
                    {
                        DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                    }

                    // Tag Collection
                    argModel.GTagRefCollection.SetGlyph();

                    foreach (HLinkTagModel tagRef in argModel.GTagRefCollection)
                    {
                        DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(argModel.HLink));
                    }
                }
            }
            catch (Exception ex)
            {
                DataStore.Instance.CN.NotifyException("Exception in OrganiseMediaRepository", ex);

                throw;
            }

            await SetMediaImages();

            return true;
        }
    }
}