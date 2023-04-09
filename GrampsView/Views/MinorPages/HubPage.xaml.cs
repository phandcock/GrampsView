// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Events;
using GrampsView.ViewModels.MinorPages;

namespace GrampsView.Views
{
    public sealed partial class HubPage : ViewBasePage
    {
        public HubPage()
        {
            InitializeComponent();
            BindingContext = Ioc.Default.GetRequiredService<HubViewModel>();

            Ioc.Default.GetRequiredService<IMessenger>().Register<NavigationPushEvent>(this, (r, m) =>
            {
                Navigation.PushAsync(m.Value);
            });

            Ioc.Default.GetRequiredService<IMessenger>().Register<NavigationPopRootEvent>(this, (r, m) =>
            {
                Navigation.PopToRootAsync();
            });
        }
    }
}