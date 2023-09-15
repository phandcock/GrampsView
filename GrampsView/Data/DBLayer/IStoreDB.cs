// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.DBModels;

using Microsoft.EntityFrameworkCore;

namespace GrampsView.Data.StoreDB
{
    public interface IStoreDB
    {
        DbSet<NoteDBModel> NoteAccess { get; }

        Task InitialiseDB();

        bool IsOpen();

        Task OpenDB();

        Task OpenOrCreate();

        void SaveChanges();
    }
}