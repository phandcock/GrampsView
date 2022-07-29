namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using Microsoft.AppCenter.Distribute;
    using Microsoft.Toolkit.Mvvm.Messaging;

    using SharedSharp.Logging;
    using SharedSharp.Model;

    using System.Reflection;

    using Xamarin.Essentials;

    public class AboutViewModel : ViewModelBase
    {
        public AboutViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
                                                                    : base(iocCommonLogging)
        {
            BaseTitle = "About";
            BaseTitleIcon = Constants.IconAbout;
        }

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
        public string AppName
        {
            get
            {
                return AppInfo.Name;
            }
        }

        public string AttributionText { get; set; }

        public CardListLineCollection HeaderData
        {
            get
            {
                return DV.HeaderDV.HeaderDataModel.DetailAsCardListLineCollection;
            }
        }

        public string PrivacyPolicyText { get; set; }

        public string WhatsNewText { get; set; }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        public override async void HandleViewAppearingEvent()
        {
            // Assembly level stuff
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            AssemblyName assemblyName = new AssemblyName(assembly.FullName);

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

            WhatsNewText = CommonRoutines.LoadResource("GrampsView.CHANGELOG.md");

            AttributionText = CommonRoutines.LoadResource("GrampsView.Attribution.md");

            PrivacyPolicyText = CommonRoutines.LoadResource("GrampsView.PrivacyPolicy.md");

            return;
        }
    }
}