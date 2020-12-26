//-----------------------------------------------------------------------
//
// View model for the fly-out page view
//
// <copyright file="AboutViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Microsoft.AppCenter.Distribute;

    using Prism.Events;

    using System.Reflection;

    using Xamarin.Essentials;

    public class AboutViewModel : ViewModelBase
    {
        private CardListLineCollection _ApplicationVersionList = new CardListLineCollection();

        //private HLinkHeaderModel _HeaderModel = new HLinkHeaderModel();

        public AboutViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
                                            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "About";
            BaseTitleIcon = CommonConstants.IconAbout;
        }

        /// <summary>
        /// Gets the application version list.
        /// </summary>
        /// <value>
        /// The application version list.
        /// </value>

        public CardListLineCollection ApplicationVersionList
        {
            get
            {
                return _ApplicationVersionList;
            }
        }

        public string AppName
        {
            get
            {
                return AppInfo.Name;
            }
        }

        public CardListLineCollection HeaderData
        {
            get
            {
                return DV.HeaderDV.HeaderDataModel.DetailAsCardListLineCollection;
            }
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        public override void PopulateViewModel()
        {
            // Assembly level stuff
            var assembly = GetType().GetTypeInfo().Assembly;
            var assemblyName = new AssemblyName(assembly.FullName);

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

            return;
        }
    }
}