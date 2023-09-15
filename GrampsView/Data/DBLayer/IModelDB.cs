// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.Data.StoreDB
{
    public interface IModelDB<T1, T2, T3>
    {
        bool AddItem(T1 argModelBase);

        bool DeleteItem(T1 item);

        T1 GetItem(string id);

        bool SaveItem(T1 argModelBase);
    }
}