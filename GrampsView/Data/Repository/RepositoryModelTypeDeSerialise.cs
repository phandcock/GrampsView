// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Models.DataModels;
using GrampsView.Models.HLinks;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using System.Runtime.Serialization;
using System.Text.Json;

namespace GrampsView.Data.Repository
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T1">
    /// Model Base.
    /// </typeparam>
    /// <typeparam name="T2">
    /// HLink Base.
    /// </typeparam>

    public class RepositoryModelTypeDeSerialise<T1, T2> : ObservableObject
        where T1 : ModelBase, new()
        where T2 : HLinkBase, new()
    {
        /// <summary>
        /// Initialize a simple random number generator.
        /// </summary>
        private readonly Random localRandomNumberGenerator = new();


        public string SerialisationName { get; }

        public RepositoryModelTypeDeSerialise(string argSerialisationName)
        {
            SerialisationName = argSerialisationName;
        }

        public async Task<RepositoryModelDictionary<T1, T2>> DeSerialize()
        {
            try
            {
                Ioc.Default.GetRequiredService<ILog>().DataLogEntryAdd($"DeSerialising {SerialisationName}");

                DataContractSerializer ser = new(typeof(DataInstance));

                FileInfo[] ttt = DataStore.Instance.AD.CurrentDataFolder.FolderasDirInfo.GetFiles(CommonRoutines.GetSerialFile(SerialisationName));

                // Check of the file exists
                if (ttt.Length != 1)
                {
                    ErrorInfo tt = new("DeSerializeRepository", "File Does not exist.  Reload the GPKG file")
                                {
                                    { "File", CommonRoutines.GetSerialFile(CommonRoutines.GetSerialFile(SerialisationName)) },
                                };

                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(tt);
                    SharedSharpSettings.DataSerialised = false;
                    return null;
                }

                //byte[] buffer = new byte[1024];

                FileStream isoStream = new FileStream(CommonRoutines.GetSerialFileFull(SerialisationName), FileMode.Open);

                //var ttt = await isoStream.ReadAsync(buffer, 0, 100);

                JsonSerializerOptions serializerOptions = CommonRoutines.GetSerializerOptions();

                return await JsonSerializer.DeserializeAsync<RepositoryModelDictionary<T1, T2>>(isoStream, serializerOptions);



            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, new ErrorInfo("Trying to deserialise object")
                {
                    new CardListLine("Data area",SerialisationName),
                });

                SharedSharpSettings.DataSerialised = false;

                return null;
            }
        }

    }
}