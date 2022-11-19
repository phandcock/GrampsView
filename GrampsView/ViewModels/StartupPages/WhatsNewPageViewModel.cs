using GrampsView.Common;
using GrampsView.Common.Interfaces;

namespace GrampsView.ViewModels.StartupPages
{
    /// <summary>
    /// View model for WHats New Page.
    /// </summary>
    public partial class WhatsNewViewModel : ViewModelBase
    {
        private readonly IAppInit _AppInit;

        private readonly string _WhatsNewText = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupStorageViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        public WhatsNewViewModel(SharedSharp.Logging.Interfaces.ILog iocCommonLogging, IMessenger iocEventAggregator, IAppInit iocAppInit)
            : base(iocCommonLogging)
        {
            LoadDataCommand = new AsyncRelayCommand(LoadDataAction);

            BaseTitle = "What's new";

            BaseTitleIcon = Constants.IconSettings;

            _AppInit = iocAppInit;
        }

        public AsyncRelayCommand LoadDataCommand
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets the whats new text.
        /// </summary>
        /// <value>
        /// Whats New text
        /// </value>
        public string WhatsNewText { get; set; }

        public override void HandleViewAppearingEvent()
        {
            WhatsNewText = CommonRoutines.LoadResource("GrampsView.CHANGELOG.md");
        }

        public async Task LoadDataAction()
        {
            _ = await Shell.Current.Navigation.PopModalAsync();

            await _AppInit.Init();
        }
    }
}