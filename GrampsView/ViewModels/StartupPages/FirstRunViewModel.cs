using GrampsView.Common;

using SharedSharp.Common.Interfaces;

namespace GrampsView.ViewModels.StartupPages
{
    /// <summary>
    /// <c> First Run View Model </c>
    /// </summary>
    public class FirstRunViewModel : ViewModelBase
    {
        private readonly ISharedSharpAppInit _AppInit;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstRunViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common logger
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public FirstRunViewModel(SharedSharp.Logging.Interfaces.ILog iocCommonLogging, IMessenger iocEventAggregator, ISharedSharpAppInit iocAppInit)
            : base(iocCommonLogging)
        {
            LoadDataCommand = new AsyncRelayCommand(FirstRunLoadAFileButton);

            BaseTitle = "First Run";

            BaseTitleIcon = Constants.IconSettings;

            _AppInit = iocAppInit;
        }

        public AsyncRelayCommand LoadDataCommand
        {
            get;
        }

        /// <summary>Gramps export XML plus media.</summary>
        public async Task FirstRunLoadAFileButton()
        {
            _ = await Shell.Current.Navigation.PopModalAsync();

            await _AppInit.Init();
        }
    }
}