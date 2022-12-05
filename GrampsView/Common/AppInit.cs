using CommunityToolkit.Mvvm.Messaging;

using GrampsView.Events;
using GrampsView.Views;

using Microsoft.Extensions.DependencyInjection;

using SharedSharp.Common;
using SharedSharp.Common.Interfaces;
using SharedSharp.Errors.Interfaces;
using SharedSharp.Services.Interfaces;

using System;
using System.Threading.Tasks;

namespace GrampsView.Common
{
    public class AppInit : ISharedSharpAppInit
    {
        public Task<string> GetChangesText()
        {
            return Task.FromResult(CommonRoutines.LoadResource("GrampsView.CHANGELOG.md"));
        }

        public async Task Init()
        {
            try
            {
                // First run?
                if (await Ioc.Default.GetRequiredService<IFirstRunDisplayService>().ShowIfAppropriate(nameof(FirstRunPage)))
                {
                    return;
                }

                // Need WhatNew?
                if (await Ioc.Default.GetRequiredService<IWhatsNewDisplayService>().ShowIfAppropriate(nameof(SharedSharp.Views.WhatsNewPage)))
                {
                    return;
                }

                if (await Ioc.Default.GetRequiredService<IDatabaseReloadDisplayService>().ShowIfAppropriate(nameof(NeedDatabaseReloadPage)))
                {
                    SharedSharpSettings.DataSerialised = false;

                    return;
                }

                // Setup Event Handling
                //Ioc.Default.GetRequiredService<IMessenger>().Register<SSharpMessageWindowSizeChanged>(this, (r, m) =>
                //{
                //    if (m.Value == null)
                //        return;

                //    SharedSharpSizes.WindowSize = m.Value;
                //    SharedSharpCardSizes.Current.ReCalculateCardWidths();
                //});

                //Ioc.Default.GetRequiredService<IMessenger>().Register<SSharpMessageOrientationChange>(this, (r, m) =>
                //{
                //    if (m == null)
                //        return;

                //    SharedSharpCardSizes.Current.ReCalculateCardWidths();
                //});

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
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("AppInit.Init",ex,null);

                throw;
            }
        }

        private async Task LoadData()
        {
            try
            {
                if (SharedSharpSettings.DataSerialised)
                {
                    // Ioc.Default.GetRequiredService<IDataRepositoryManager>().StartDataLoad();
                    _ = Ioc.Default.GetRequiredService<IMessenger>().Send(new DataLoadStartEvent(true));
                    return;
                }

                // No Serialised Data and made it this far so some problem has occurred. Load
                // everything from the beginning.
                await SharedSharpNavigation.NavigateAsync(nameof(FileInputHandlerPage));
            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("AppInit.LoadData",ex,null);

                throw;
            }
        }
    }
}