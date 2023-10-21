// Copyright (c) phandcock. All rights reserved.

using GrampsView.Common;
using GrampsView.Models.DBModels;

using Microsoft.EntityFrameworkCore;

using SharedSharp.Errors.Interfaces;

using SQLite;

namespace GrampsView.Data.StoreDB
{
    public partial class StoreDB : DbContext, IStoreDB
    {
        private bool _IsOpen = false;

        public DbSet<CitationDBModel> CitationAccess { get; set; }

        public DbSet<EventDBModel> EventAccess { get; set; }

        public DbSet<FamilyDBModel> FamilyAccess { get; set; }

        public DbSet<NoteDBModel> NoteAccess { get; set; }

        public StoreDB()
        {
            this.Database.EnsureCreated();

            _IsOpen = true;
        }

        public async Task Clear()
        {
            _IsOpen = false;

            await InitialiseDB();
        }

        public async Task InitialiseDB()
        {
            try
            {
                if (!_IsOpen)
                {
                    _IsOpen = true;

                    SQLitePCL.Batteries_V2.Init();

                    bool t = Database.EnsureDeleted();

                    Database.EnsureCreated();

                    //string sql = Database.GenerateCreateScript();
                }

                _IsOpen = true;
            }
            catch (SQLiteException ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("InitialiseDB - SQLiteException", ex);
                return;
            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("InitialiseDB", ex);
                return;
            }
        }

        public bool IsOpen()
        {
            return _IsOpen;
        }

        public async Task OpenDB()
        {
            _IsOpen = true;
        }

        public async Task OpenOrCreate()
        {
            if (File.Exists(Constants.DatabasePath))
            {
                await OpenDB();
            }
            else
            {
                await InitialiseDB();
            }
        }

        public void Reload()
        {
            Database.CloseConnection();
            Database.OpenConnection();
        }

        void IStoreDB.SaveChanges()
        {
            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite($"Filename={Constants.DatabasePath}");

            string t = Constants.DatabasePath;
        }
    }
}