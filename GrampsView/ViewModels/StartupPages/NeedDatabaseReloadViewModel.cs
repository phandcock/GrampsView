namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using Microsoft.Toolkit.Mvvm.Messaging;

    /// <summary>
    /// <c> viewmodel </c> for the About <c> Flyout </c>.
    /// </summary>
    public class NeedDatabaseReloadViewModel : ViewModelBase
    {
        private IStartAppLoad _StartAppLoad;

        /// <summary>
        /// Initializes a new instance of the <see cref="NeedDatabaseReloadViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Injected common loggeing.
        /// </param>
        /// <param name="iocEventAggregator">
        /// Injected event aggregator.
        /// </param>
        public NeedDatabaseReloadViewModel(ICommonLogging iocCommonLogging, IMessenger iocEventAggregator, IStartAppLoad iocStartAppLoad)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Database reload needed";

            BaseTitleIcon = CommonConstants.IconSettings;

            LoadDataCommand = new DelegateCommand(LoadDataAction);

            _StartAppLoad = iocStartAppLoad;
        }

        public DelegateCommand LoadDataCommand
        {
            get; private set;
        }

        public async void LoadDataAction()
        {
            await Xamarin.Forms.Shell.Current.Navigation.PopModalAsync();

            _StartAppLoad.StartProcessing();
        }
    }
}