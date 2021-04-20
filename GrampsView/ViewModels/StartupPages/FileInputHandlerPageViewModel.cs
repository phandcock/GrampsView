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

            LoadSampleCommand = new AsyncCommand(() => LoadSample(), _ => LocalCanHandleSample);

            PickFileCommand = new AsyncCommand(() => PickFile(), _ => LocalCanHandleDataFolderChosen);
        }

        public IAsyncCommand LoadSampleCommand
        {
            get; private set;
        }

        public bool LocalCanHandleDataFolderChosen
        {
            get; set;
        } = true;

        public bool LocalCanHandleSample
        {
            get; set;
        } = true;

        public IAsyncCommand PickFileCommand
        {
            get; private set;
        }

        /// <summary>
        /// Called when navigation is performed to a page. You can use this method to load state if
        /// it is available.
        /// </summary>
        public override void BaseHandleAppearingEvent()
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

            BaseCL.Progress("Tell someone to load the file");

            // Remove the old dateTime stamps so the files get reloaded even if they have been seen before
            CommonLocalSettings.SetReloadDatabase();

            BaseEventAggregator.GetEvent<DataLoadStartEvent>().Publish();

            // await Xamarin.Forms.Shell.Current.Navigation.PopAsync();
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
                    BaseCL.Progress("Tell someone to load the file");

                    // Remove the old dateTime stamps so the files get reloaded even if they have
                    // been seen before
                    CommonLocalSettings.SetReloadDatabase();

                    BaseEventAggregator.GetEvent<DataLoadStartEvent>().Publish();

                    await DataStore.Instance.CN.DataLogEntryAdd("File picked").ConfigureAwait(false);

                    await CommonRoutines.NavigateBack();
                }
                else
                {
                    BaseCL.Progress("File picker error");
                    await DataStore.Instance.CN.NotifyAlert("No input file was selected");

                    // Allow another pick if required
                    LocalCanHandleDataFolderChosen = true;

                    BaseCurrentLayoutState = LayoutState.None;
                }
            }
            catch (Exception ex)
            {
                await DataStore.Instance.CN.NotifyException("Exception when using File Picker", ex);

                throw;
            }
        }
    }
}