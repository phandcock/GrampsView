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


        /// <summary>Initializes a new instance of the <see cref="WhatsNewViewModel" /> class.</summary>
        /// <param name="iocCommonLogging">The ioc common logging.</param>
        /// <param name="iocEventAggregator">The ioc event aggregator.</param>
        /// <param name="iocAppInit">The ioc application initialize.</param>
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

        public async Task HandleViewAppearingEvent()
        {
            WhatsNewText = await CommonRoutines.LoadResource("GrampsView.CHANGELOG.md");
        }

        public async Task LoadDataAction()
        {
            _ = await Shell.Current.Navigation.PopModalAsync();

            await _AppInit.Init();
        }
    }
}