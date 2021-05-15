namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class StorePostLoad : CommonBindableBase, IStorePostLoad
    {
        /// <summary>
        /// Organises the address repository.
        /// </summary>
        private static async Task<bool> OrganiseAddressRepository()
        {
            await DataStore.CN.DataLogEntryAdd("Organising Address data").ConfigureAwait(false);

            SetAddressImages();

            return true;
        }

        private static async Task<bool> OrganiseBookMarkRepository()
        {
            await DataStore.CN.DataLogEntryAdd("Organising BookMark data").ConfigureAwait(false);

            DV.BookMarkCollection.SetGlyph();

            return true;
        }

        /// <summary>
        /// Organises the Citation Repository.
        /// </summary>
        private static async Task<bool> OrganiseCitationRepository()
        {
            await DataStore.CN.DataLogEntryAdd("Organising Citation data").ConfigureAwait(false);

            SetCitationImages();

            foreach (AddressModel theAddressModel in DV.AddressDV.DataViewData)
            {
                theAddressModel.GCitationRefCollection.SetGlyph();

                // Citation Collection
                foreach (HLinkCitationModel citationRef in theAddressModel.GCitationRefCollection)
                {
                    DataStore.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theAddressModel.HLink));
                }
            }

            foreach (EventModel theEventModel in DV.EventDV.DataViewData)
            {
                //if (theEventModel.Id == "E0203")
                //{
                //}

                theEventModel.GCitationRefCollection.SetGlyph();

                // tagref Citation Collection
                foreach (HLinkCitationModel citationRef in theEventModel.GCitationRefCollection)
                {
                    DataStore.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theEventModel.HLink));
                }
            }

            foreach (FamilyModel theFamilyModel in DV.FamilyDV.DataViewData)
            {
                theFamilyModel.GCitationRefCollection.SetGlyph();

                foreach (HLinkCitationModel citationRef in theFamilyModel.GCitationRefCollection)
                {
                    DataStore.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theFamilyModel.HLink));
                }
            }

            foreach (IMediaModel theMediaObject in DV.MediaDV.DataViewData)
            {
                theMediaObject.GCitationRefCollection.SetGlyph();

                foreach (HLinkCitationModel citationRef in theMediaObject.GCitationRefCollection)
                {
                    DataStore.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theMediaObject.HLink));
                }
            }

            foreach (PersonNameModel thePersonNameModel in DV.PersonNameDV.DataViewData)
            {
                thePersonNameModel.GCitationRefCollection.SetGlyph();

                foreach (HLinkCitationModel citationRef in thePersonNameModel.GCitationRefCollection)
                {
                    citationRef.HLinkGlyphItem = DV.CitationDV.GetGlyph(citationRef.HLinkKey);

                    DataStore.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonNameModel.HLink));
                }
            }

            foreach (PersonModel thePersonModel in DV.PersonDV.DataViewData)
            {
                //if (thePersonModel.Id == "I0600")
                //{
                //}

                thePersonModel.GCitationRefCollection.SetGlyph();

                foreach (HLinkCitationModel citationRef in thePersonModel.GCitationRefCollection)
                {
                    DataStore.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonModel.HLink));
                }
            }

            foreach (PlaceModel thePlaceModel in DV.PlaceDV.DataViewData)
            {
                thePlaceModel.GCitationRefCollection.SetGlyph();

                foreach (HLinkCitationModel citationRef in thePlaceModel.GCitationRefCollection)
                {
                    DataStore.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePlaceModel.HLink));
                }
            }

            // TODO finish adding the collections to the backlinks

            return true;
        }

        /// <summary>
        /// Organises the event repository.
        /// </summary>
        private static async Task<bool> OrganiseEventRepository()
        {
            await DataStore.CN.DataLogEntryAdd("Organising Event data").ConfigureAwait(false);

            SetEventImages();

            foreach (FamilyModel theFamilyModel in DV.FamilyDV.DataViewData)
            {
                theFamilyModel.GEventRefCollection.SetGlyph();

                foreach (HLinkEventModel eventRef in theFamilyModel.GEventRefCollection)
                {
                    DataStore.DS.EventData[eventRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theFamilyModel.HLink));
                }
            }

            foreach (PersonModel thePersonModel in DV.PersonDV.DataViewData)
            {
                //if (thePersonModel.Id == "I0704")
                //{
                //}

                thePersonModel.GEventRefCollection.SetGlyph();

                foreach (HLinkEventModel eventRef in thePersonModel.GEventRefCollection)
                {
                    DataStore.DS.EventData[eventRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonModel.HLink));
                }
            }

            return true;
        }

        /// <summary>
        /// Organises the family repository.
        /// </summary>
        private static async Task<bool> OrganiseFamilyRepository()
        {
            await DataStore.CN.DataLogEntryAdd("Organising Family data ").ConfigureAwait(false);

            SetFamilyImages();

            // Person models already contain a link tot he family model (if they in in one)

            // The family model already contains a reference to the person

            return true;
        }

        /// <summary>
        /// Organises the header repository.
        /// </summary>
        private static async Task<bool> OrganiseHeaderRepository()
        {
            await DataStore.CN.DataLogEntryAdd("Organising Header data").ConfigureAwait(false);

            SetHeaderImages();

            return true;
        }

        /// <summary>
        /// Organises the namemap repository.
        /// </summary>
        private static async Task<bool> OrganiseNameMapRepository()
        {
            await DataStore.CN.DataLogEntryAdd("Organising NameMap data").ConfigureAwait(false);

            SetNameMapImages();

            return true;
        }

        /// <summary>
        /// Organises the note repository.
        /// </summary>
        private static async Task<bool> OrganiseNoteRepository()
        {
            await DataStore.CN.DataLogEntryAdd("Organising Note data").ConfigureAwait(false);

            SetNotesImages();

            foreach (ICitationModel theCitationModel in DV.CitationDV.DataViewData)
            {
                theCitationModel.GNoteRefCollection.SetGlyph();

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in theCitationModel.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theCitationModel.HLink));
                }
            }

            foreach (EventModel theEventModel in DV.EventDV.DataViewData)
            {
                theEventModel.GNoteRefCollection.SetGlyph();

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in theEventModel.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theEventModel.HLink));
                }
            }

            foreach (FamilyModel theFamilyModel in DV.FamilyDV.DataViewData)
            {
                theFamilyModel.GNoteRefCollection.SetGlyph();

                // Note Collection
                foreach (HLinkNoteModel noteRef in theFamilyModel.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theFamilyModel.HLink));
                }
            }

            foreach (IMediaModel theMediaObject in DV.MediaDV.DataViewData)
            {
                theMediaObject.GNoteRefCollection.SetGlyph();

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in theMediaObject.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theMediaObject.HLink));
                }
            }

            foreach (PersonNameModel thePersonNameModel in DV.PersonNameDV.DataViewData)
            {
                thePersonNameModel.GNoteReferenceCollection.SetGlyph();

                // Note Collection
                foreach (HLinkNoteModel noteRef in thePersonNameModel.GNoteReferenceCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonNameModel.HLink));
                }
            }

            foreach (PersonModel thePersonModel in DV.PersonDV.DataViewData)
            {
                //if (thePersonModel.Id == "I0337")
                //{
                //}

                thePersonModel.GNoteRefCollection.SetGlyph();

                foreach (HLinkNoteModel noteRef in thePersonModel.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonModel.HLink));
                }
            }

            foreach (PlaceModel thePlaceModel in DV.PlaceDV.DataViewData)
            {
                // Back Reference Note HLinks
                thePlaceModel.GNoteRefCollection.SetGlyph();

                foreach (HLinkNoteModel noteRef in thePlaceModel.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePlaceModel.HLink));
                }
            }

            foreach (RepositoryModel theRepositoryModel in DV.RepositoryDV.DataViewData)
            {
                theRepositoryModel.GNoteRefCollection.SetGlyph();

                foreach (HLinkNoteModel noteRef in theRepositoryModel.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theRepositoryModel.HLink));
                }
            }

            foreach (SourceModel theSourceModel in DV.SourceDV.DataViewData)
            {
                theSourceModel.GNoteRefCollection.SetGlyph();

                foreach (IHLinkNoteModel noteRef in theSourceModel.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theSourceModel.HLink));
                }
            }

            return true;
        }

        private static async Task<bool> OrganisePersonNameRepository()
        {
            await DataStore.CN.DataLogEntryAdd("Organising Person Name data").ConfigureAwait(false);

            SetPersonNameImages();

            foreach (PersonModel thePersonModel in DV.PersonDV.DataViewData)
            {
                thePersonModel.GPersonNamesCollection.SetGlyph();

                // PersonName Collection
                foreach (HLinkPersonNameModel personNameRef in thePersonModel.GPersonNamesCollection)
                {
                    DataStore.DS.PersonNameData[personNameRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonModel.HLink));
                }
            }

            return true;
        }

        /// <summary>
        /// Organises the person repository.
        /// </summary>
        private static async Task<bool> OrganisePersonRepository()
        {
            await DataStore.CN.DataLogEntryAdd("Organising Person data").ConfigureAwait(false);

            SetPersonImages();

            foreach (FamilyModel theFamilyModel in DV.FamilyDV.DataViewData)
            {
                theFamilyModel.GChildRefCollection.SetGlyph();

                // Child Collection
                foreach (HLinkChildRefModel childRef in theFamilyModel.GChildRefCollection)
                {
                    DataStore.DS.PersonData[childRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theFamilyModel.HLink));
                }

                // Parents
                theFamilyModel.GFather.HLinkGlyphItem = theFamilyModel.GFather.DeRef.ModelItemGlyph;
                theFamilyModel.GMother.HLinkGlyphItem = theFamilyModel.GMother.DeRef.ModelItemGlyph;
            }

            foreach (PersonModel thePersonModel in DV.PersonDV.DataViewData)
            {
                thePersonModel.SiblingRefCollection.SetGlyph();

                // Sibling Collection
                foreach (HLinkPersonModel personRef in thePersonModel.SiblingRefCollection)
                {
                    DataStore.DS.PersonData[personRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonModel.HLink));
                }
            }

            foreach (PersonModel thePersonModel in DV.PersonDV.DataViewData)
            {
                if (thePersonModel.Id == "I0693")
                {
                }

                // -- Setup some extra values ------------------------------

                // --
                if (thePersonModel.GChildOf.Valid)
                {
                    thePersonModel.GChildOf = DataStore.DS.FamilyData[thePersonModel.GChildOf.HLinkKey.Value].HLink;
                }

                // set Birthdate
                EventModel birthDate = DV.EventDV.GetEventType(thePersonModel.GEventRefCollection, CommonConstants.EventTypeBirth);
                if (birthDate.Valid)
                {
                    thePersonModel.BirthDate = birthDate.GDate;
                }

                // set Is Living
                if (DV.EventDV.GetEventType(thePersonModel.GEventRefCollection, CommonConstants.EventTypeDeath).Valid)
                {
                    thePersonModel.IsLiving = false;
                }
                else
                {
                    thePersonModel.IsLiving = true;
                }

                // set Sibling Collection
                if (thePersonModel.GChildOf.Valid)
                {
                    thePersonModel.SiblingRefCollection.Clear();

                    foreach (HLinkChildRefModel item in DV.FamilyDV.FamilyData[thePersonModel.GChildOf.HLinkKey.Value].GChildRefCollection)
                    {
                        thePersonModel.SiblingRefCollection.Add(item.GetHLinkPerson);
                    }
                }

                DataStore.DS.PersonData[thePersonModel.HLinkKey.Value] = thePersonModel;
            }

            return true;
        }

        /// <summary>
        /// Organises the place repository.
        /// </summary>
        private static async Task<bool> OrganisePlaceRepository()
        {
            await DataStore.CN.DataLogEntryAdd("Organising Place data").ConfigureAwait(false);

            SetPlaceImages();

            foreach (EventModel theEventModel in DV.EventDV.DataViewData)
            {
                if (theEventModel.GPlace.Valid)
                {
                    DataStore.DS.PlaceData[theEventModel.GPlace.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theEventModel.HLink));
                }
            }

            foreach (PlaceModel thePlaceModel in DV.PlaceDV.DataViewData)
            {
                thePlaceModel.GPlaceRefCollection.SetGlyph();

                foreach (HLinkPlaceModel placeRef in thePlaceModel.GPlaceRefCollection)
                {
                    DataStore.DS.PlaceData[placeRef.HLinkKey.Value].PlaceChildCollection.Add(thePlaceModel.HLink);
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

            return true;
        }

        /// <summary>
        /// Organises the repository repository.
        /// </summary>
        private static async Task<bool> OrganiseRepositoryRepository()
        {
            await DataStore.CN.DataLogEntryAdd("Organising Repository data").ConfigureAwait(false);

            SetRepositoryImages();

            foreach (SourceModel theSourceModel in DV.SourceDV.DataViewData)
            {
                if (theSourceModel.Id == "S0011")
                {
                }

                theSourceModel.GRepositoryRefCollection.SetGlyph();

                foreach (HLinkRepositoryModel repositoryRef in theSourceModel.GRepositoryRefCollection)
                {
                    DataStore.DS.RepositoryData[repositoryRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theSourceModel.HLink));
                }
            }

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
            await DataStore.CN.DataLogEntryAdd("Organising Source data").ConfigureAwait(false);

            SetSourceImages();

            foreach (ICitationModel theCitationModel in DV.CitationDV.DataViewData)
            {
                theCitationModel.GSourceRef.HLinkGlyphItem = DV.SourceDV.GetGlyph(theCitationModel.GSourceRef.HLinkKey);

                DataStore.DS.SourceData[theCitationModel.GSourceRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theCitationModel.HLink));
            }

            return true;
        }

        /// <summary>
        /// Organises the tag repository.
        /// </summary>
        private static async Task<bool> OrganiseTagRepository()
        {
            await DataStore.CN.DataLogEntryAdd("Organising Tag data").ConfigureAwait(false);

            SetTagImages();

            foreach (ICitationModel theCitationModel in DV.CitationDV.DataViewData)
            {
                theCitationModel.GTagRef.SetGlyph();

                foreach (HLinkTagModel tagRef in theCitationModel.GTagRef)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theCitationModel.HLink));
                }
            }

            foreach (EventModel theEventModel in DV.EventDV.DataViewData)
            {
                theEventModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in theEventModel.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theEventModel.HLink));
                }
            }

            foreach (FamilyModel theFamilyModel in DV.FamilyDV.DataViewData)
            {
                theFamilyModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in theFamilyModel.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theFamilyModel.HLink));
                }
            }

            foreach (IMediaModel theMediaObject in DV.MediaDV.DataViewData)
            {
                theMediaObject.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in theMediaObject.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theMediaObject.HLink));
                }
            }

            foreach (NoteModel theNoteModel in DV.NoteDV.DataViewData)
            {
                theNoteModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in theNoteModel.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theNoteModel.HLink));
                }
            }

            foreach (PersonModel thePersonModel in DV.PersonDV.DataViewData)
            {
                if (thePersonModel.Id == "I0729")
                {
                }

                thePersonModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in thePersonModel.GTagRefCollection)
                {
                    // Set the backlinks
                    DataStore.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonModel.HLink));
                }
            }

            foreach (PlaceModel thePlaceModel in DV.PlaceDV.DataViewData)
            {
                thePlaceModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in thePlaceModel.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePlaceModel.HLink));
                }
            }

            foreach (SourceModel theSourceModel in DV.SourceDV.DataViewData)
            {
                theSourceModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in theSourceModel.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theSourceModel.HLink));
                }
            }

            foreach (RepositoryModel theRepositoryModel in DV.RepositoryDV.DataViewData)
            {
                // Back Reference Tag HLinks
                theRepositoryModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in theRepositoryModel.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theRepositoryModel.HLink));
                }
            }

            return true;
        }

        /// <summary>
        /// Organises the media repository.
        /// </summary>
        private async Task<bool> OrganiseMediaRepository()
        {
            await DataStore.CN.DataLogEntryAdd("Organising Media data").ConfigureAwait(false);

            await SetMediaImages();

            try
            {
                foreach (ICitationModel theCitationModel in DV.CitationDV.DataViewData)
                {
                    theCitationModel.GMediaRefCollection.SetGlyph();

                    // Media Collection - Create backlinks in media models to citation models
                    foreach (HLinkMediaModel mediaRef in theCitationModel.GMediaRefCollection)
                    {
                        //mediaRef.HLinkGlyphItem = DV.MediaDV.GetGlyph(mediaRef.HLinkKey);

                        DataStore.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theCitationModel.HLink));
                    }
                }

                foreach (EventModel theEventModel in DV.EventDV.DataViewData)
                {
                    theEventModel.GMediaRefCollection.SetGlyph();

                    // Media Collection
                    foreach (HLinkMediaModel mediaRef in theEventModel.GMediaRefCollection)
                    {
                        //mediaRef.HLinkGlyphItem = DV.MediaDV.GetGlyph(mediaRef.HLinkKey);

                        DataStore.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theEventModel.HLink));
                    }
                }

                foreach (FamilyModel theFamilyModel in DV.FamilyDV.DataViewData)
                {
                    theFamilyModel.GMediaRefCollection.SetGlyph();

                    // Media Collection
                    foreach (HLinkMediaModel mediaRef in theFamilyModel.GMediaRefCollection)
                    {
                        //mediaRef.HLinkGlyphItem = DV.MediaDV.GetGlyph(mediaRef.HLinkKey);

                        DataStore.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theFamilyModel.HLink));
                    }
                }

                foreach (PersonModel thePersonModel in DV.PersonDV.DataViewData)
                {
                    if (thePersonModel.Id == "I0704")
                    {
                    }

                    thePersonModel.GMediaRefCollection.SetGlyph();

                    foreach (HLinkMediaModel mediaRef in thePersonModel.GMediaRefCollection)
                    {
                        //mediaRef.HLinkGlyphItem = DV.MediaDV.GetGlyph(mediaRef.HLinkKey);

                        DataStore.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonModel.HLink));
                    }
                }

                foreach (PlaceModel thePlaceModel in DV.PlaceDV.DataViewData)
                {
                    thePlaceModel.GMediaRefCollection.SetGlyph();

                    foreach (HLinkMediaModel mediaRef in thePlaceModel.GMediaRefCollection)
                    {
                        DataStore.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePlaceModel.HLink));
                    }
                }

                foreach (SourceModel theSourceModel in DV.SourceDV.DataViewData)
                {
                    if (theSourceModel.Id == "S0381")
                    {
                    }

                    theSourceModel.GMediaRefCollection.SetGlyph();

                    foreach (HLinkMediaModel mediaRef in theSourceModel.GMediaRefCollection)
                    {
                        DataStore.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theSourceModel.HLink));
                    }
                }
            }
            catch (Exception ex)
            {
                DataStore.CN.NotifyException("Exception in OrganiseMediaRepository", ex);

                throw;
            }

            return true;
        }
    }
}