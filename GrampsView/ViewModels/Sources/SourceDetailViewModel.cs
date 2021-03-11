namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

    using System;

    /// <summary>
    /// Defines the Source Detail Page View ViewModel.
    /// </summary>
    public class SourceDetailViewModel : ViewModelBase
    {
        private ItemGlyph _MediaCard = new ItemGlyph();

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
        public SourceDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Source Detail";
            BaseTitleIcon = CommonConstants.IconSource;
        }

        public ItemGlyph MediaCard
        {
            get
            {
                return _MediaCard;
            }
            set
            {
                SetProperty(ref _MediaCard, value);
            }
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
        public override void BaseHandleAppearingEvent()
        {
            HLinkSourceModel HLinkObject = CommonRoutines.DeserialiseObject<HLinkSourceModel>(Uri.UnescapeDataString(BaseParamsHLink));

            // Cache the Source model
            SourceObject = HLinkObject.DeRef;

            if (!(SourceObject is null))
            {
                // Get basic details
                BaseTitle = SourceObject.GetDefaultText;
                BaseTitleIcon = CommonConstants.IconSource;

                MediaCard = SourceObject.ModelItemGlyph;

                // Header Card
                BaseDetail.Add(new CardListLineCollection("Source Detail")
                    {
                       new CardListLine("Title:", SourceObject.GSTitle),
                       new CardListLine("Author:", SourceObject.GSAuthor),
                       new CardListLine("Pub Info:", SourceObject.GSPubInfo),
                       new CardListLine("Abbrev:", SourceObject.GSAbbrev),
                    });

                // Add Model details
                BaseDetail.Add(DV.SourceDV.GetModelInfoFormatted(SourceObject));
            }
        }
    }
}