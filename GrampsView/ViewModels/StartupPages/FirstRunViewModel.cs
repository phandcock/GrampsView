namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using Prism.Events;

    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

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
            LoadDataCommand = new AsyncCommand(() => FirstRunLoadAFileButton());

            BaseTitle = "First Run";

            BaseTitleIcon = CommonConstants.IconSettings;
        }

        public IAsyncCommand LoadDataCommand
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
        public async Task FirstRunLoadAFileButton()
        {
            await Xamarin.Forms.Shell.Current.Navigation.PopModalAsync();

            await StartAppLoad.StartProcessing();
        }
    }
}