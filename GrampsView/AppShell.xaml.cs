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
        Routing.RegisterRoute(nameof(FirstRunPage), typeof(FirstRunPage));
        Routing.RegisterRoute(nameof(HubPage), typeof(HubPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(WhatsNewPage), typeof(WhatsNewPage));

        Routing.RegisterRoute(nameof(WhatsNewPage), typeof(WhatsNewPage));
    }
}