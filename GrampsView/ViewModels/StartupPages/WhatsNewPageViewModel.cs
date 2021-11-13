namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using Microsoft.Toolkit.Mvvm.Messaging;

    /// <summary>
    /// View model for WHats New Page.
    /// </summary>
    public partial class WhatsNewViewModel : ViewModelBase
    {
        private IStartAppLoad _StartAppLoad;
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
        public WhatsNewViewModel(ICommonLogging iocCommonLogging, IMessenger iocEventAggregator, IStartAppLoad iocStartAppLoad)
            : base(iocCommonLogging, iocEventAggregator)
        {
            LoadDataCommand = new DelegateCommand(LoadDataAction);

            BaseTitle = "What's new";

            BaseTitleIcon = CommonConstants.IconSettings;

            _StartAppLoad = iocStartAppLoad;
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

            await _StartAppLoad.StartProcessing();
        }
    }
}