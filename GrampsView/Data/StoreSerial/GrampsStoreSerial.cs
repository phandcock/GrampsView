// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Repository;

using SharedSharp.Errors.Interfaces;

namespace GrampsView.Data.StoreSerial
{
    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public class GrampsStoreSerial : ObservableObject, IGrampsStoreSerial
    {
        /// <summary>
        /// The local common logging.
        /// </summary>
        private readonly ILog localGVLogging;

        /// <summary>Initializes a new instance of the <see cref="GrampsStoreSerial" /> class.</summary>
        /// <param name="iocGVLogging">The ioc gv logging.</param>
        public GrampsStoreSerial(ILog iocGVLogging)
        {
            // save injected references for later
            localGVLogging = iocGVLogging;
        }

        /// <summary>
        /// Deserialise the previously serialised repository. Perform as a single step so that it
        /// goes faster at the cost of providing less feedback to the user.
        /// </summary>

        public async Task DeSerializeRepository()
        {
            localGVLogging.RoutineEntry(nameof(DeSerializeRepository));

            try
            {
                await DataStore.Instance.DS.AddressData.DeSerialize();
                await DataStore.Instance.DS.BookMarkCollection.DeSerialize();
                await DataStore.Instance.DS.CitationData.DeSerialize();
                await DataStore.Instance.DS.EventData.DeSerialize();
                await DataStore.Instance.DS.FamilyData.DeSerialize();
                await DataStore.Instance.DS.HeaderData.DeSerialize();
                await DataStore.Instance.DS.MediaData.DeSerialize();
                await DataStore.Instance.DS.NameMapData.DeSerialize();
                await DataStore.Instance.DS.NoteData.DeSerialize();
                await DataStore.Instance.DS.PersonData.DeSerialize();
                await DataStore.Instance.DS.PersonNameData.DeSerialize();
                await DataStore.Instance.DS.PlaceData.DeSerialize();
                await DataStore.Instance.DS.RepositoryData.DeSerialize();
                await DataStore.Instance.DS.SourceData.DeSerialize();
                await DataStore.Instance.DS.TagData.DeSerialize();

                localGVLogging.RoutineExit(nameof(DeSerializeRepository));
            }
            catch (Exception ex)
            {
                localGVLogging.Progress("DeSerializeRepository - Exception ");
                SharedSharpSettings.DataSerialised = false;
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("Old data deserialization error.  Data loading cancelled", ex);
            }

            return;
        }

        public async Task SerializeRepository()
        {

            await DataStore.Instance.DS.AddressData.Serialize();
            await DataStore.Instance.DS.BookMarkCollection.Serialize();
            await DataStore.Instance.DS.CitationData.Serialize();
            await DataStore.Instance.DS.EventData.Serialize();
            await DataStore.Instance.DS.FamilyData.Serialize();
            await DataStore.Instance.DS.HeaderData.Serialize();
            await DataStore.Instance.DS.MediaData.Serialize();
            await DataStore.Instance.DS.NameMapData.Serialize();
            await DataStore.Instance.DS.NoteData.Serialize();
            await DataStore.Instance.DS.PersonData.Serialize();
            await DataStore.Instance.DS.PersonNameData.Serialize();
            await DataStore.Instance.DS.PlaceData.Serialize();
            await DataStore.Instance.DS.RepositoryData.Serialize();
            await DataStore.Instance.DS.SourceData.Serialize();
            await DataStore.Instance.DS.TagData.Serialize();

            localGVLogging.Progress("SerializeRepository - Completed ");

            SharedSharpSettings.DataSerialised = true;
        }


    }
}