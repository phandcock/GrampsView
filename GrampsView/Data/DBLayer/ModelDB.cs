// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.DataModels;
using GrampsView.Models.DBModels;
using GrampsView.Models.HLinks;

namespace GrampsView.Data.StoreDB
{
    public class ModelDB<T1, T2> : ObservableObject, IModelDB<T1, T2>
            where T1 : ModelBase, new()
            where T2 : HLinkBase, new()

    {
        private IStoreDB LocalDB { get; }

        public ModelDB()
        {
            LocalDB = Ioc.Default.GetRequiredService<IStoreDB>();
        }

        public bool DeleteItem(T1 item)
        {
            NoteDBModel t = LocalDB.Notes.Where(Id => Id.HLinkKey == item.HLinkKey).First();

            base.Remove(t);

            return true;
        }

        public T1 GetItem(string id)
        {
            T3 t = this.Where(Id => Id.HLinkKeyValue == id).First();

            return t.DeSerialise();
        }

        public bool SaveItem(T1 argModelBase)
        {
            T3 TheDBModel = new T3();

            TheDBModel.Serialise(argModelBase);

            base.Add(TheDBModel);

            return true;
        }

        public bool AddItem(T1 argModelBase)
        {
            T3 TheDBModel = new T3();

            TheDBModel.Serialise(argModelBase);

            base.Add(TheDBModel);

            return true;
        }


    }
}