namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using Microsoft.Toolkit.Mvvm.Messaging;

    /// <summary>
    /// <c> First Run View Model </c>
    /// </summary>
    public class FirstRunViewModel : ViewModelBase
    {
        private IStartAppLoad _StartAppLoad;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstRunViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common logger
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public FirstRunViewModel(ICommonLogging iocCommonLogging, IMessenger iocEventAggregator, IStartAppLoad iocStartAppLoad)
            : base(iocCommonLogging, iocEventAggregator)
        {
            LoadDataCommand = new DelegateCommand(FirstRunLoadAFileButton);

            BaseTitle = "First Run";

            BaseTitleIcon = CommonConstants.IconSettings;

            _StartAppLoad = iocStartAppLoad;
        }

        public DelegateCommand LoadDataCommand
        {
            get;
        }

        /// <summary>
        /// Gramps export XML plus media.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public async void FirstRunLoadAFileButton()
        {
            await Xamarin.Forms.Shell.Current.Navigation.PopModalAsync();

            _StartAppLoad.StartProcessing();
        }
    }
}