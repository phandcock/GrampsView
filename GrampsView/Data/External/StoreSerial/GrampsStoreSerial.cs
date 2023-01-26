// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Repository;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using System.Runtime.Serialization;
using System.Text.Json;

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
        /// goes faster at the cost of providing less feedbak to the user.
        /// </summary>

        public async Task DeSerializeRepository()
        {
            localGVLogging.RoutineEntry(nameof(DeSerializeRepository));

            try
            {
                DataContractSerializer ser = new(typeof(DataInstance));

                FileInfo[] ttt = DataStore.Instance.AD.CurrentDataFolder.FolderasDirInfo.GetFiles(GetSerialFile());

                // Check of the file exists
                if (ttt.Length != 1)
                {
                    ErrorInfo tt = new("DeSerializeRepository", "File Does not exist.  Reload the GPKG file")
                                {
                                    { "File", GetSerialFile() },
                                };

                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(tt);
                    SharedSharpSettings.DataSerialised = false;
                    return;
                }

                //byte[] buffer = new byte[1024];

                FileStream isoStream = new FileStream(DataStore.Instance.AD.CurrentDataFolder.GetAbsoluteFilePath(GetSerialFile()), FileMode.Open);

                //var ttt = await isoStream.ReadAsync(buffer, 0, 100);

                JsonSerializerOptions serializerOptions = GetSerializerOptions();

                DataInstance t = await JsonSerializer.DeserializeAsync<DataInstance>(isoStream, serializerOptions);

                // Check for nulls
                if (t.AddressData != null)
                {
                    DataStore.Instance.DS.AddressData = t.AddressData;
                }
                else
                {
                    SharedSharpSettings.DataSerialised = false;
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Address deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                }

                if (t.BookMarkCollection != null)
                {
                    DataStore.Instance.DS.BookMarkCollection = t.BookMarkCollection;
                }
                else
                {
                    SharedSharpSettings.DataSerialised = false;
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad BookMark deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                }

                if (t.CitationData != null)
                {
                    DataStore.Instance.DS.CitationData = t.CitationData;
                }
                else
                {
                    SharedSharpSettings.DataSerialised = false;
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Citation deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                }

                if (t.EventData != null)
                {
                    DataStore.Instance.DS.EventData = t.EventData;
                }
                else
                {
                    SharedSharpSettings.DataSerialised = false;
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Event deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                }

                if (t.FamilyData != null)
                {
                    DataStore.Instance.DS.FamilyData = t.FamilyData;
                }
                else
                {
                    SharedSharpSettings.DataSerialised = false;
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Family deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                }

                if (t.MediaData != null)
                {
                    DataStore.Instance.DS.MediaData = t.MediaData;
                }
                else
                {
                    SharedSharpSettings.DataSerialised = false;
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Media deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                }

                if (t.PersonData != null)
                {
                    DataStore.Instance.DS.PersonData = t.PersonData;
                }
                else
                {
                    SharedSharpSettings.DataSerialised = false;
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Person deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                }

                if (t.PersonNameData != null)
                {
                    DataStore.Instance.DS.PersonNameData = t.PersonNameData;
                }
                else
                {
                    SharedSharpSettings.DataSerialised = false;
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Person Name deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                }

                // Check for nulls
                if (t.SourceData != null)
                {
                    DataStore.Instance.DS.SourceData = t.SourceData;
                }
                else
                {
                    SharedSharpSettings.DataSerialised = false;
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Source data deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                }

                // Check for nulls
                if (t.TagData != null)
                {
                    DataStore.Instance.DS.TagData = t.TagData;
                }
                else
                {
                    SharedSharpSettings.DataSerialised = false;
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Tag data deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                }

                // TODO Finish setting the checks up on these
                DataStore.Instance.DS.HeaderData = t.HeaderData;
                DataStore.Instance.DS.NameMapData = t.NameMapData;
                DataStore.Instance.DS.NoteData = t.NoteData;

                DataStore.Instance.DS.PlaceData = t.PlaceData;
                DataStore.Instance.DS.RepositoryData = t.RepositoryData;

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

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <param name="theObject">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public async Task SerializeObject(DataInstance theObject)
        {
            try
            {
                JsonSerializerOptions serializerOptions = GetSerializerOptions();

                FileStream stream = new(DataStore.Instance.AD.CurrentDataFolder.GetAbsoluteFilePath(GetSerialFile()), FileMode.Create);
                //StreamWriter sw = new StreamWriter(stream);

                await JsonSerializer.SerializeAsync(stream, theObject, serializerOptions);

                //DateObjectModelBase tt = DV.MediaDV.DataViewData[10].GDateValue;

                //string json = JsonSerializer.Serialize<DateObjectModelBase>(tt, serializerOptions);

                //byte[] buffer = new byte[1024];

                //IsolatedStorageFileStream isoStream = new(GetSerialFile(), FileMode.Open, IsolatedStorageFile.GetUserStoreForApplication());

                //int ttt = isoStream.Read(buffer, 0, 100);

                SharedSharpSettings.DataSerialised = true;
                return;
            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("Trying to serialise object ", ex);
                SharedSharpSettings.DataSerialised = false;
            }
        }

        private static string GetSerialFile()
        {
            return typeof(DataInstance).Name.Trim() + ".json";
        }

        private JsonSerializerOptions GetSerializerOptions()
        {
            JsonSerializerOptions serialzerOptions = new();

            // Special converter for colours
            serialzerOptions.Converters.Add(new Converters.JsonColorConverter());
            //serialzerOptions.Converters.Add(new Converters.JsonDateObjectModelConverter());

            //// TODO Why does this work Preserve reference data
            //serialzerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;

            serialzerOptions.IgnoreReadOnlyFields = true;
            serialzerOptions.IgnoreReadOnlyProperties = true;

            return serialzerOptions;
        }
    }
}