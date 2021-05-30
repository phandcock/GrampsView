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
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Address data").ConfigureAwait(false);

            SetAddressImages();

            foreach (PersonModel thePersonModel in DV.PersonDV.DataViewData)
            {
                thePersonModel.GAddress.SetGlyph();

                // Address Collection
                foreach (HLinkAdressModel addressRef in thePersonModel.GAddress)
                {
                    DataStore.Instance.DS.AddressData[addressRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonModel.HLink));
                }
            }

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

            SetCitationImages();

            foreach (AddressModel theAddressModel in DV.AddressDV.DataViewData)
            {
                theAddressModel.GCitationRefCollection.SetGlyph();

                // Citation Collection
                foreach (HLinkCitationModel citationRef in theAddressModel.GCitationRefCollection)
                {
                    DataStore.Instance.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theAddressModel.HLink));
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
                    DataStore.Instance.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theEventModel.HLink));
                }
            }

            foreach (FamilyModel theFamilyModel in DV.FamilyDV.DataViewData)
            {
                theFamilyModel.GCitationRefCollection.SetGlyph();

                foreach (HLinkCitationModel citationRef in theFamilyModel.GCitationRefCollection)
                {
                    DataStore.Instance.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theFamilyModel.HLink));
                }
            }

            foreach (IMediaModel theMediaObject in DV.MediaDV.DataViewData)
            {
                theMediaObject.GCitationRefCollection.SetGlyph();

                foreach (HLinkCitationModel citationRef in theMediaObject.GCitationRefCollection)
                {
                    DataStore.Instance.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theMediaObject.HLink));
                }
            }

            foreach (PersonNameModel thePersonNameModel in DV.PersonNameDV.DataViewData)
            {
                thePersonNameModel.GCitationRefCollection.SetGlyph();

                foreach (HLinkCitationModel citationRef in thePersonNameModel.GCitationRefCollection)
                {
                    citationRef.HLinkGlyphItem = DV.CitationDV.GetGlyph(citationRef.HLinkKey);

                    DataStore.Instance.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonNameModel.HLink));
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
                    DataStore.Instance.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonModel.HLink));
                }
            }

            foreach (PlaceModel thePlaceModel in DV.PlaceDV.DataViewData)
            {
                thePlaceModel.GCitationRefCollection.SetGlyph();

                foreach (HLinkCitationModel citationRef in thePlaceModel.GCitationRefCollection)
                {
                    DataStore.Instance.DS.CitationData[citationRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePlaceModel.HLink));
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
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Event data").ConfigureAwait(false);

            SetEventImages();

            foreach (FamilyModel theFamilyModel in DV.FamilyDV.DataViewData)
            {
                theFamilyModel.GEventRefCollection.SetGlyph();

                foreach (HLinkEventModel eventRef in theFamilyModel.GEventRefCollection)
                {
                    DataStore.Instance.DS.EventData[eventRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theFamilyModel.HLink));
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
                    DataStore.Instance.DS.EventData[eventRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonModel.HLink));
                }
            }

            return true;
        }

        /// <summary>
        /// Organises the family repository.
        /// </summary>
        private static async Task<bool> OrganiseFamilyRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Family data ").ConfigureAwait(false);

            SetFamilyImages();

            foreach (FamilyModel theFamilyModel in DV.FamilyDV.DataViewData)
            {
                // Refresh the glyphs
                if (theFamilyModel.GFather.Valid)
                {
                    theFamilyModel.GFather = DataStore.Instance.DS.PersonData[theFamilyModel.GFather.HLinkKey.Value].HLink;
                }
                if (theFamilyModel.GMother.Valid)
                {
                    theFamilyModel.GMother = DataStore.Instance.DS.PersonData[theFamilyModel.GMother.HLinkKey.Value].HLink;
                }
            }

            // The family model already contains a reference to the person

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

            SetNotesImages();

            foreach (ICitationModel theCitationModel in DV.CitationDV.DataViewData)
            {
                theCitationModel.GNoteRefCollection.SetGlyph();

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in theCitationModel.GNoteRefCollection)
                {
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theCitationModel.HLink));
                }
            }

            foreach (EventModel theEventModel in DV.EventDV.DataViewData)
            {
                theEventModel.GNoteRefCollection.SetGlyph();

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in theEventModel.GNoteRefCollection)
                {
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theEventModel.HLink));
                }
            }

            foreach (FamilyModel theFamilyModel in DV.FamilyDV.DataViewData)
            {
                theFamilyModel.GNoteRefCollection.SetGlyph();

                // Note Collection
                foreach (HLinkNoteModel noteRef in theFamilyModel.GNoteRefCollection)
                {
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theFamilyModel.HLink));
                }
            }

            foreach (IMediaModel theMediaObject in DV.MediaDV.DataViewData)
            {
                theMediaObject.GNoteRefCollection.SetGlyph();

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in theMediaObject.GNoteRefCollection)
                {
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theMediaObject.HLink));
                }
            }

            foreach (PersonNameModel thePersonNameModel in DV.PersonNameDV.DataViewData)
            {
                thePersonNameModel.GNoteReferenceCollection.SetGlyph();

                // Note Collection
                foreach (HLinkNoteModel noteRef in thePersonNameModel.GNoteReferenceCollection)
                {
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonNameModel.HLink));
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
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonModel.HLink));
                }
            }

            foreach (PlaceModel thePlaceModel in DV.PlaceDV.DataViewData)
            {
                // Back Reference Note HLinks
                thePlaceModel.GNoteRefCollection.SetGlyph();

                foreach (HLinkNoteModel noteRef in thePlaceModel.GNoteRefCollection)
                {
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePlaceModel.HLink));
                }
            }

            foreach (RepositoryModel theRepositoryModel in DV.RepositoryDV.DataViewData)
            {
                theRepositoryModel.GNoteRefCollection.SetGlyph();

                foreach (HLinkNoteModel noteRef in theRepositoryModel.GNoteRefCollection)
                {
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theRepositoryModel.HLink));
                }
            }

            foreach (SourceModel theSourceModel in DV.SourceDV.DataViewData)
            {
                theSourceModel.GNoteRefCollection.SetGlyph();

                foreach (IHLinkNoteModel noteRef in theSourceModel.GNoteRefCollection)
                {
                    DataStore.Instance.DS.NoteData[noteRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theSourceModel.HLink));
                }
            }

            return true;
        }

        private static async Task<bool> OrganisePersonNameRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Person Name data").ConfigureAwait(false);

            SetPersonNameImages();

            foreach (PersonModel thePersonModel in DV.PersonDV.DataViewData)
            {
                // PersonName Collection
                foreach (HLinkPersonNameModel personNameRef in thePersonModel.GPersonNamesCollection)
                {
                    DataStore.Instance.DS.PersonNameData[personNameRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonModel.HLink));
                }
            }

            return true;
        }

        /// <summary>
        /// Organises the person repository.
        /// </summary>
        private static async Task<bool> OrganisePersonRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Person data").ConfigureAwait(false);

            foreach (PersonModel thePersonModel in DV.PersonDV.DataViewData)
            {
                if (thePersonModel.Id == "I0693")
                {
                }

                SetPersonImages();

                // Set addresses
                thePersonModel.GAddress.SetGlyph();

                foreach (FamilyModel theFamilyModel in DV.FamilyDV.DataViewData)
                {
                    if (theFamilyModel.Id == "F0151")
                    {
                    }

                    theFamilyModel.GChildRefCollection.SetGlyph();

                    // Child Collection
                    foreach (HLinkChildRefModel childRef in theFamilyModel.GChildRefCollection)
                    {
                        DataStore.Instance.DS.PersonData[childRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theFamilyModel.HLink));
                    }

                    // Parents
                    theFamilyModel.GFather.HLinkGlyphItem = theFamilyModel.GFather.DeRef.ModelItemGlyph;
                    theFamilyModel.GMother.HLinkGlyphItem = theFamilyModel.GMother.DeRef.ModelItemGlyph;
                }

                // Parent In Collection
                thePersonModel.GParentInRefCollection.SetGlyph();

                // Sibling Collection
                thePersonModel.SiblingRefCollection.SetGlyph();

                foreach (HLinkPersonModel personRef in thePersonModel.SiblingRefCollection)
                {
                    DataStore.Instance.DS.PersonData[personRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonModel.HLink));
                }

                // --
                if (thePersonModel.GChildOf.Valid)
                {
                    thePersonModel.GChildOf = DataStore.Instance.DS.FamilyData[thePersonModel.GChildOf.HLinkKey.Value].HLink;
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
                        thePersonModel.SiblingRefCollection.Add(item.DeRef.HLink);
                    }
                }

                DataStore.Instance.DS.PersonData[thePersonModel.HLinkKey.Value] = thePersonModel;
            }
            return true;
        }

        /// <summary>
        /// Organises the place repository.
        /// </summary>
        private static async Task<bool> OrganisePlaceRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Place data").ConfigureAwait(false);

            SetPlaceImages();

            foreach (EventModel theEventModel in DV.EventDV.DataViewData)
            {
                if (theEventModel.GPlace.Valid)
                {
                    DataStore.Instance.DS.PlaceData[theEventModel.GPlace.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theEventModel.HLink));
                }
            }

            foreach (PlaceModel thePlaceModel in DV.PlaceDV.DataViewData)
            {
                thePlaceModel.GPlaceRefCollection.SetGlyph();

                foreach (HLinkPlaceModel placeRef in thePlaceModel.GPlaceRefCollection)
                {
                    DataStore.Instance.DS.PlaceData[placeRef.HLinkKey.Value].PlaceChildCollection.Add(thePlaceModel.HLink);
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
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Repository data").ConfigureAwait(false);

            SetRepositoryImages();

            foreach (SourceModel theSourceModel in DV.SourceDV.DataViewData)
            {
                if (theSourceModel.Id == "S0011")
                {
                }

                theSourceModel.GRepositoryRefCollection.SetGlyph();

                foreach (HLinkRepositoryModel repositoryRef in theSourceModel.GRepositoryRefCollection)
                {
                    DataStore.Instance.DS.RepositoryData[repositoryRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theSourceModel.HLink));
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
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Source data").ConfigureAwait(false);

            SetSourceImages();

            foreach (ICitationModel theCitationModel in DV.CitationDV.DataViewData)
            {
                theCitationModel.GSourceRef.HLinkGlyphItem = DV.SourceDV.GetGlyph(theCitationModel.GSourceRef.HLinkKey);

                DataStore.Instance.DS.SourceData[theCitationModel.GSourceRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theCitationModel.HLink));
            }

            return true;
        }

        /// <summary>
        /// Organises the tag repository.
        /// </summary>
        private static async Task<bool> OrganiseTagRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Tag data").ConfigureAwait(false);

            SetTagImages();

            foreach (ICitationModel theCitationModel in DV.CitationDV.DataViewData)
            {
                theCitationModel.GTagRef.SetGlyph();

                foreach (HLinkTagModel tagRef in theCitationModel.GTagRef)
                {
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theCitationModel.HLink));
                }
            }

            foreach (EventModel theEventModel in DV.EventDV.DataViewData)
            {
                theEventModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in theEventModel.GTagRefCollection)
                {
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theEventModel.HLink));
                }
            }

            foreach (FamilyModel theFamilyModel in DV.FamilyDV.DataViewData)
            {
                theFamilyModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in theFamilyModel.GTagRefCollection)
                {
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theFamilyModel.HLink));
                }
            }

            foreach (IMediaModel theMediaObject in DV.MediaDV.DataViewData)
            {
                theMediaObject.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in theMediaObject.GTagRefCollection)
                {
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theMediaObject.HLink));
                }
            }

            foreach (NoteModel theNoteModel in DV.NoteDV.DataViewData)
            {
                theNoteModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in theNoteModel.GTagRefCollection)
                {
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theNoteModel.HLink));
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
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonModel.HLink));
                }
            }

            foreach (PlaceModel thePlaceModel in DV.PlaceDV.DataViewData)
            {
                thePlaceModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in thePlaceModel.GTagRefCollection)
                {
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePlaceModel.HLink));
                }
            }

            foreach (SourceModel theSourceModel in DV.SourceDV.DataViewData)
            {
                theSourceModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in theSourceModel.GTagRefCollection)
                {
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theSourceModel.HLink));
                }
            }

            foreach (RepositoryModel theRepositoryModel in DV.RepositoryDV.DataViewData)
            {
                // Back Reference Tag HLinks
                theRepositoryModel.GTagRefCollection.SetGlyph();

                foreach (HLinkTagModel tagRef in theRepositoryModel.GTagRefCollection)
                {
                    DataStore.Instance.DS.TagData[tagRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theRepositoryModel.HLink));
                }
            }

            return true;
        }

        /// <summary>
        /// Organises the media repository.
        /// </summary>
        private async Task<bool> OrganiseMediaRepository()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Organising Media data").ConfigureAwait(false);

            await SetMediaImages();

            try
            {
                foreach (ICitationModel theCitationModel in DV.CitationDV.DataViewData)
                {
                    if (theCitationModel.Id == "C0776")
                    {
                    }

                    theCitationModel.GMediaRefCollection.SetGlyph();

                    // Media Collection - Create backlinks in media models to citation models
                    foreach (HLinkMediaModel mediaRef in theCitationModel.GMediaRefCollection)
                    {
                        //mediaRef.HLinkGlyphItem = DV.MediaDV.GetGlyph(mediaRef.HLinkKey);

                        DataStore.Instance.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theCitationModel.HLink));
                    }
                }

                foreach (EventModel theEventModel in DV.EventDV.DataViewData)
                {
                    theEventModel.GMediaRefCollection.SetGlyph();

                    // Media Collection
                    foreach (HLinkMediaModel mediaRef in theEventModel.GMediaRefCollection)
                    {
                        //mediaRef.HLinkGlyphItem = DV.MediaDV.GetGlyph(mediaRef.HLinkKey);

                        DataStore.Instance.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theEventModel.HLink));
                    }
                }

                foreach (FamilyModel theFamilyModel in DV.FamilyDV.DataViewData)
                {
                    theFamilyModel.GMediaRefCollection.SetGlyph();

                    // Media Collection
                    foreach (HLinkMediaModel mediaRef in theFamilyModel.GMediaRefCollection)
                    {
                        //mediaRef.HLinkGlyphItem = DV.MediaDV.GetGlyph(mediaRef.HLinkKey);

                        DataStore.Instance.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theFamilyModel.HLink));
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

                        DataStore.Instance.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePersonModel.HLink));
                    }
                }

                foreach (PlaceModel thePlaceModel in DV.PlaceDV.DataViewData)
                {
                    thePlaceModel.GMediaRefCollection.SetGlyph();

                    foreach (HLinkMediaModel mediaRef in thePlaceModel.GMediaRefCollection)
                    {
                        DataStore.Instance.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(thePlaceModel.HLink));
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
                        DataStore.Instance.DS.MediaData[mediaRef.HLinkKey.Value].BackHLinkReferenceCollection.Add(new HLinkBackLink(theSourceModel.HLink));
                    }
                }
            }
            catch (Exception ex)
            {
                DataStore.Instance.CN.NotifyException("Exception in OrganiseMediaRepository", ex);

                throw;
            }

            return true;
        }
    }
}