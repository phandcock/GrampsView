namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Microsoft.Toolkit.Mvvm.Messaging;

    using SharedSharp.Logging;
    using SharedSharp.Model;

    /// <summary>
    /// Defines the Citation Detail Page View ViewModel.
    /// </summary>
    public class CitationDetailViewModel : ViewModelBase
    {
        public CitationModel CitationObject
        {
            get; set;
        }

        public HLinkNoteModel HighlightedNote
        {
            get; set;
        } = new HLinkNoteModel();

        public HLinkEventModel HLinkObject
        {
            get; set;
        }

        public HLinkNoteModelCollection NotesWithoutHighlight
        {
            get; set;
        } = new HLinkNoteModelCollection();

        /// <summary>
        /// Initializes a new instance of the <see cref="CitationDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public CitationDetailViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
        }

        /// <summary>
        /// Gets or sets the citation object.
        /// </summary>
        /// <value>
        /// The citation object.
        /// </value>
        public override void HandleViewDataLoadEvent()
        {
            // Handle HLinkKeys
            HLinkCitationModel HLinkCitation = CommonRoutines.GetHLinkParameter<HLinkCitationModel>(BaseParamsHLink);

            CitationObject = HLinkCitation.DeRef;

            if (CitationObject != null)
            {
                BaseModelBase = CitationObject;
                BaseTitleIcon = CommonConstants.IconCitation;

                BaseDetail.Add(new CardListLineCollection("Citation Detail")
                {
                    new CardListLine("Page:", CitationObject.GPage),
                    new CardListLine("Confidence:", CitationObject.GConfidence.ToString())
                });

                // Get date card
                BaseDetail.Add(CitationObject.GDateContent.AsHLink("Event Date"));

                BaseDetail.Add(DV.CitationDV.GetModelInfoFormatted(CitationObject));

                // If event note, display it while showing the full list further below.
                HighlightedNote = CitationObject.GNoteRefCollection.GetFirstOfType(CommonConstants.NoteTypeCitation);

                NotesWithoutHighlight = CitationObject.GNoteRefCollection.GetCollectionWithoutOne(HighlightedNote);

                CitationObject.GSourceRef.DisplayAs = CommonEnums.DisplayFormat.LargeCard;
                BaseDetail.Add(CitationObject.GSourceRef);
            }
        }
    }
}