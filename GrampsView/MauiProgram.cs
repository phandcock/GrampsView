﻿// Copyright (c) phandcock.  All rights reserved.

using CommunityToolkit.Maui.Markup;

using GrampsView.Common;
using GrampsView.Data.ExternalStorage;
using GrampsView.Data.Repository;
using GrampsView.Data.StoreDB;
using GrampsView.Data.StoreFile;
using GrampsView.Data.StorePostLoad;
using GrampsView.Data.StoreSerial;
using GrampsView.Data.StoreXML;
using GrampsView.ViewModels;
using GrampsView.ViewModels.Citation;
using GrampsView.ViewModels.Event;
using GrampsView.ViewModels.Family;
using GrampsView.ViewModels.Media;
using GrampsView.ViewModels.MinorModels;
using GrampsView.ViewModels.MinorPages;
using GrampsView.ViewModels.Note;
using GrampsView.ViewModels.Person;
using GrampsView.ViewModels.Places;
using GrampsView.ViewModels.Repository;
using GrampsView.ViewModels.Sources;
using GrampsView.ViewModels.StartupPages;
using GrampsView.ViewModels.Tags;

using IeuanWalker.Maui.StateButton;

using Microsoft.Maui.LifecycleEvents;

using SharedSharp;
using SharedSharp.Common.Interfaces;
using SharedSharp.Services;
using SharedSharp.Services.Interfaces;

using System.Diagnostics;

