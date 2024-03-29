﻿// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;

namespace GrampsView.ViewModels.Repository
{
    /// <summary>
    /// Defines the EVent Detail Page View ViewModel.
    /// </summary>
    public class RepositoryDetailViewModel : ViewModelBase
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
        [Obsolete]
        public RepositoryDetailViewModel(ILog iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging)
        {
        }

        public HLinkRepositoryModel RepositoryHLink
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
        public override void HandleViewModelParameters()
        {
            if (base.NavigationParameter is not null && base.NavigationParameter.Valid)
            {
                RepositoryHLink = base.NavigationParameter as HLinkRepositoryModel;

                RepositoryObject = RepositoryHLink.DeRef;

                if (!(RepositoryObject == null))
                {
                    BaseModelBase = RepositoryObject;
                    BaseTitleIcon = Constants.IconRepository;

                    BaseDetail.Clear();

                    BaseDetail.Add(new CardListLineCollection("Repository Detail")
                    {
                        new CardListLine("Name:", RepositoryObject.GRName),
                        new CardListLine("Type:", RepositoryObject.GType),
                    });

                    BaseDetail.Add(DV.RepositoryDV.GetModelInfoFormatted(RepositoryObject));
                }
            }
        }
    }
}