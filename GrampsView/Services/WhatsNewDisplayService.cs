// <copyright file="WhatsNewDisplayService.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Services
{
    using GrampsView.Events;
    using GrampsView.Views;

    using Prism.Events;

    using Xamarin.Essentials;

    public class WhatsNewDisplayService : IWhatsNewDisplayService
    {
        public WhatsNewDisplayService()
        {
        }

        public bool ShowIfAppropriate(IEventAggregator iocEventAggregator)
        {
            if (iocEventAggregator is null)
            {
                return false;
            }

            // VersionTracking.IsFirstLaunchForCurrentBuild returns true every time called when
            // first run. If this service is called multiple times then it will need the flag.
            if ((VersionTracking.IsFirstLaunchForCurrentBuild)) // && (!Common.CommonLocalSettings.WhatsNewDisplayed))
            {
                // Common.CommonLocalSettings.WhatsNewDisplayed = true;
                iocEventAggregator.GetEvent<PageNavigateEvent>().Publish(nameof(WhatsNewPage));

                return true;
            }

            return false;
        }
    }
}