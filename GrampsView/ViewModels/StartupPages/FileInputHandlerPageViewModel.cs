﻿// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Repository;
using GrampsView.Data.StoreDB;
using GrampsView.Data.StoreFile;
using GrampsView.Events;

using SharedSharp.Errors.Interfaces;
using SharedSharp.Sizes;

namespace GrampsView.ViewModels.StartupPages
{
    /// <summary>
    /// View model for File Input Page.
    /// </summary>
    public partial class FileInputHandlerViewModel : ViewModelBase
    {
        /// <summary>Initializes a new instance of the <see cref="FileInputHandlerViewModel" /> class.</summary>
        /// <param name="iocCommonLogging">The common logging.</param>
        /// <param name="iocCardSizes"></param>
        public FileInputHandlerViewModel(ILog iocCommonLogging, ISharedCardSizes iocCardSizes)
            : base(iocCommonLogging)
        {
            CardSizes = iocCardSizes;

            BaseTitle = "File Chooser";

            BaseTitleIcon = Constants.IconSettings;

            LoadSampleCommand = new AsyncRelayCommand(LoadSample);

            PickFileCommand = new AsyncRelayCommand(PickFile);
        }

        public ISharedCardSizes CardSizes { get; }

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

            // Load Resource

            string resourceName = "EnglishTudorHouse.gpkg";

            DataStore.Instance.AD.CurrentInputStream = await FileSystem.Current.OpenAppPackageFileAsync(resourceName);

            DataStore.Instance.AD.CurrentInputStreamPath = "/EnglishTudorHouse.gpkg";

            await StartLoad();
        }

        /// <summary>
        /// Gramps export XML plus media.
        /// </summary>
        public async Task PickFile()
        {
            BaseCL.Progress("Calling file picker");

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
            }
        }

        private async Task StartLoad()
        {
            BaseCL.Progress("Tell someone to load the file");

            await Ioc.Default.GetRequiredService<IStoreDB>().Clear();

            // Remove the old dateTime stamps so the files get reloaded even if they have been seen before
            CommonLocalSettings.SetReloadDatabase();

            //Ioc.Default.GetRequiredService<IMessenger>().Send(new NavigationPopRootEvent(true));

            await Task.Delay(500);

            _ = Ioc.Default.GetRequiredService<IMessenger>().Send(new AppStartLoadDataEvent(true));
        }
    }
}