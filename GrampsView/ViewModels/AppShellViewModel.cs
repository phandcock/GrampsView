﻿// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Views;
using GrampsView.Views.StartupPages;

namespace GrampsView.ViewModels
{
    public class AppShellViewModel : ViewModelBase
    {
        public AppShellViewModel(SharedSharp.Logging.Interfaces.ILog iocCommonLogging)
             : base(iocCommonLogging)
        {
            RegisterMiscRoutes();

            RegisterDetailRoutes();

            TopMenuNoteCommand = new AsyncRelayCommand(TopMenuNoteCommandHandler);

            //Back_Command = new RelayCommand(Back);
        }

        public new IAsyncRelayCommand TopMenuNoteCommand
        {
            get; private set;
        }

        public async Task TopMenuNoteCommandHandler()
        {
            string body = string.Empty;

            List<string> recipients = new()
            {
                CommonLocalSettings.NoteEmailAddress
            };

            EmailMessage message = new()
            {
                Subject = $"GrampsView Note for ({CommonStatic.CurrentActiveModel.Id})",
                Body = body,
                To = recipients,
                //Cc = ccRecipients,
                //Bcc = bccRecipients
            };
            await Email.ComposeAsync(message);
        }

        private static void RegisterMiscRoutes()
        {
            Routing.RegisterRoute(nameof(FileInputHandlerPage), typeof(FileInputHandlerPage));
            Routing.RegisterRoute(nameof(FirstRunPage), typeof(FirstRunPage));

            Routing.RegisterRoute(nameof(HubPage), typeof(HubPage));

            Routing.RegisterRoute(nameof(NeedDatabaseReloadPage), typeof(NeedDatabaseReloadPage));

            Routing.RegisterRoute(nameof(WhatsNewPage), typeof(WhatsNewPage));
        }

        private void RegisterDetailRoutes()
        {
            Routing.RegisterRoute(nameof(AddressDetailPage), typeof(AddressDetailPage));
            Routing.RegisterRoute(nameof(AttributeDetailPage), typeof(AttributeDetailPage));

            Routing.RegisterRoute(nameof(ChildRefDetailPage), typeof(ChildRefDetailPage));
            Routing.RegisterRoute(nameof(CitationDetailPage), typeof(CitationDetailPage));

            Routing.RegisterRoute(nameof(DateSpanDetailPage), typeof(DateSpanDetailPage));
            Routing.RegisterRoute(nameof(DateRangeDetailPage), typeof(DateRangeDetailPage));
            Routing.RegisterRoute(nameof(DateStrDetailPage), typeof(DateStrDetailPage));
            Routing.RegisterRoute(nameof(DateValDetailPage), typeof(DateValDetailPage));

            Routing.RegisterRoute(nameof(EventDetailPage), typeof(EventDetailPage));

            Routing.RegisterRoute(nameof(FamilyDetailPage), typeof(FamilyDetailPage));

            Routing.RegisterRoute(nameof(MediaDetailPage), typeof(MediaDetailPage));

            Routing.RegisterRoute(nameof(NoteDetailPage), typeof(NoteDetailPage));

            Routing.RegisterRoute(nameof(PersonDetailPage), typeof(PersonDetailPage));
            Routing.RegisterRoute(nameof(PersonNameDetailPage), typeof(PersonNameDetailPage));
            Routing.RegisterRoute(nameof(PlaceDetailPage), typeof(PlaceDetailPage));

            Routing.RegisterRoute(nameof(RepositoryDetailPage), typeof(RepositoryDetailPage));
            Routing.RegisterRoute(nameof(RepositoryRefDetailPage), typeof(RepositoryRefDetailPage));

            Routing.RegisterRoute(nameof(SourceDetailPage), typeof(SourceDetailPage));

            Routing.RegisterRoute(nameof(TagDetailPage), typeof(TagDetailPage));
        }
    }
}