﻿// Copyright (c) phandcock.  All rights reserved.

using GrampsView.DBModels;
using GrampsView.Models.DataModels.Minor;

using Microsoft.EntityFrameworkCore;

namespace GrampsView.Data.StoreDB
{
    public interface IStoreDB
    {
        DbSet<AddressDBModel> AddressAccess { get; set; }

        DbSet<CitationDBModel> CitationAccess { get; }
        DbSet<EventDBModel> EventAccess { get; }

        DbSet<FamilyDBModel> FamilyAccess { get; }
        bool IsOpen { get; }
        DbSet<NoteDBModel> NoteAccess { get; }

        Task Clear();

        Task InitialiseDB();

        Task OpenDB();

        Task OpenOrCreate();

        void SaveChanges();
    }
}