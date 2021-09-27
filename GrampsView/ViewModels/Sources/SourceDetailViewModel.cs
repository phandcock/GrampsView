namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

    /// <summary>
    /// Defines the Source Detail Page View ViewModel.
    /// </summary>
    public class SourceDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourceDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// </param>
        /// <param name="iocEventAggregator">
        /// </param>
        public SourceDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
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
            get; set;
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void HandleViewDataLoadEvent()
        {
            HLinkSourceModel HLinkObject = CommonRoutines.GetHLinkParameter<HLinkSourceModel>((BaseParamsHLink));

            // Cache the Source model
            SourceObject = HLinkObject.DeRef;

            if (!(SourceObject is null))
            {
                // Get basic details
                BaseModelBase = SourceObject;

                // MediaCard = SourceObject.ModelItemGlyph;

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