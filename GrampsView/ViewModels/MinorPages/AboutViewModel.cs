using GrampsView.Common;
using GrampsView.Data.DataView;

using Microsoft.AppCenter.Distribute;

using SharedSharp.Common.Interfaces;
using SharedSharp.Model;

using System.Reflection;

namespace GrampsView.ViewModels.MinorPages
{
    public class AboutViewModel : ViewModelBase
    {
        [Obsolete]
        public AboutViewModel(ILog iocCommonLogging, IMessenger iocEventAggregator)
                                                                    : base(iocCommonLogging)
        {
            BaseTitle = "About";
            BaseTitleIcon = Constants.IconAbout;
        }

        public CardListLineCollection ApplicationStateList
        {
            get;
        }

            = new CardListLineCollection();

        public CardListLineCollection ApplicationVersionList
        {
            get;
        }

        = new CardListLineCollection();

        /// <summary>
        /// Gets the application version list.
        /// </summary>
        /// <value>
        /// The application version list.
        /// </value>
        public string AppName => AppInfo.Name;

        public string AttributionText { get; set; }

        public CardListLineCollection HeaderData => DV.HeaderDV.HeaderDataModel.DetailAsCardListLineCollection;

        public string PrivacyPolicyText { get; set; }

        public string WhatsNewText { get; set; }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        public async Task HandleViewAppearingEvent()
        {
            // Assembly level stuff
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            AssemblyName assemblyName = new(assembly.FullName);

            ISharedSharpSizes MySizes = Ioc.Default.GetRequiredService<ISharedSharpSizes>();
            ISharedSharpCardSizes MyCardSizes = Ioc.Default.GetRequiredService<ISharedSharpCardSizes>();

            ApplicationVersionList.Clear();

            ApplicationVersionList.AddRange(new CardListLineCollection
                {
                new CardListLine("Application Name", AppInfo.Name),

                new CardListLine("Package Name", AppInfo.PackageName),

                new CardListLine("First Launch Ever?", VersionTracking.IsFirstLaunchEver),

                new CardListLine("First Launch Current Version?", VersionTracking.IsFirstLaunchForCurrentVersion),

                new CardListLine("First Launch Current Build?", VersionTracking.IsFirstLaunchForCurrentBuild),

                new CardListLine("Current Version", VersionTracking.CurrentVersion),

                new CardListLine("Current Build", VersionTracking.CurrentBuild),

                new CardListLine("Previous Version", VersionTracking.PreviousVersion),

                new CardListLine("Previous Build", VersionTracking.PreviousBuild),

                new CardListLine("First Version Installed", VersionTracking.FirstInstalledVersion),

                new CardListLine("First Build Installed", VersionTracking.FirstInstalledBuild),

                new CardListLine("Major Version", assemblyName.Version.Major),

                new CardListLine("Minor Version", assemblyName.Version.Minor),

                new CardListLine("Major Revision", assemblyName.Version.MajorRevision),

                new CardListLine("Middle Revision", assemblyName.Version.Revision),

                new CardListLine("Minor Revision", assemblyName.Version.MinorRevision),

                new CardListLine("App Center Update Track",  Distribute.UpdateTrack.ToString()),
            });

            ApplicationVersionList.Title = "Application Versions";

            ///////////////////////////////
            ///
            ApplicationStateList.Clear();

            ApplicationStateList.AddRange(new CardListLineCollection
                {
                new CardListLine("Display Class", MySizes.CurrentDisplayClass.ToString()),

                new CardListLine("Window Size", MySizes.WindowSize.ToString()),

                new CardListLine("Screen Size", MySizes.ScreenSize.ToString()),

                new CardListLine("Orientation", MySizes.CurrentOrientation.ToString()),

                new CardListLine("Idiom", DeviceInfo.Idiom.ToString()),

                new CardListLine("CardSize Small Width", MyCardSizes.CardSmallWidth.ToString()),

                new CardListLine("CardSize Number Columns", MyCardSizes.CardsAcrossColumns.ToString()),
            });

            ApplicationStateList.Title = "Application State";

            /////////////////////////////////////////

            WhatsNewText = await CommonRoutines.LoadResource("CHANGELOG.md");

            AttributionText = await CommonRoutines.LoadResource("Attribution.md");

            PrivacyPolicyText = await CommonRoutines.LoadResource("PrivacyPolicy.md");

            return;
        }
    }
}