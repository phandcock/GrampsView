//-----------------------------------------------------------------------
//
// View model for the fly-out page view
//
// <copyright file="NeedDatabaseReloadViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using Prism.Events;
    using Prism.Navigation;

    /// <summary>
    /// <c>viewmodel</c> for the About <c>Flyout</c>.
    /// </summary>
    public class NeedDatabaseReloadViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NeedDatabaseReloadViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Injected common loggeing.
        /// </param>
        /// <param name="iocEventAggregator">
        /// Injected event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// </param>
        public NeedDatabaseReloadViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Database reload needed";

            BaseTitleIcon = CommonConstants.IconSettings;
        }
    }
}