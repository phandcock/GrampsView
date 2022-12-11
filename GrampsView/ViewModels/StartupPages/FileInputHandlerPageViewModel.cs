using GrampsView.Common;
using GrampsView.Data.External.StoreFile;
using GrampsView.Data.Repository;
using GrampsView.Events;

using SharedSharp.Common.Interfaces;
using SharedSharp.Errors.Interfaces;

using System.Reflection;

namespace GrampsView.ViewModels.StartupPages
{
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
        public FileInputHandlerViewModel(SharedSharp.Logging.Interfaces.ILog iocCommonLogging, IMessenger iocEventAggregator, ISharedSharpCardSizes iocCardSizes)
            : base(iocCommonLogging)
        {
            CardSizes = iocCardSizes;

            BaseTitle = "File Chooser";

            BaseTitleIcon = Constants.IconSettings;

            LoadSampleCommand = new AsyncRelayCommand(LoadSample);

            PickFileCommand = new AsyncRelayCommand(PickFile);
        }

        public ISharedSharpCardSizes CardSizes { get; }

        public IAsyncRelayCommand LoadSampleCommand
        {
            get; private set;
        }

        public IAsyncRelayCommand PickFileCommand
        {
            get; private set;
        }

        /// <summary>
        /// Loads the sample data.
        /// </summary>
        public async Task LoadSample()
        {
            BaseCL.Progress("Load sample data");

            CommonRoutines.ListEmbeddedResources();

            // Load Resource
            Assembly assemblyExec = Assembly.GetExecutingAssembly();
            string resourceName = "GrampsView.AnythingElse.SampleData.EnglishTudorHouse.gpkg";

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
                Ioc.Default.GetRequiredService<ILog>().DataLogEntryReplace("");

                if (await StoreFileUtility.PickCurrentInputFile().ConfigureAwait(false))
                {
                    await StartLoad();
                }
                else
                {
                    BaseCL.Progress("File picker error");
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyAlert("No input file was selected");
                }
            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("Exception when using File Picker", ex);

                throw;
            }
        }

        private async Task StartLoad()
        {
            BaseCL.Progress("Tell someone to load the file");

            // Remove the old dateTime stamps so the files get reloaded even if they have been seen before
            CommonLocalSettings.SetReloadDatabase();

            SharedSharp.Common.SharedSharpNavigation.NavigateHub();

            await Task.Delay(500);

            _ = Ioc.Default.GetRequiredService<IMessenger>().Send(new DataLoadStartEvent(true));
        }
    }
}