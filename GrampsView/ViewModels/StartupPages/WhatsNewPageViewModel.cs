using GrampsView.Common;

using SharedSharp.Common.Interfaces;
using SharedSharp.ViewModels;

namespace GrampsView.ViewModels.StartupPages
{
    /// <summary>
    /// View model for Whats New Page.
    /// </summary>
    public partial class WhatsNewViewModel : SharedSharpViewModelBase
    {
        private readonly ISharedSharpAppInit _AppInit;

        /// <summary>Initializes a new instance of the <see cref="WhatsNewViewModel" /> class.</summary>
        /// <param name="iocAppInit">Initialisation Code</param>
        public WhatsNewViewModel(ISharedSharpAppInit iocAppInit)
        {
            _AppInit = iocAppInit;

            LoadDataCommand = new AsyncRelayCommand(LoadDataAction);

            BaseTitle = "What's new";

            BaseTitleIcon = Constants.IconSettings;

            WhatsNewText = Task.Run(async () => await CommonRoutines.LoadResource("Reading\\CHANGELOG.md")).Result;
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
        public string WhatsNewText { get; set; } = "";

        private async Task LoadDataAction()
        {
            await SharedSharpNavigation.NavigateBack();

            await _AppInit.Init();
        }
    }
}