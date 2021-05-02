namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using Prism.Commands;
    using Prism.Events;

    /// <summary>
    /// View model for WHats New Page.
    /// </summary>
    public partial class WhatsNewViewModel : ViewModelBase
    {
        private string _WhatsNewText = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupStorageViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        public WhatsNewViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            LoadDataCommand = new DelegateCommand(LoadDataAction);

            BaseTitle = "What's new";

            BaseTitleIcon = CommonConstants.IconSettings;
        }

        public DelegateCommand LoadDataCommand
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets the whats new text.
        /// </summary>
        /// <value>
        /// Whats New text
        /// </value>
        public string WhatsNewText
        {
            get
            {
                return _WhatsNewText;
            }

            set
            {
                SetProperty(ref _WhatsNewText, value);
            }
        }

        public async void LoadDataAction()
        {
            await Xamarin.Forms.Shell.Current.Navigation.PopModalAsync();

            StartAppLoad.StartProcessing();
        }
    }
}