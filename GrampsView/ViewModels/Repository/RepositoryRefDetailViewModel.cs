﻿namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

    /// <summary>
    /// Defines the EVent Detail Page View ViewModel.
    /// </summary>
    public class RepositoryRefDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public RepositoryRefDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
        }

        public HLinkRepositoryRefModel RepositoryHLink
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the repository object.
        /// </summary>
        /// <value>
        /// The repository object.
        /// </value>
        public RepositoryModel RepositoryObject
        {
            get; set;
        }

        /// <summary>
        /// Handles navigation inwards and sets up the repository model parameter.
        /// </summary>
        public override void BaseHandleLoadEvent()
        {
            RepositoryHLink = CommonRoutines.GetHLinkParameter<HLinkRepositoryRefModel>(BaseParamsHLink);

            RepositoryObject = RepositoryHLink.DeRef;

            if (!(RepositoryObject == null))
            {
                BaseTitle = RepositoryObject.GetDefaultText;
                BaseTitleIcon = CommonConstants.IconRepository;

                BaseDetail.Add(new CardListLineCollection("Call Details")
                    {
                        new CardListLine("Call No:", RepositoryHLink.GCallNo),
                        new CardListLine("Medium:", RepositoryHLink.GMedium),
                    });

                BaseDetail.Add(RepositoryHLink.DeRef.HLink);

                BaseDetail.Add(DV.RepositoryDV.GetModelInfoFormatted(RepositoryObject));
            }
        }
    }
}