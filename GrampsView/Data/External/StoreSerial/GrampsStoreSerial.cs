namespace GrampsView.Data.External.StoreSerial
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;

    using Newtonsoft.Json;

    using System;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Runtime.Serialization;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public class GrampsStoreSerial : CommonBindableBase, IGrampsStoreSerial
    {
        /// <summary>
        /// The local common logging.
        /// </summary>
        private readonly ICommonLogging localGVLogging;

        /// <summary>
        /// Initializes a new instance of the <see cref="GrampsStoreSerial"/> class.
        /// </summary>
        /// <param name="iocGVProgress">
        /// The ioc gv progress.
        /// </param>
        /// <param name="iocGVLogging">
        /// The ioc gv logging.
        /// </param>
        public GrampsStoreSerial(ICommonLogging iocGVLogging)
        {
            // save injected references for later
            localGVLogging = iocGVLogging;
        }

        /// <summary>
        /// Deserialise the previously serialised repository. Perform as a single step so that it
        /// goes faster at the cost of providing less feedbak to the user.
        /// </summary>

        public void DeSerializeRepository()
        {
            localGVLogging.RoutineEntry(nameof(DeSerializeRepository));

            try
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(DataInstance));

                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    // Check of the file exists
                    string DataInstanceFileName = typeof(DataInstance).Name.Trim() + ".json";

                    if (!isoStore.FileExists(DataInstanceFileName))
                    {
                        ErrorInfo t = new ErrorInfo("DeSerializeRepository", "File Does not exist.  Reload the GPKG file")
                                {
                                    { "File", DataInstanceFileName },
                                };

                        DataStore.Instance.CN.NotifyError(t);
                        CommonLocalSettings.DataSerialised = false;
                        return;
                    }

                    var stream = new IsolatedStorageFileStream(DataInstanceFileName, FileMode.Open, isoStore);

                    using (StreamReader file = new StreamReader(stream))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Converters.Add(new GrampsView.Converters.NewtonSoftColorConverter());

                        DataInstance t = (DataInstance)serializer.Deserialize(file, typeof(DataInstance));

                        // Check for nulls
                        if (t.AddressData != null)
                        {
                            DataStore.Instance.DS.AddressData = t.AddressData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.Instance.CN.NotifyError(new ErrorInfo("Bad Address deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                        }

                        if (t.BookMarkCollection != null)
                        {
                            DataStore.Instance.DS.BookMarkCollection = t.BookMarkCollection;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.Instance.CN.NotifyError(new ErrorInfo("Bad BookMark deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                        }

                        if (t.CitationData != null)
                        {
                            DataStore.Instance.DS.CitationData = t.CitationData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.Instance.CN.NotifyError(new ErrorInfo("Bad Citation deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                        }

                        if (t.EventData != null)
                        {
                            DataStore.Instance.DS.EventData = t.EventData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.Instance.CN.NotifyError(new ErrorInfo("Bad Event deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                        }

                        if (t.FamilyData != null)
                        {
                            DataStore.Instance.DS.FamilyData = t.FamilyData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.Instance.CN.NotifyError(new ErrorInfo("Bad Family deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                        }

                        if (t.MediaData != null)
                        {
                            DataStore.Instance.DS.MediaData = t.MediaData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.Instance.CN.NotifyError(new ErrorInfo("Bad Media deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                        }

                        if (t.PersonData != null)
                        {
                            DataStore.Instance.DS.PersonData = t.PersonData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.Instance.CN.NotifyError(new ErrorInfo("Bad Person deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                        }

                        if (t.PersonNameData != null)
                        {
                            DataStore.Instance.DS.PersonNameData = t.PersonNameData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.Instance.CN.NotifyError(new ErrorInfo("Bad Person Name deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                        }

                        // Check for nulls
                        if (t.SourceData != null)
                        {
                            DataStore.Instance.DS.SourceData = t.SourceData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.Instance.CN.NotifyError(new ErrorInfo("Bad Source data deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                        }

                        // Check for nulls
                        if (t.TagData != null)
                        {
                            DataStore.Instance.DS.TagData = t.TagData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.Instance.CN.NotifyError(new ErrorInfo("Bad Tag data deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                        }

                        // TODO Finish setting the checks up on these
                        DataStore.Instance.DS.HeaderData = t.HeaderData;
                        DataStore.Instance.DS.NameMapData = t.NameMapData;
                        DataStore.Instance.DS.NoteData = t.NoteData;

                        DataStore.Instance.DS.PlaceData = t.PlaceData;
                        DataStore.Instance.DS.RepositoryData = t.RepositoryData;
                    }
                }

                localGVLogging.RoutineExit(nameof(DeSerializeRepository));
            }
            catch (Exception ex)
            {
                localGVLogging.Progress("DeSerializeRepository - Exception ");
                CommonLocalSettings.DataSerialised = false;
                DataStore.Instance.CN.NotifyException("Old data deserialisation error.  Data loading cancelled", ex);
                throw;
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
        public bool SerializeObject(object theObject)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();

                serializer.Converters.Add(new GrampsView.Converters.NewtonSoftColorConverter());

                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(typeof(DataInstance).Name.Trim() + ".json", FileMode.Create, isoStore))
                    {
                        StreamWriter sw = new StreamWriter(stream);

                        using (JsonWriter writer = new JsonTextWriter(sw))
                        {
                            serializer.Serialize(writer, theObject);
                        }
                    }
                }

                CommonLocalSettings.DataSerialised = true;
                return true;
            }
            catch (Exception ex)
            {
                DataStore.Instance.CN.NotifyException("Trying to serialise object ", ex);
                CommonLocalSettings.DataSerialised = false;
                throw;
            }
        }
    }
}