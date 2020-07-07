// <copyright file="FileInputHandlerPageViewModel.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using GrampsView.Events;
    using GrampsView.Views;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;

    using System;

    using Xamarin.Essentials;

    /// <summary>
    /// View model for File Input Page.
    /// </summary>
    public partial class FileInputHandlerViewModel : ViewModelBase
    {
        private bool _CanHandleDataFolderChosen = true;

        private bool _CanHandleUseExistingFolder = false;

        /// <summary>
        /// The local data detail list.
        /// </summary>
        private CardListLineCollection localDataDetailList = new CardListLineCollection();

        /// <summary>
        /// Initializes a new instance of the <see cref="FileInputHandlerViewModel"/> class.
        /// </summary>
        /// <param name="iocDataRepositoryManager">
        /// The ioc data repository manager.
        /// </param>
        /// <param name="iocCommonLogging">
        /// The common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        public FileInputHandlerViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "File Input Handler";

            BaseTitleIcon = CommonConstants.IconSettings;

            PickFileCommand = new DelegateCommand(PickFile).ObservesCanExecute(() => LocalCanHandleDataFolderChosen);

            UseExistingFolderCommand = new DelegateCommand(UseExistingFolder).ObservesCanExecute(() => LocalCanHandleUseExistingFolder);
        }

        /// <summary>
        /// Gets or sets the data detail list.
        /// </summary>
        /// <value>
        /// The data detail list.
        /// </value>
        // [RestorableState]
        public CardListLineCollection DataDetailList
        {
            get
            {
                return localDataDetailList;
            }

            set
            {
                SetProperty(ref localDataDetailList, value);
            }
        }

        public bool LocalCanHandleDataFolderChosen
        {
            get { return _CanHandleDataFolderChosen; }
            set { SetProperty(ref _CanHandleDataFolderChosen, value); }
        }

        public bool LocalCanHandleUseExistingFolder
        {
            get { return _CanHandleUseExistingFolder; }
            set { SetProperty(ref _CanHandleUseExistingFolder, value); }
        }

        public DelegateCommand PickFileCommand { get; private set; }

        public DelegateCommand UseExistingFolderCommand { get; private set; }

        /// <summary>
        /// Gramps export XML plus media.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public async void PickFile()
        {
            BaseCL.LogProgress("Calling folder picker");

            if (await StoreFileUtility.PickCurrentInputFile().ConfigureAwait(false))
            {
                BaseCL.LogProgress("Tell someone to load the file");

                // Remove the old dateTime stamps so the files get reloaded even if they have been
                // seen before
                CommonLocalSettings.SetReloadDatabase();

                BaseEventAggregator.GetEvent<DataLoadStartEvent>().Publish(false);

                BaseEventAggregator.GetEvent<PageNavigateEvent>().Publish(nameof(MessageLogPage));

                await DataStore.CN.ChangeLoadingMessage("File picked").ConfigureAwait(false);
            }
            else
            {
                BaseCL.LogProgress("File picker error");
                DataStore.CN.NotifyAlert("No input file was selected");

                // Allow another pick if required
                LocalCanHandleDataFolderChosen = true;
            }
        }

        /// <summary>
        /// Called when navigation is performed to a page. You can use this method to load state if
        /// it is available.
        /// </summary>
        public override void PopulateViewModel()
        {
            BaseEventAggregator.GetEvent<ProgressLoading>().Publish(null);

            if (DataStore.AD.CurrentDataFolderValid)
            {
                DataDetailList = new CardListLineCollection
                {
                    new CardListLine(
                        "Data Folder:",
                        DataStore.AD.CurrentDataFolder.FullName),
                };

                LocalCanHandleUseExistingFolder = true;
            }
            else
            {
                DataDetailList = new CardListLineCollection
                {
                    new CardListLine(
                        "Data Folder:",
                        "Not set"),
                };

                LocalCanHandleUseExistingFolder = false;
            }
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
        public void UseExistingFolder()
        {
            LocalCanHandleDataFolderChosen = false;

            if (DataStore.AD.CurrentDataFolderValid)
            {
                BaseCL.LogProgress("Tell someone to load the file");

                // Remove the old dateTime stamps so the files get reloaded even if they have been
                // seen before
                Preferences.Set(Common.CommonConstants.SettingsGPKGFileLastDateTimeModified, DateTime.MinValue);
                Preferences.Set(Common.CommonConstants.SettingsGPRAMPSFileLastDateTimeModified, DateTime.MinValue);
                Preferences.Set(Common.CommonConstants.SettingsXMLFileLastDateTimeModified, DateTime.MinValue);

                BaseEventAggregator.GetEvent<DataLoadStartEvent>().Publish(false);

                BaseEventAggregator.GetEvent<PageNavigateEvent>().Publish(nameof(MessageLogPage));
            }
        }
    }
}