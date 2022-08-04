namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using CommunityToolkit.Mvvm.Messaging;

    using SharedSharp.Logging;

    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// <c> viewmodel </c> for the About <c> Flyout </c>.
    /// </summary>
    public class NeedDatabaseReloadViewModel : ViewModelBase
    {
        private IAppInit _AppInit;

        /// <summary>
        /// Initializes a new instance of the <see cref="NeedDatabaseReloadViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Injected common loggeing.
        /// </param>
        /// <param name="iocEventAggregator">
        /// Injected event aggregator.
        /// </param>
        public NeedDatabaseReloadViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator, IAppInit iocAppInit)
            : base(iocCommonLogging)
        {
            BaseTitle = "Database reload needed";

            BaseTitleIcon = Constants.IconSettings;

            LoadDataCommand = new AsyncCommand(LoadDataAction);

            _AppInit = iocAppInit;
        }

        public AsyncCommand LoadDataCommand
        {
            get; private set;
        }

        public async Task LoadDataAction()
        {
            await Xamarin.Forms.Shell.Current.Navigation.PopModalAsync();

            await _AppInit.Init();
        }
    }
}