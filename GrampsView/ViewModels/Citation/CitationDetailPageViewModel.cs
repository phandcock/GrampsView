namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

    /// <summary>
    /// Defines the Citation Detail Page View ViewModel.
    /// </summary>
    public class CitationDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitationDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public CitationDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
        }

        public CitationModel CitationObject
        {
            get; set;
        }

        public HLinkNoteModel HLinkNote
        {
            get; set;
        } = new HLinkNoteModel();

        /// <summary>
        /// Gets or sets the citation object.
        /// </summary>
        /// <value>
        /// The citation object.
        /// </value>
        public override void BaseHandleLoadEvent()
        {
            // Handle HLinkKeys
            HLinkCitationModel HLinkCitation = CommonRoutines.GetHLinkParameter<HLinkCitationModel>(BaseParamsHLink);

            CitationObject = HLinkCitation.DeRef;

            if (CitationObject != null)
            {
                BaseTitle = CitationObject.GetDefaultTextShort;
                BaseTitleIcon = CommonConstants.IconCitation;

                BaseDetail.Add(new CardListLineCollection("Citation Detail")
                {
                    new CardListLine("Page:", CitationObject.GPage),
                    new CardListLine("Confidence:", CitationObject.GConfidence.ToString())
                });

                // Get date card
                BaseDetail.Add(CitationObject.GDateContent.AsHLink("Event Date"));

                BaseDetail.Add(DV.CitationDV.GetModelInfoFormatted(CitationObject));

                // If at least one note, display it while showing the full list further below.
                if (CitationObject.GNoteRefCollection.Count > 0)
                {
                    HLinkNote = CitationObject.GNoteRefCollection[0];
                }

                CitationObject.GSourceRef.DisplayAs = CommonEnums.DisplayFormat.LargeCard;
                BaseDetail.Add(CitationObject.GSourceRef);
            }
        }
    }
}