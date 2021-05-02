﻿namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using Prism.Commands;
    using Prism.Events;

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
        public NeedDatabaseReloadViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Database reload needed";

            BaseTitleIcon = CommonConstants.IconSettings;

            LoadDataCommand = new DelegateCommand(LoadDataAction);
        }

        public DelegateCommand LoadDataCommand
        {
            get; private set;
        }

        public async void LoadDataAction()
        {
            await Xamarin.Forms.Shell.Current.Navigation.PopModalAsync();

            StartAppLoad.StartProcessing();
        }
    }
}