namespace GrampsView
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();

            _ = builder.UseMauiApp<App>()
                    .UseMauiCommunityToolkit()
                    .UseMauiCommunityToolkitMarkup()
                    .UseMauiCommunityToolkitMediaElement()
                    .SharedSharpInit()
                    .ConfigureEssentials()
                    .UseStateButton()
                    .RegisterFonts()
                    .RegisterLifeCycleEvents()
                    .RegisterHandlers()
                    .RegisterServices();

            _ = builder.Services.AddLocalization();

            MauiApp mauiApp = builder.Build();

            Ioc.Default.ConfigureServices(mauiApp.Services);

            SSharpCore.SharedSharpStart(argLogLevel: Microsoft.Extensions.Logging.LogLevel.Trace);

            SharedSharpGeneral.MSAppCenterInit(argMSAppCenterSecretAndroid: Secret.AndroidSecret, argMSAppCenterSecretWinUI: Secret.UWPSecret, argLogLevel: Microsoft.AppCenter.LogLevel.Warn);

            return mauiApp;
        }

        public static MauiAppBuilder RegisterFonts(this MauiAppBuilder builder)
        {
            return builder.ConfigureFonts(fonts =>
            {
                // Your fonts here...
                //fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });
        }

        public static MauiAppBuilder RegisterHandlers(this MauiAppBuilder builder)
        {
            //// RegisterMappers();
            //return builder.ConfigureMauiHandlers(handlers =>
            //{
            //});

            return builder;
        }

        public static MauiAppBuilder RegisterLifeCycleEvents(this MauiAppBuilder builder)
        {
            _ = builder.ConfigureLifecycleEvents(events =>
            {
                Debug.WriteLine("RegisterLifeCycleEvents");
            });

            return builder;
        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            // Add your services here...
            // Default method
            //builder.Services.Add();
            // Scoped objects are the same within a request, but different across different requests.
            //builder.Services.AddScoped();
            // Singleton objects are created as a single instance throughout the application. It creates the instance for the first time and reuses the same object in the all calls.
            //builder.Services.AddSingleton();
            // Transient objects lifetime services are created each time they are requested. This lifetime works best for lightweight, stateless services.
            //builder.Services.AddTransient();

            // Add Services
            _ = builder.Services.AddSingleton<IDatabaseReloadDisplayService, DatabaseReloadDisplayService>();
            _ = builder.Services.AddSingleton<IDataRepositoryManager, DataRepositoryManager>();
            _ = builder.Services.AddSingleton<IGrampsStoreSerial, GrampsStoreSerial>();
            _ = builder.Services.AddSingleton<ISharedSharpAppInit, AppInit>();
            _ = builder.Services.AddSingleton<IStoreDB, StoreDB>();
            _ = builder.Services.AddSingleton<IStoreFile, StoreFile>();
            _ = builder.Services.AddSingleton<IStoreFileTar, StoreFileTar>();
            _ = builder.Services.AddSingleton<IStoreFileZip, StoreFileZip>();
            _ = builder.Services.AddSingleton<IStorePostLoad, StorePostLoad>();
            _ = builder.Services.AddSingleton<IStoreXML, StoreXML>();

            // Viewmodels
            _ = builder.Services.AddTransient<AboutViewModel>();
            //_ = builder.Services.AddTransient<AppShellViewModel>();
            _ = builder.Services.AddTransient<AddressDetailViewModel>();
            _ = builder.Services.AddTransient<AttributeDetailViewModel>();

            _ = builder.Services.AddTransient<BookMarkListViewModel>();

            _ = builder.Services.AddTransient<ChildRefDetailViewModel>();
            _ = builder.Services.AddTransient<CitationDetailViewModel>();
            _ = builder.Services.AddTransient<CitationListViewModel>();

            _ = builder.Services.AddTransient<DateRangeDetailViewModel>();
            _ = builder.Services.AddTransient<DateSpanDetailViewModel>();
            _ = builder.Services.AddTransient<DateStrDetailViewModel>();
            _ = builder.Services.AddTransient<DateValDetailViewModel>();

            _ = builder.Services.AddTransient<EventDetailViewModel>();
            _ = builder.Services.AddTransient<EventListViewModel>();

            _ = builder.Services.AddTransient<FamilyDetailViewModel>();
            _ = builder.Services.AddTransient<FamilyListViewModel>();
            _ = builder.Services.AddTransient<FileInputHandlerViewModel>();
            _ = builder.Services.AddTransient<FirstRunViewModel>();

            _ = builder.Services.AddTransient<HubViewModel>();

            _ = builder.Services.AddTransient<MediaDetailViewModel>();
            _ = builder.Services.AddTransient<MediaListViewModel>();

            _ = builder.Services.AddTransient<NeedDatabaseReloadViewModel>();
            _ = builder.Services.AddTransient<NoteDetailViewModel>();
            _ = builder.Services.AddTransient<NoteListViewModel>();

            _ = builder.Services.AddTransient<PersonBirthdayViewModel>();
            _ = builder.Services.AddTransient<PersonDetailViewModel>();
            _ = builder.Services.AddTransient<PersonListViewModel>();
            _ = builder.Services.AddTransient<PersonNameDetailViewModel>();
            _ = builder.Services.AddTransient<PlaceDetailViewModel>();
            _ = builder.Services.AddTransient<PlaceListViewModel>();

            _ = builder.Services.AddTransient<RepositoryDetailViewModel>();
            _ = builder.Services.AddTransient<RepositoryRefDetailViewModel>();
            _ = builder.Services.AddTransient<RepositoryListViewModel>();

            _ = builder.Services.AddTransient<SearchPageViewModel>();
            _ = builder.Services.AddTransient<SettingsViewModel>();
            _ = builder.Services.AddTransient<SourceDetailViewModel>();
            _ = builder.Services.AddTransient<SourceListViewModel>();

            _ = builder.Services.AddTransient<TagDetailViewModel>();
            _ = builder.Services.AddTransient<TagListViewModel>();

            _ = builder.Services.AddTransient<FirstRunViewModel>();
            _ = builder.Services.AddTransient<WhatsNewViewModel>();

            //_ = builder.Services.AddTransient<SharedSharp.ViewModels.WhatsNewViewModel>();

            //   _ = builder.Services.AddTransient<NavigationPage>();

            return builder;
        }
    }
}