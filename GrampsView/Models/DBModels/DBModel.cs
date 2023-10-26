// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Models.DataModels;
using GrampsView.Models.DBModels.Interfaces;
using GrampsView.Models.HLinks;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json;

namespace GrampsView.Models.DBModels
{
    public class DBModel<T1, T2> : IDBModel<T1, T2>
            where T1 : ModelBase, new()
            where T2 : HLinkBase, new()
    {
        public DBModel()
        {
        }

        public DBModel(T1 argModel)
        {
            Serialise(argModel);
        }

        [Key]
        public string HLinkKeyValue { get; set; } = Guid.NewGuid().ToString();

        public string serialisedModel { get; set; } = string.Empty;

        public T1 DeSerialise()
        {
            JsonSerializerOptions serializerOptions = CommonRoutines.GetSerializerOptions();

            if (string.IsNullOrEmpty(serialisedModel))
            {
                return new T1();
            }

            return JsonSerializer.Deserialize<T1>(serialisedModel, serializerOptions) ?? new();
        }

        public void Serialise(T1 argModel)
        {
            JsonSerializerOptions serializerOptions = CommonRoutines.GetSerializerOptions();

            serialisedModel = JsonSerializer.Serialize<T1>(argModel, serializerOptions);

            HLinkKeyValue = argModel.HLinkKey.Value;

            Debug.WriteLine($"Key {HLinkKeyValue}");
        }
    }
}