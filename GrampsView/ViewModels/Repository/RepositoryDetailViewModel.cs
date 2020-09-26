//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="RepositoryDetailViewModel.cs" company="MeMyselfAndI">
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
    public class RepositoryDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// The local book mark object.
        /// </summary>
        private RepositoryModel _RepositoryObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// </param>
        public RepositoryDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
        }

        /// <summary>
        /// Gets or sets the repository object.
        /// </summary>
        /// <value>
        /// The repository object.
        /// </value>
        public RepositoryModel RepositoryObject
        {
            get
            {
                return _RepositoryObject;
            }

            set
            {
                SetProperty(ref _RepositoryObject, value);
            }
        }

        /// <summary>
        /// Handles navigation inwards and sets up the repository model parameter.
        /// </summary>
        public override void PopulateViewModel()
        {
            RepositoryObject = DV.RepositoryDV.GetModelFromHLink(BaseNavParamsHLink);

            // Trigger refresh of View fields via INotifyPropertyChanged
            RaisePropertyChanged(string.Empty);

            if (!(RepositoryObject == null))
            {
                BaseTitle = RepositoryObject.GetDefaultText;
                BaseTitleIcon = CommonConstants.IconRepository;

                // Get basic details
                CardGroup t = new CardGroup { Title = "Header Details" };

                t.Add(new CardListLineCollection
                    {
                        new CardListLine("Card Type:", "Repository Detail"),
                        new CardListLine("Name:", RepositoryObject.GRName),
                        new CardListLine("Type:", RepositoryObject.GType),
                    });

                t.Add(DV.RepositoryDV.GetModelInfoFormatted(RepositoryObject));

                BaseDetail.Add(t);

                // Add details
                BaseDetail.Add(RepositoryObject.GNoteRefCollection.GetCardGroup());
                BaseDetail.Add(RepositoryObject.GTagRefCollection.GetCardGroup());
                BaseDetail.Add(RepositoryObject.GAddress.GetCardGroup());
                BaseDetail.Add(RepositoryObject.GURL);
                BaseDetail.Add(RepositoryObject.BackHLinkReferenceCollection.GetCardGroup());
            }
        }
    }
}