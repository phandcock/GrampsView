// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Models.DBModels;

using Microsoft.EntityFrameworkCore;

namespace GrampsView.Data.StoreDB
{
    public partial class StoreDB : DbContext, IStoreDB
    {
        private bool _IsOpen = false;

        public StoreDB()
        {
            this.Database.EnsureCreated();

            _IsOpen = true;
        }

        public DbSet<NoteDBModel> NoteAccess { get; set; }

        public async Task InitialiseDB()
        {
            //try
            //{
            //    if (CommonStatic.Database is not null)
            //    {
            //        return;
            //    }

            //    CommonStatic.Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            //    CreateTableResult result = await CommonStatic.Database.CreateTableAsync(typeof(NoteModel));

            //    _IsOpen = true;
            //}
            //catch (SQLiteException ex)
            //{
            //    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("InitialiseDB - SQLiteException", ex);
            //    return;
            //}
            //catch (Exception ex)
            //{
            //    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("InitialiseDB", ex);
            //    return;
            //}
        }

        public bool IsOpen()
        {
            return _IsOpen;
        }

        public async Task OpenDB()
        {
            //if (CommonStatic.Database is not null)
            //{
            //    return;
            //}

            //CommonStatic.Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            //_IsOpen = true;
        }

        public async Task OpenOrCreate()
        {
            //if (File.Exists(Constants.DatabasePath))
            //{
            //    await OpenDB();
            //}
            //else
            //{
            //    await InitialiseDB();
            //}
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite($"Filename={Constants.DatabasePath}");

            string t = Constants.DatabasePath;
        }

        void IStoreDB.SaveChanges()
        {
            this.SaveChanges();
        }
    }
}