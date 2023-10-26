// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.DataModels;
using GrampsView.Models.HLinks;

namespace GrampsView.Models.DBModels.Interfaces
{
    public interface IEventDBModel<T1, T2> : IDBModel<T1, T2>
            where T1 : ModelBase, new()
            where T2 : HLinkBase, new()
    {
    }
}