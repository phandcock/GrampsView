using SharedSharp.Common.Interfaces;
using SharedSharp.ViewModels;

using System.Diagnostics;

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

            // BaseTitleIcon = CommonConstants.IconSettings;

            Debug.WriteLine($"WhatsNewViewModel");
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

        public override void HandleViewModelParameters()
        {
            foreach (KeyValuePair<string, object> item in BasePassedArguments)
            {
                Debug.WriteLine($"BasePassedArguments - {item.Key}: {item.Value}");
            }

            if (BasePassedArguments.Count > 0)
            {
                WhatsNewText = (string)BasePassedArguments[SharedSharpConstants.ShellParameter1];
            }
        }

        private async Task LoadDataAction()
        {
            _ = SharedSharpNavigation.NavigateBack();

            await _AppInit.Init();
        }
    }
}