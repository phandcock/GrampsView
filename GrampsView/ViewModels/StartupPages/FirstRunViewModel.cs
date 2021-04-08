namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using Prism.Commands;
    using Prism.Events;

    /// <summary>
    /// <c>First Run View Model</c>
    /// </summary>
    public class FirstRunViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirstRunViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common logger
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public FirstRunViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            LoadDataCommand = new DelegateCommand(FirstRunLoadAFileButton);

            BaseTitle = "First Run";

            BaseTitleIcon = CommonConstants.IconSettings;
        }

        public DelegateCommand LoadDataCommand
        {
            get; private set;
        }

        /// <summary>
        /// Raises the <see cref="avigatedTo"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="NavigatedToEventArgs"/> instance containing the event data.
        /// </param>
        /// <param name="viewModelState">
        /// State of the view ViewModel.
        /// </param>
        public override void BaseHandleAppearingEvent()
        {
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
            await AppShell.Current.Navigation.PopModalAsync();

            Common.StartAppLoad.StartProcessing();
        }
    }
}