namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using Microsoft.Toolkit.Mvvm.Messaging;

    using SharedSharp.Logging;

    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// View model for WHats New Page.
    /// </summary>
    public partial class WhatsNewViewModel : ViewModelBase
    {
        private IAppInit _AppInit;

        private string _WhatsNewText = string.Empty;

        public AsyncCommand LoadDataCommand
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

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupStorageViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        public WhatsNewViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator, IAppInit iocAppInit)
            : base(iocCommonLogging, iocEventAggregator)
        {
            LoadDataCommand = new AsyncCommand(LoadDataAction);

            BaseTitle = "What's new";

            BaseTitleIcon = CommonConstants.IconSettings;

            _AppInit = iocAppInit;
        }

        public async Task LoadDataAction()
        {
            await Xamarin.Forms.Shell.Current.Navigation.PopModalAsync();

            await _AppInit.Init();
        }
    }
}