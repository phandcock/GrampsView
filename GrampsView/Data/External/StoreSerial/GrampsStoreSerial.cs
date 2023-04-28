// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Repository;

using SharedSharp.Errors.Interfaces;

namespace GrampsView.Data.External.StoreSerial
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

                // Bookmark
                await DataStore.Instance.DS.BookMarkCollection.DeSerialize();

                // Ioc.Default.GetRequiredService<ILog>().DataLogEntryAdd($"DeSerialising BookMarks");

                //DataContractSerializer ser = new(typeof(DataInstance));

                //FileInfo[] ttt = DataStore.Instance.AD.CurrentDataFolder.FolderasDirInfo.GetFiles(CommonRoutines.GetSerialFile("BookMarks"));

                //// Check of the file exists
                //if (ttt.Length != 1)
                //{
                //    ErrorInfo tt = new("DeSerializeRepository", "File Does not exist.  Reload the GPKG file")
                //                {
                //                    { "File", CommonRoutines.GetSerialFile("BookMarks") },
                //                };

                //    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(tt);
                //    SharedSharpSettings.DataSerialised = false;
                //}
                //else
                //{
                //    FileStream isoStream = new FileStream(CommonRoutines.GetSerialFileFull("BookMarks"), FileMode.Open);

                //    JsonSerializerOptions serializerOptions = CommonRoutines.GetSerializerOptions();

                //    DataStore.Instance.DS.BookMarkCollection = await JsonSerializer.DeserializeAsync<object>(isoStream, serializerOptions) as HLinkBackLinkModelCollection;
                //}

                await DataStore.Instance.DS.CitationData.DeSerialize();

                await DataStore.Instance.DS.EventData.DeSerialize();

                await DataStore.Instance.DS.FamilyData.DeSerialize();

                await DataStore.Instance.DS.MediaData.DeSerialize();

                await DataStore.Instance.DS.PersonData.DeSerialize();

                await DataStore.Instance.DS.PersonNameData.DeSerialize();

                // Check for nulls

                await DataStore.Instance.DS.SourceData.DeSerialize();

                // Check for nulls

                await DataStore.Instance.DS.TagData.DeSerialize();

                // TODO Finish setting the checks up on these
                await DataStore.Instance.DS.HeaderData.DeSerialize();
                await DataStore.Instance.DS.NameMapData.DeSerialize();
                await DataStore.Instance.DS.NoteData.DeSerialize();

                await DataStore.Instance.DS.PlaceData.DeSerialize();
                await DataStore.Instance.DS.RepositoryData.DeSerialize();

                localGVLogging.RoutineExit(nameof(DeSerializeRepository));
            }
            catch (Exception ex)
            {
                localGVLogging.Progress("DeSerializeRepository - Exception ");
                SharedSharpSettings.DataSerialised = false;
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("Old data deserialisation error.  Data loading cancelled", ex);
            }

            return;
        }

        public async Task SerializeRepository()
        {
            await DataStore.Instance.DS.AddressData.Serialize();

            // Backlink
            await DataStore.Instance.DS.BookMarkCollection.Serialize();

            //try
            //{
            //    JsonSerializerOptions serializerOptions = CommonRoutines.GetSerializerOptions();

            //    FileStream stream = new(CommonRoutines.GetSerialFileFull("Bookmarks"), FileMode.Create);

            //    await JsonSerializer.SerializeAsync(stream, DataStore.Instance.DS.BookMarkCollection, serializerOptions);
            //}
            //catch (Exception ex)
            //{
            //    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("Trying to serialise object ", ex);
            //    SharedSharpSettings.DataSerialised = false;
            //}

            await DataStore.Instance.DS.CitationData.Serialize();

            await DataStore.Instance.DS.EventData.Serialize();

            await DataStore.Instance.DS.FamilyData.Serialize();

            await DataStore.Instance.DS.MediaData.Serialize();

            await DataStore.Instance.DS.PersonData.Serialize();

            await DataStore.Instance.DS.PersonNameData.Serialize();

            await DataStore.Instance.DS.SourceData.Serialize();

            await DataStore.Instance.DS.TagData.Serialize();

            // TODO Finish setting the checks up on these
            await DataStore.Instance.DS.HeaderData.Serialize();
            await DataStore.Instance.DS.NameMapData.Serialize();
            await DataStore.Instance.DS.NoteData.Serialize();

            await DataStore.Instance.DS.PlaceData.Serialize();
            await DataStore.Instance.DS.RepositoryData.Serialize();

            localGVLogging.Progress("SerializeRepository - Completed ");

            SharedSharpSettings.DataSerialised = true;
        }

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <param name="theObject">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        //public async Task SerializeRMD(RepositoryModelDictionary<T1,T2> argObject)
        //        where T1 : ModelBase, new()
        //        where T2 : HLinkBase, new()
        //{
        //    try
        //    {
        //        JsonSerializerOptions serializerOptions = GetSerializerOptions();

        //        FileStream stream = new(DataStore.Instance.AD.CurrentDataFolder.GetAbsoluteFilePath(GetSerialFile(argObject)), FileMode.Create);

        //        await JsonSerializer.SerializeAsync(stream, argObject, serializerOptions);

        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("Trying to serialise object ", ex);
        //        SharedSharpSettings.DataSerialised = false;
        //    }
        //}
    }
}