// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Views;
using GrampsView.Views.StartupPages;

namespace GrampsView;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
        Routing.RegisterRoute(nameof(BookMarkListPage), typeof(BookMarkListPage));
        Routing.RegisterRoute(nameof(CitationListPage), typeof(CitationListPage));
        Routing.RegisterRoute(nameof(EventListPage), typeof(EventListPage));
        Routing.RegisterRoute(nameof(FamilyListPage), typeof(FamilyListPage));
        Routing.RegisterRoute(nameof(FirstRunPage), typeof(FirstRunPage));
        Routing.RegisterRoute(nameof(HubPage), typeof(HubPage));
        Routing.RegisterRoute(nameof(MediaListPage), typeof(MediaListPage));
        Routing.RegisterRoute(nameof(NoteListPage), typeof(NoteListPage));
        Routing.RegisterRoute(nameof(PersonBirthdayPage), typeof(PersonBirthdayPage));
        Routing.RegisterRoute(nameof(PlaceListPage), typeof(PlaceListPage));
        Routing.RegisterRoute(nameof(RepositoryListPage), typeof(RepositoryListPage));
        Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(SourceListPage), typeof(SourceListPage));
        Routing.RegisterRoute(nameof(TagListPage), typeof(TagListPage));
        Routing.RegisterRoute(nameof(WhatsNewPage), typeof(WhatsNewPage));
        Routing.RegisterRoute(nameof(WhatsNewPage), typeof(WhatsNewPage));
    }
}