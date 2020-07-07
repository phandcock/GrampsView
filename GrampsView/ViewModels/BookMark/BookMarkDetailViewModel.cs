//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="BookMarkDetailViewModel.cs" company="MeMyselfAndI">
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
    public class BookMarkDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// The local book mark object.
        /// </summary>
        private BookMarkModel localBookMarkObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookMarkDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocCommonModelGridBuilder">
        /// The ioc common model grid builder.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public BookMarkDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
        }

        /// <summary>
        /// Gets or sets the public Event ViewModel.
        /// </summary>
        // [RestorableState]
        public BookMarkModel BookMarkObject
        {
            get
            {
                return localBookMarkObject;
            }

            set
            {
                SetProperty(ref localBookMarkObject, value);
            }
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void PopulateViewModel()
        {
            BookMarkObject = DV.BookMarkDV.GetModelFromHLinkString(BaseNavParamsHLink.HLinkKey);

            if (BookMarkObject != null)
            {
                BaseTitle = BookMarkObject.GetDefaultText;
                BaseTitleIcon = CommonConstants.IconBookMark;

                // Get basic details
                CardGroup t = new CardGroup { Title = "Header Details" };

                t.Cards.Add(new CardListLineCollection
                    {
                        new CardListLine("Card Type:", "BookMark Detail"),
                        new CardListLine("Private:", BookMarkObject.PrivAsString),
                        new CardListLine("Target:", BookMarkObject.GTarget),
                    });

                // Add Model details
                t.Cards.Add(DV.BookMarkDV.GetModelInfoFormatted(BookMarkObject));

                BaseHeader.Add(t);

                BaseBackLinks.Add(BookMarkObject.BackHLinkReferenceCollection.GetCardGroup());
            }
        }
    }
}