// <copyright file="FirstRunDisplayService.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Services
{
    using GrampsView.Events;
    using GrampsView.Views;
    using Prism.Events;

    using Xamarin.Essentials;

    public class FirstRunDisplayService : IFirstRunDisplayService
    {
        public FirstRunDisplayService()
        {
        }

        /// <summary>
        /// Shows FirstRun pageif appropriate asynchronous.
        /// </summary>
        /// <param name="iocEventAggregator">
        /// </param>
        /// <returns>
        /// </returns>
        public bool ShowIfAppropriate(IEventAggregator iocEventAggregator)
        {
            if (iocEventAggregator is null)
            {
                return false;
            }

            if (VersionTracking.IsFirstLaunchEver) //  && !Common.CommonLocalSettings.FirstRunDisplay)
            {
                Common.CommonLocalSettings.FirstRunDisplay = true;
                iocEventAggregator.GetEvent<PageNavigateEvent>().Publish(nameof(FirstRunPage));

                return true;
            }

            return false;
        }
    }
}