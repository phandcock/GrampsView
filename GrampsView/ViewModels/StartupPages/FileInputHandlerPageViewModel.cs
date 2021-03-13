namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data;
    using GrampsView.Data.Model;
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
        /// The local data detail list.
        /// </summary>
        private readonly CardListLineCollection _DataDetailList = new CardListLineCollection();

        private bool _CanHandleDataFolderChosen = true;

        private bool _LocalCanHandleSample = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileInputHandlerViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// Prism Navigation Service
        /// </param>
        public FileInputHandlerViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "File Input Handler";

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
            get
            {
                return _CanHandleDataFolderChosen;
            }
            set
            {
                SetProperty(ref _CanHandleDataFolderChosen, value);
            }
        }

        public bool LocalCanHandleSample
        {
            get
            {
                return _LocalCanHandleSample;
            }
            set
            {
                SetProperty(ref _LocalCanHandleSample, value);
            }
        }

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
            base.BaseCurrentState = LayoutState.None;
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
        public async Task PickFile()
        {
            BaseCL.Progress("Calling folder picker");

            try
            {
                base.BaseCurrentState = LayoutState.Loading;

                if (await StoreFileUtility.PickCurrentInputFile().ConfigureAwait(false))
                {
                    BaseCL.Progress("Tell someone to load the file");

                    // Remove the old dateTime stamps so the files get reloaded even if they have
                    // been seen before
                    CommonLocalSettings.SetReloadDatabase();

                    BaseEventAggregator.GetEvent<DataLoadStartEvent>().Publish();

                    await DataStore.Instance.CN.DataLogEntryAdd("File picked").ConfigureAwait(false);
                }
                else
                {
                    BaseCL.Progress("File picker error");
                    DataStore.Instance.CN.NotifyAlert("No input file was selected");

                    // Allow another pick if required
                    LocalCanHandleDataFolderChosen = true;

                    base.BaseCurrentState = LayoutState.None;
                }
            }
            catch (Exception ex)
            {
                DataStore.Instance.CN.NotifyException("Exception when using File Picker", ex);

                throw;
            }
        }
    }
}