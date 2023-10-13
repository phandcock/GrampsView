// Copyright (c) phandcock.  All rights reserved.

using GrampsView.ViewModels.MinorPages;

namespace GrampsView.Views
{
    public sealed partial class HubPage : ViewBasePage
    {
        public HubPage()
        {
            InitializeComponent();
            BindingContext = Ioc.Default.GetRequiredService<HubViewModel>();

            //Ioc.Default.GetRequiredService<IMessenger>().Register<NavigationPushEvent>(this, async (r, m) =>
            //{
            //    try
            //    {
            //        IReadOnlyList<Page> t = Navigation.NavigationStack;

            //        if (MainThread.IsMainThread)
            //        {
            //            await Navigation.PushAsync(m.Value);
            //        }
            //        else
            //        {
            //            await MainThread.InvokeOnMainThreadAsync(async () =>
            //            {
            //                await Navigation.PushAsync(m.Value);
            //            });
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        ErrorInfo t = new("Load Clipped Media")
            //        {
            //            new CardListLine("Page", m.Value.ToString())
            //        };

            //        Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, t);
            //    }

            //    //    await SharedSharpNavigation.NavigateAsyncNS(m.Value);
            //});

            //Ioc.Default.GetRequiredService<IMessenger>().Register<NavigationPopRootEvent>(this, async (r, m) =>
            //{
            //    IReadOnlyList<Page> t = Navigation.NavigationStack;

            //    if (MainThread.IsMainThread)
            //    {
            //        await Navigation.PopToRootAsync();
            //    }
            //    else
            //    {
            //        await MainThread.InvokeOnMainThreadAsync(async () =>
            //        {
            //            await Application.Current.MainPage.Navigation.PopToRootAsync();
            //        });
            //    }
            //    //  await SharedSharpNavigation.NavigateHubNS();
            //});
        }
    }
}