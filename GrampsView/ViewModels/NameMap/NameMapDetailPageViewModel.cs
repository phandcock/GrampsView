//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="NameMapDetailPageViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;
    using Prism.Navigation;

    /// <summary>
    /// Defines the EVent Detail Page View ViewModel.
    /// </summary>
    public class NameMapDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// The local book mark object.
        /// </summary>
        private NameMapModel localNameMapObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="NameMapDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocNameMapDataView">
        /// The ioc book mark data view.
        /// </param>
        /// <param name="iocLoggingService">
        /// The ioc logging service.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        /// <param name="iocCommonModelGridBuilder">
        /// The ioc common model grid builder.
        /// </param>
        public NameMapDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="NameMapDetailViewModel" /> class.
        ///// </summary>
        //public NameMapDetailViewModel()
        //{
        //}

        /// <summary>
        /// Gets or sets the public Event ViewModel.
        /// </summary>
        // [RestorableState]
        public NameMapModel NameMapObject
        {
            get
            {
                return localNameMapObject;
            }

            set
            {
                SetProperty(ref localNameMapObject, value);
            }
        }

        /// <summary>
        /// Handles navigation in wards and sets up the event model parameter.
        /// </summary>
        /// <param name="e">
        /// The <see cref="NavigatedToEventArgs"/> instance containing the event data.
        /// </param>
        /// <param name="viewModelState">
        /// The parameter is not used.
        /// </param>
        public override void PopulateViewModel()
        {
            // base.OnNavigatedTo(INavigationParameters parameters);
            // BaseEventAggregator.GetEvent<PageTitleChangedEvent>().Publish(new
            // PageTitleChangedEventArgs { PageTitle = "NameMap Detail", PageIcon =
            // CommonConstants.IconNameMaps });

            // cache the Note model TODO NameMapObject = DV.NameMapDV.GetModel(parameters as string);

            // Get basic details
            CardGroup t = new CardGroup { Title = "Header Details" };

            t.Add(new CardListLineCollection
            {
                new CardListLine("Card Type:", "Name Map Detail"),
                new CardListLine("Private:", NameMapObject.PrivAsString),
            });

            // Add Model details
            t.Add(DV.NameMapDV.GetModelInfoFormatted(NameMapObject));

            BaseHeader.Add(t);

            // BackHLinkRefNavArgument = localNavigationHelper.HLinkModelCollectionAdd(NoteObject.BackHLinkReferenceCollection);
            BaseBackLinks.Add(NameMapObject.BackHLinkReferenceCollection.GetCardGroup());
        }
    }
}