// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.StoreDB;
using GrampsView.Events;
using GrampsView.Views;
using GrampsView.Views.StartupPages;

using SharedSharp.Common.Interfaces;
using SharedSharp.Errors.Interfaces;
using SharedSharp.Services.Interfaces;

namespace GrampsView.Common
{
    public class AppInit : ISharedSharpAppInit
    {
        public async Task<string> GetChangesText()
        {
            return await CommonRoutines.LoadResource("Reading\\CHANGELOG.md");
        }

        public async Task Init()
        {
            try
            {
                // Need FirstRun?
                if (Ioc.Default.GetRequiredService<IFirstRunDisplayService>().ShowIfAppropriate())
                {
                    Ioc.Default.GetRequiredService<IMessenger>().Send(new NavigationPushEvent(new FirstRunPage()));

                    return;
                }

                // Need WhatsNew?
                if (Ioc.Default.GetRequiredService<IWhatsNewDisplayService>().ShowIfAppropriate())
                {
                    Ioc.Default.GetRequiredService<IMessenger>().Send(new NavigationPushEvent(new WhatsNewPage()));

                    return;
                }

                if (await Ioc.Default.GetRequiredService<IDatabaseReloadDisplayService>().ShowIfAppropriate())
                {
                    Ioc.Default.GetRequiredService<IMessenger>().Send(new NavigationPushEvent(new NeedDatabaseReloadPage()));

                    SharedSharpSettings.DataSerialised = false;

                    return;
                }

                // Load da data
                await LoadData().ConfigureAwait(false);

                //// Action startup parameters
                //if (App.Current.StartUpParameterUri != null)
                //{
                //    HyperLinkCard parameterURICard = new HyperLinkCard();
                //    parameterURICard.Create();
                //    parameterURICard.Title = DateTime.UtcNow.ToString();
                //    parameterURICard.theLink = App.Current.StartUpParameterUri;

                //    await Ioc.Default.GetRequiredService<IDataStore>().ItemAddShareTargetAsync(parameterURICard);
                //}
            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("AppInit.Init", ex);
            }
        }

        private async Task LoadData()
        {
            try
            {
                await Task.Run(async () =>
                  {
                      await Ioc.Default.GetRequiredService<IStoreDB>().OpenOrCreate();
                  });


                if (SharedSharpSettings.DataSerialised)
                {
                    // Ioc.Default.GetRequiredService<IDataRepositoryManager>().StartDataLoad();
                    _ = Ioc.Default.GetRequiredService<IMessenger>().Send(new AppStartLoadDataEvent(true));
                    return;
                }

                // No Serialised Data and made it this far so some problem has occurred. Load
                // everything from the beginning.
                Ioc.Default.GetRequiredService<IMessenger>().Send(new NavigationPushEvent(new FileInputHandlerPage()));
            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("AppInit.LoadData", ex);
            }
        }
    }
}