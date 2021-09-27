namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data;
    using GrampsView.Data.Repository;
    using GrampsView.Events;

    using Prism.Events;

    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.CommunityToolkit.UI.Views;

    /// <summary>
    /// View model for File Input Page.
    /// </summary>
    public partial class FileInputHandlerViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileInputHandlerViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        public FileInputHandlerViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "File Chooser";

            BaseTitleIcon = CommonConstants.IconSettings;

            LoadSampleCommand = new AsyncCommand(LoadSample);

            PickFileCommand = new AsyncCommand(PickFile);
        }

        public IAsyncCommand LoadSampleCommand
        {
            get; private set;
        }

        public IAsyncCommand PickFileCommand
        {
            get; private set;
        }

        /// <summary>
        /// Called when navigation is performed to a page. You can use this method to load state if
        /// it is available.
        /// </summary>
        public override void HandleViewAppearingEvent()
        {
            BaseCurrentLayoutState = LayoutState.None;
        }

        /// <summary>
        /// Loads the sample data.
        /// </summary>
        public async Task LoadSample()
        {
            BaseCL.Progress("Load sample data");

            CommonRoutines.ListEmbeddedResources();

            // Load Resource
            var assemblyExec = Assembly.GetExecutingAssembly();
            var resourceName = "GrampsView.AnythingElse.SampleData.EnglishTudorHouse.gpkg";

            DataStore.Instance.AD.CurrentInputStream = assemblyExec.GetManifestResourceStream(resourceName);

            DataStore.Instance.AD.CurrentInputStreamPath = "AnythingElse/Sample Data/EnglishTudorHouse.gpkg";

            await StartLoad();
        }

        /// <summary>
        /// Gramps export XML plus media.
        /// </summary>
        public async Task PickFile()
        {
            BaseCL.Progress("Calling folder picker");

            try
            {
                BaseCurrentLayoutState = LayoutState.Loading;

                DataStore.Instance.CN.DataLog.Clear();

                if (await StoreFileUtility.PickCurrentInputFile().ConfigureAwait(false))
                {
                    await StartLoad();
                }
                else
                {
                    BaseCL.Progress("File picker error");
                    DataStore.Instance.CN.NotifyAlert("No input file was selected");

                    BaseCurrentLayoutState = LayoutState.None;
                }
            }
            catch (Exception ex)
            {
                DataStore.Instance.CN.NotifyException("Exception when using File Picker", ex);

                throw;
            }
        }

        private async Task StartLoad()
        {
            BaseCL.Progress("Tell someone to load the file");

            // Remove the old dateTime stamps so the files get reloaded even if they have been seen before
            CommonLocalSettings.SetReloadDatabase();

            BaseEventAggregator.GetEvent<DataLoadStartEvent>().Publish();

            CommonRoutines.NavigateHub();
        }
    }
}