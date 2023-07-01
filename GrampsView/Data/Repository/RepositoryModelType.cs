// Copyright (c) phandcock.  All rights reserved.

using CommunityToolkit.Diagnostics;

using GrampsView.Common;
using GrampsView.Exceptions;
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

    public class RepositoryModelDictionary<T1, T2> : Dictionary<string, T1>, IRepositoryModelDictionary<T1, T2>
        where T1 : ModelBase, new()
        where T2 : HLinkBase, new()
    {
        /// <summary>
        /// Initialize a simple random number generator.
        /// </summary>
        private readonly Random localRandomNumberGenerator = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryModelDictionary{T, U}"/> class.
        /// </summary>
        public RepositoryModelDictionary(string argSerialisationName)
        {
            SerialisationName = argSerialisationName;
        }

        public RepositoryModelDictionary()
        {
            // Only for serialisation
        }

        public List<T1> GetList => Values.ToList();
        public string SerialisationName { get; private set; } = string.Empty;

        public new T1 this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                {
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("Null or empty HLinkKey"));
                }

                //if (key == "_e5bfa72904e68ce059252b501df" || key == "_e5bfa72904e68ce059252b501df")
                //{
                //}

                return Find(key);
            }

            set =>
                //if (string.IsNullOrEmpty(key))
                //{
                //}

                //if (key == "_e5bfa72904e68ce059252b501df" || key == "_e5bfa72904e68ce059252b501df")
                //{
                //}

                base[key] = value;
        }

        public T1 this[T2 hLink]
        {
            get
            {
                if (hLink.HLinkKey.Valid)
                {
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("Null or empty HLinkKey"));
                }

                //if (hLink.HLinkKey.Value == "_e5bfa72904e68ce059252b501df" || hLink.HLinkKey.Value == "_e5bfa72904e68ce059252b501df")
                //{
                //}

                return Find(hLink.HLinkKey.Value);
            }
        }

        /// <summary>
        /// Adds the specified argument to the end of the list and updates the Indexes.
        /// </summary>
        /// <param name="arg">
        /// The model to add.
        /// </param>
        public void Add(T1 arg)
        {
            if (!arg.HLinkKey.Valid)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("Null or empty HLinkKey"));
            }

            //Update(localItems.Count, arg);

            //if (arg.HLinkKey == "_e5bfa72904e68ce059252b501df" || arg.HLinkKey == "_e5bfa72904e68ce059252b501df")
            //{
            //}

            string key = KeySelector(arg);

            if (ContainsKey(key))
            {
                throw new DuplicateKeyException(key);
            }

            Add(arg.HLinkKey.Value, arg);
        }

        public async Task DeSerialize()
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
                    return;
                }

                //byte[] buffer = new byte[1024];

                FileStream isoStream = new FileStream(CommonRoutines.GetSerialFileFull(SerialisationName), FileMode.Open);

                //var ttt = await isoStream.ReadAsync(buffer, 0, 100);

                JsonSerializerOptions serializerOptions = CommonRoutines.GetSerializerOptions();

                RepositoryModelDictionary<T1, T2> t = await JsonSerializer.DeserializeAsync<RepositoryModelDictionary<T1, T2>>(isoStream, serializerOptions);
                this.Clear();
                foreach (KeyValuePair<string, T1> item in t)
                {
                    this.Add(item.Key, item.Value);
                }

                return;
            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, new ErrorInfo("Trying to deserialise object")
                {
                    new CardListLine("Data area",SerialisationName),
                });

                SharedSharpSettings.DataSerialised = false;

                return;
            }
        }

        /// <summary>
        /// Gets or sets the element with the specified key. If setting a new value, new value must
        /// have same key.
        /// </summary>
        /// <param name="key">
        /// Key of element to replace.
        /// </param>
        /// <returns>
        /// </returns>
        public T1 Find(string key)
        {
            return ContainsKey(key) == true ? base[key] : new T1();
        }

        /// <summary>
        /// Gets the model from h link.
        /// </summary>
        /// <param name="argHLink">
        /// The argument h link.
        /// </param>
        /// <returns>
        /// </returns>
        public T1 GetModelFromHLink(T2 argHLink)
        {
            return argHLink is null ? throw new ArgumentNullException(nameof(argHLink)) : GetModelFromHLink(argHLink);
        }

        /// <summary>
        /// Gets the model from h link.
        /// </summary>
        /// <param name="argHLink">
        /// The argument h link.
        /// </param>
        /// <returns>
        /// </returns>
        public T1 GetModelFromHLink(string argHLink)
        {
            T1 tempMO = Values.FirstOrDefault(x => x.HLinkKey.Value == argHLink);

            return tempMO ?? new T1();
        }

        /// <summary>
        /// returns a random object from the objects recorded.
        /// </summary>
        public T1 GetRandomItem()
        {
            if (Count > 0)
            {
                return Values.ElementAt(localRandomNumberGenerator.Next(0, Count));
            }
            else
            {
                // return nothing
                return new T1();
            }
        }

        public async Task Serialize()
        {
            try
            {
                Guard.IsNotNull(SerialisationName, nameof(SerialisationName));

                Ioc.Default.GetRequiredService<ILog>().DataLogEntryAdd($"Serialising {CommonRoutines.GetSerialFile(SerialisationName)}");

                JsonSerializerOptions serializerOptions = CommonRoutines.GetSerializerOptions();

                FileStream stream = new(CommonRoutines.GetSerialFileFull(SerialisationName), FileMode.Create);

                await JsonSerializer.SerializeAsync(stream, this, serializerOptions);

                stream.Close();
                stream.Dispose();

                return;
            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, new ErrorInfo("Trying to Serialise object")
                {
                    new CardListLine("Data area",SerialisationName),
                });
                SharedSharpSettings.DataSerialised = false;
            }
        }

        /// <summary>
        /// Keys the selector.
        /// </summary>
        /// <param name="argModel">
        /// The argument.
        /// </param>
        /// <returns>
        /// </returns>
        private string KeySelector(T1 argModel)
        {
            return argModel.HLinkKey.Value;
        }
    }
}