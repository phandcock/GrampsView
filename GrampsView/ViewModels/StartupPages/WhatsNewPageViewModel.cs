using SharedSharp.ViewModels;

using System.Diagnostics;
using System.Windows.Input;

namespace GrampsView.ViewModels.StartupPages
{
    /// <summary>
    /// View model for Whats New Page.
    /// </summary>
    public partial class WhatsNewViewModel : SharedSharpViewModelBase
    {
        /// <summary>Initializes a new instance of the <see cref="WhatsNewViewModel" /> class.</summary>
        public WhatsNewViewModel()
        {
            LoadDataCommand = new Command(LoadDataAction);

            BaseTitle = "What's new";

            // BaseTitleIcon = CommonConstants.IconSettings;

            Debug.WriteLine($"WhatsNewViewModel");
        }

        public ICommand LoadDataCommand
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

        private void LoadDataAction()
        {
            _ = SharedSharpNavigation.NavigateBack();
        }
    }
}