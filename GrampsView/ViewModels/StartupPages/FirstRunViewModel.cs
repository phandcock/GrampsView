//-----------------------------------------------------------------------
//
// View model for the fly-out page view
//
// <copyright file="FirstRunViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Events;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;

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
        /// <param name="iocNavigationService">
        /// </param>
        public FirstRunViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            LoadDataCommand = new DelegateCommand(FirstRunLoadAFileButton);

            BaseTitle = "First Run";

            BaseTitleIcon = CommonConstants.IconSettings;
        }

        public DelegateCommand LoadDataCommand { get; private set; }

        /// <summary>
        /// Gramps export XML plus media.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public void FirstRunLoadAFileButton()
        {
            BaseEventAggregator.GetEvent<AppStartWhatsNewEvent>().Publish();
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
        public void LoadSampleFileButton()
        {
            // TODO add the sample button back

            //var uri = new Uri("ms-appx:///AnythingElse/SampleData/EnglishTudorHouse.gpkg");
            //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);

            //CommonLocalSettings.FileDataInput = file;

            //BaseEventAggregator.GetEvent<DataLoadStartEvent>().Publish(true);

            ////// Navigate to the Hubpage
            //// localNavigationService.NavigateAsync(nameof(Views.MessageLogPage));
        }

        /// <summary>
        /// Raises the <see cref="avigatedTo"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="NavigatedToEventArgs"/> instance containing the event data.
        /// </param>
        /// <param name="viewModelState">
        /// State of the view ViewModel.
        /// </param>
        public override void PopulateViewModel()
        {
        }
    }
}