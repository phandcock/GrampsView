// Copyright (c) phandcock. All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Data.Repository;
using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Minor;
using GrampsView.Models.HLinks.Models;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="GrampsStoreSerial"/> class.
        /// </summary>
        /// <param name="iocGVLogging">
        /// The ioc gv logging.
        /// </param>
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
                DataStore.Instance.DS.AddressData = await new RepositoryModelTypeDeSerialise<AddressModel, HLinkAdressModel>("AddressData").DeSerialize();
                await DataStore.Instance.DS.BookMarkCollection.DeSerialize();
                DataStore.Instance.DS.HeaderData = await new RepositoryModelTypeDeSerialise<HeaderModel, HLinkHeaderModel>("HeaderData").DeSerialize();
                DataStore.Instance.DS.MediaData = await new RepositoryModelTypeDeSerialise<MediaModel, HLinkMediaModel>("MediaData").DeSerialize();
                DataStore.Instance.DS.NameMapData = await new RepositoryModelTypeDeSerialise<NameMapModel, HLinkNameMapModel>("NameMapData").DeSerialize();
                DataStore.Instance.DS.PersonData = await new RepositoryModelTypeDeSerialise<PersonModel, HLinkPersonModel>("PersonData").DeSerialize();
                DataStore.Instance.DS.PersonNameData = await new RepositoryModelTypeDeSerialise<PersonNameModel, HLinkPersonNameModel>("PersonNameData").DeSerialize();
                DataStore.Instance.DS.PlaceData = await new RepositoryModelTypeDeSerialise<PlaceModel, HLinkPlaceModel>("PlaceData").DeSerialize();
                DataStore.Instance.DS.RepositoryData = await new RepositoryModelTypeDeSerialise<RepositoryModel, HLinkRepositoryModel>("RepositoryData").DeSerialize();
                DataStore.Instance.DS.SourceData = await new RepositoryModelTypeDeSerialise<SourceModel, HLinkSourceModel>("SourceData").DeSerialize();
                DataStore.Instance.DS.TagData = await new RepositoryModelTypeDeSerialise<TagModel, HLinkTagModel>("TagData").DeSerialize();

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
            await DataStore.Instance.DS.HeaderData.Serialize();
            await DataStore.Instance.DS.MediaData.Serialize();
            await DataStore.Instance.DS.NameMapData.Serialize();
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