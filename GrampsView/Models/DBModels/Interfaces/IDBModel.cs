// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.DataModels;
using GrampsView.Models.HLinks;

using System.ComponentModel.DataAnnotations;

namespace GrampsView.Models.DBModels.Interfaces
{
    public interface IDBModel<T1, T2>
            where T1 : ModelBase, new()
            where T2 : HLinkBase, new()
    {
        [Key]
        string HLinkKeyValue { get; set; }

        public T1 DeSerialise();

        public void Serialise(T1 argModel);
    }
}