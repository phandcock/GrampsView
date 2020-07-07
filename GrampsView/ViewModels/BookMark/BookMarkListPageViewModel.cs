//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="BookMarkListPageViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using Prism.Events;
    using Prism.Navigation;

    /// <summary>
    /// View Model for the Event Section Page.
    /// </summary>
    public class BookMarkListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookMarkListViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// Prism Navigation Service.
        /// </param>
        public BookMarkListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "BookMark List";
            BaseTitleIcon = CommonConstants.IconBookMark;
        }

        public CardGroup BookMarkSource
        {
            get
            {
                CardGroup t = new CardGroup();
                t.Add(DV.BookMarkCollection.GetCardGroup());
                return t;
            }
        }
    }
}