// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.DBModels;
using GrampsView.ModelsDB.HLinks.Models;

namespace GrampsView.ViewModels.Family
{
    /// <summary>
    /// Family detail page view ViewModel.
    /// </summary>
    public class FamilyDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Holds the family object.
        /// </summary>
        private FamilyDBModel localFamilyModel = new();

        /// <summary>
        /// Gets or sets the Family object.
        /// </summary>
        /// <value>
        /// The family object.
        /// </value>
        public FamilyDBModel FamilyObject
        {
            get => localFamilyModel;

            set => SetProperty(ref localFamilyModel, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// Prism event aggregator.
        /// </param>
        [Obsolete]
        public FamilyDetailViewModel(ILog iocCommonLogging, IMessenger iocEventAggregator)
                                    : base(iocCommonLogging)
        {
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// Task.
        /// </returns>
        public override void HandleViewModelParameters()
        {
            if (base.NavigationParameter is not null && base.NavigationParameter.Valid)
            {
                HLinkFamilyDBModel HLinkObject = base.NavigationParameter as HLinkFamilyDBModel;

                FamilyObject = HLinkObject.DeRef;

                if (FamilyObject is not null)
                {
                    BaseDBModelBase = FamilyObject;
                    BaseTitleIcon = Constants.IconFamilies;

                    BaseDetail.Clear();

                    // Get basic details
                    BaseDetail.Add(new CardListLineCollection("Family Detail")
                    {
                    new CardListLine("Family Display Name:", FamilyObject.ToString()),
                    new CardListLine("Family Relationship:", FamilyObject.GFamilyRelationship),
                    new CardListLine("Father Name:", FamilyObject.GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.FullName),
                    new CardListLine("Mother Name:", FamilyObject.GMother.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.FullName),
                    new CardListLine("Date:",FamilyObject.GDate.LongDate),
                });

                    // Add Model details
                    BaseDetail.Add(DL.FamilyDL.GetModelInfoFormatted(FamilyObject));

                    // Add parent link
                    BaseDetail.Add(new HLinkFamilyDBModel
                    {
                        HLinkKey = localFamilyModel.HLinkKey,
                        DisplayAs = CommonEnums.DisplayFormat.LinkCardMedium
                    });
                }
            }
        }
    }
}