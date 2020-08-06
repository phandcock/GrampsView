//-----------------------------------------------------------------------
// <copyright file="SourceDetailViewModel.cs" company="MeMyselfAndI">
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

    using System.Diagnostics.Contracts;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// Defines the Source Detail Page View ViewModel.
    /// </summary>
    public class SourceDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// The local Source object.
        /// </summary>
        private SourceModel _SourcesObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// </param>
        /// <param name="iocEventAggregator">
        /// </param>
        /// <param name="iocNavigationService">
        /// </param>
        public SourceDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Source Detail";
            BaseTitleIcon = CommonConstants.IconSource;
        }

        /// <summary>
        /// Gets or sets the public Source ViewModel.
        /// </summary>
        /// <value>
        /// The source object.
        /// </value>
        public SourceModel SourceObject
        {
            get
            {
                return _SourcesObject;
            }

            set
            {
                SetProperty(ref _SourcesObject, value);
            }
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void PopulateViewModel()
        {
            // Cache the Source model
            SourceObject = DV.SourceDV.GetModelFromHLink(BaseNavParamsHLink);

            if (!(SourceObject is null))
            {
                // Get basic details
                BaseTitle = SourceObject.GetDefaultText;
                BaseTitleIcon = CommonConstants.IconSource;

                // Get media image
                HLinkMediaModel personImage = SourceObject.HomeImageHLink.ConvertToHLinkMediaModel();
                personImage.CardType = DisplayFormat.MediaCardLarge;
                Contract.Assert(SourceObject.HomeImageHLink != null, SourceObject.Id);
                BaseDetail.Add(personImage);

                // Header Card
                CardGroup t = new CardGroup { Title = "Header Details" };

                t.Add(new CardListLineCollection
                    {
                       new CardListLine("Card Type:", "Source Detail"),
                       new CardListLine("Title:", SourceObject.GSTitle),
                       new CardListLine("Author:", SourceObject.GSAuthor),
                       new CardListLine("Pub Info:", SourceObject.GSPubInfo),
                       new CardListLine("Abbrev:", SourceObject.GSAbbrev),
                    });

                // Add Model details
                t.Add(DV.SourceDV.GetModelInfoFormatted(SourceObject));

                BaseDetail.Add(t);

                // Add bulk items
                BaseDetail.Add(SourceObject.GMediaRefCollection.GetCardGroup());
                BaseDetail.Add(SourceObject.GNoteRefCollection.GetCardGroup());
                BaseDetail.Add(SourceObject.GTagRefCollection.GetCardGroup());
                BaseDetail.Add(SourceObject.GRepositoryRefCollection.GetCardGroup());
                BaseDetail.Add(SourceObject.GSourceAttributeCollection);

                BaseDetail.Add(SourceObject.BackHLinkReferenceCollection.GetCardGroup());
            }
        }
    }
}