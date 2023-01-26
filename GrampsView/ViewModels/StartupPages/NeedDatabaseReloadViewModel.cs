// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;

using SharedSharp.Common.Interfaces;

namespace GrampsView.ViewModels.StartupPages
{
    /// <summary>
    /// <c> viewmodel </c> for the About <c> Flyout </c>.
    /// </summary>
    public class NeedDatabaseReloadViewModel : ViewModelBase
    {
        private readonly ISharedSharpAppInit _AppInit;

        /// <summary>
        /// Initializes a new instance of the <see cref="NeedDatabaseReloadViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Injected common loggeing.
        /// </param>
        /// <param name="iocEventAggregator">
        /// Injected event aggregator.
        /// </param>
        public NeedDatabaseReloadViewModel(SharedSharp.Logging.Interfaces.ILog iocCommonLogging, IMessenger iocEventAggregator, ISharedSharpAppInit iocAppInit)
            : base(iocCommonLogging)
        {
            BaseTitle = "Database reload needed";

            BaseTitleIcon = Constants.IconSettings;

            LoadDataCommand = new AsyncRelayCommand(LoadDataAction);

            _AppInit = iocAppInit;
        }

        public AsyncRelayCommand LoadDataCommand
        {
            get; private set;
        }

        public async Task LoadDataAction()
        {
            await SharedSharpNavigation.NavigateBack();

            await _AppInit.Init();
        }
    }
}