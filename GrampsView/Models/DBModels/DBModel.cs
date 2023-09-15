// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Models.DataModels;
using GrampsView.Models.HLinks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public HLinkKey HLinkKey
        {
            get
            {
                return new HLinkKey(HLinkKeyValue);
            }
        }

        [Key]
        public string HLinkKeyValue { get; set; } = string.Empty;

        private string serialisedModel { get; set; } = string.Empty;

        public T1 DeSerialise()
        {
            JsonSerializerOptions serializerOptions = CommonRoutines.GetSerializerOptions();

            return JsonSerializer.Deserialize<T1>(serialisedModel, serializerOptions);
        }

        public void Serialise(T1 argNoteModel)
        {
            JsonSerializerOptions serializerOptions = CommonRoutines.GetSerializerOptions();

            serialisedModel = JsonSerializer.Serialize<T1>(argNoteModel, serializerOptions);

            HLinkKeyValue = argNoteModel.HLinkKey.Value;
        }
    }
}