using GrampsView.Common;
using GrampsView.Data.Repository;

using Microsoft.Extensions.DependencyInjection;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.CommunityToolkit.ObjectModel;

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
        private readonly SharedSharp.Logging.Interfaces.ILog localGVLogging;

        /// <summary>
        /// Initializes a new instance of the <see cref="GrampsStoreSerial"/> class.
        /// </summary>
        /// <param name="iocGVProgress">
        /// The ioc gv progress.
        /// </param>
        /// <param name="iocGVLogging">
        /// The ioc gv logging.
        /// </param>
        public GrampsStoreSerial(SharedSharp.Logging.Interfaces.ILog iocGVLogging)
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
                DataContractSerializer ser = new DataContractSerializer(typeof(DataInstance));

                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    // Check of the file exists
                    if (!isoStore.FileExists(GetSerialFile()))
                    {
                        ErrorInfo tt = new ErrorInfo("DeSerializeRepository", "File Does not exist.  Reload the GPKG file")
                                {
                                    { "File", GetSerialFile() },
                                };

                        App.Current.Services.GetService<IErrorNotifications>().NotifyError(tt);
                        CommonLocalSettings.DataSerialised = false;
                        return;
                    }

                    //byte[] buffer = new byte[1024];

                    IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(GetSerialFile(), FileMode.Open, isoStore);

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
                        CommonLocalSettings.DataSerialised = false;
                        App.Current.Services.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Address deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                    }

                    if (t.BookMarkCollection != null)
                    {
                        DataStore.Instance.DS.BookMarkCollection = t.BookMarkCollection;
                    }
                    else
                    {
                        CommonLocalSettings.DataSerialised = false;
                        App.Current.Services.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad BookMark deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                    }

                    if (t.CitationData != null)
                    {
                        DataStore.Instance.DS.CitationData = t.CitationData;
                    }
                    else
                    {
                        CommonLocalSettings.DataSerialised = false;
                        App.Current.Services.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Citation deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                    }

                    if (t.EventData != null)
                    {
                        DataStore.Instance.DS.EventData = t.EventData;
                    }
                    else
                    {
                        CommonLocalSettings.DataSerialised = false;
                        App.Current.Services.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Event deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                    }

                    if (t.FamilyData != null)
                    {
                        DataStore.Instance.DS.FamilyData = t.FamilyData;
                    }
                    else
                    {
                        CommonLocalSettings.DataSerialised = false;
                        App.Current.Services.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Family deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                    }

                    if (t.MediaData != null)
                    {
                        DataStore.Instance.DS.MediaData = t.MediaData;
                    }
                    else
                    {
                        CommonLocalSettings.DataSerialised = false;
                        App.Current.Services.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Media deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                    }

                    if (t.PersonData != null)
                    {
                        DataStore.Instance.DS.PersonData = t.PersonData;
                    }
                    else
                    {
                        CommonLocalSettings.DataSerialised = false;
                        App.Current.Services.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Person deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                    }

                    if (t.PersonNameData != null)
                    {
                        DataStore.Instance.DS.PersonNameData = t.PersonNameData;
                    }
                    else
                    {
                        CommonLocalSettings.DataSerialised = false;
                        App.Current.Services.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Person Name deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                    }

                    // Check for nulls
                    if (t.SourceData != null)
                    {
                        DataStore.Instance.DS.SourceData = t.SourceData;
                    }
                    else
                    {
                        CommonLocalSettings.DataSerialised = false;
                        App.Current.Services.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Source data deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                    }

                    // Check for nulls
                    if (t.TagData != null)
                    {
                        DataStore.Instance.DS.TagData = t.TagData;
                    }
                    else
                    {
                        CommonLocalSettings.DataSerialised = false;
                        App.Current.Services.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Tag data deserialisation error.  Data loading cancelled. Restart the program and reload the data."));
                    }

                    // TODO Finish setting the checks up on these
                    DataStore.Instance.DS.HeaderData = t.HeaderData;
                    DataStore.Instance.DS.NameMapData = t.NameMapData;
                    DataStore.Instance.DS.NoteData = t.NoteData;

                    DataStore.Instance.DS.PlaceData = t.PlaceData;
                    DataStore.Instance.DS.RepositoryData = t.RepositoryData;
                }

                localGVLogging.RoutineExit(nameof(DeSerializeRepository));
            }
            catch (Exception ex)
            {
                localGVLogging.Progress("DeSerializeRepository - Exception ");
                CommonLocalSettings.DataSerialised = false;
                App.Current.Services.GetService<IErrorNotifications>().NotifyException("Old data deserialisation error.  Data loading cancelled", ex);
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

                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using IsolatedStorageFileStream stream = new IsolatedStorageFileStream(GetSerialFile(), FileMode.Create, isoStore);
                    //StreamWriter sw = new StreamWriter(stream);

                    await JsonSerializer.SerializeAsync(stream, theObject, serializerOptions);
                }

                byte[] buffer = new byte[1024];

                IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(GetSerialFile(), FileMode.Open, IsolatedStorageFile.GetUserStoreForApplication());

                int ttt = isoStream.Read(buffer, 0, 100);

                CommonLocalSettings.DataSerialised = true;
                return;
            }
            catch (Exception ex)
            {
                App.Current.Services.GetService<IErrorNotifications>().NotifyException("Trying to serialise object ", ex);
                CommonLocalSettings.DataSerialised = false;
            }
        }

        private string GetSerialFile()
        {
            return typeof(DataInstance).Name.Trim() + ".json";
        }

        private JsonSerializerOptions GetSerializerOptions()
        {
            JsonSerializerOptions serialzerOptions = new JsonSerializerOptions();

            // Special converter for colours
            serialzerOptions.Converters.Add(new Converters.JsonColorConverter());
            serialzerOptions.Converters.Add(new Converters.JsonDateObjectModelConverter());

            //// TODO Why does this work Preserve reference data
            //serialzerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;

            serialzerOptions.IgnoreReadOnlyFields = true;
            serialzerOptions.IgnoreReadOnlyProperties = true;

            return serialzerOptions;
        }
    }
}