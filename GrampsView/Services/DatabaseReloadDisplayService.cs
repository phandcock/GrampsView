// <copyright file="DatabaseReloadDisplayService.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Services
{
    using GrampsView.Common;
    using GrampsView.Events;
    using GrampsView.Views;

    using Prism.Events;

    // For instructions on testing this service see https://github.com/Microsoft/WindowsTemplateStudio/tree/master/docs/features/whats-new-prompt.md
    public class DatabaseReloadDisplayService : IDatabaseReloadDisplayService
    {
        public DatabaseReloadDisplayService()
        {
        }

        /// <summary>
        /// Shows database reload view if appropriate.
        /// </summary>
        /// <returns>
        /// if the view is displayed.
        /// </returns>
        public bool ShowIfAppropriate(IEventAggregator iocEventAggregator)
        {
            if (iocEventAggregator is null)
            {
                return false;
            }
            if (CommonLocalSettings.DatabaseReloadNeeded)
            {
                iocEventAggregator.GetEvent<PageNavigateEvent>().Publish(nameof(NeedDatabaseReloadPage));

                return true;
            }

            return false;
        }
    }
}