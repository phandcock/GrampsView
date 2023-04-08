// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels;

namespace GrampsView.ViewModels.Citation
{
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
        public CitationDetailViewModel(ILog iocCommonLogging)
            : base(iocCommonLogging)
        {
        }

        public CitationModel CitationObject
        {
            get; set;
        } = new CitationModel();

        public HLinkNoteModel HighlightedNote
        {
            get; set;
        } = new HLinkNoteModel();

        public HLinkEventModel HLinkObject
        {
            get; set;
        } = new HLinkEventModel();

        public HLinkNoteModelCollection NotesWithoutHighlight
        {
            get; set;
        } = new HLinkNoteModelCollection();

        /// <summary>
        /// Gets or sets the citation object.
        /// </summary>
        /// <value>
        /// The citation object.
        /// </value>
        public override void HandleViewModelParameters()
        {
            if (base.NavigationParameter is not null && base.NavigationParameter.Valid)
            {
                HLinkCitationModel HLinkObject = base.NavigationParameter as HLinkCitationModel;

                CitationObject = HLinkObject.DeRef;

                if (CitationObject is not null)
                {
                    BaseModelBase = CitationObject;
                    BaseTitleIcon = Constants.IconCitation;

                    BaseDetail.Clear();

                    BaseDetail.Add(new CardListLineCollection("Citation Detail")
                {
                    new CardListLine("Page:", CitationObject.GPage),
                    new CardListLine("Confidence:", CitationObject.GConfidence.ToString())
                });

                    // Get date card
                    BaseDetail.Add(CitationObject.GDateContent.AsHLink("Event Date"));

                    BaseDetail.Add(DV.CitationDV.GetModelInfoFormatted(CitationObject));

                    // If event note, display it while showing the full list further below.
                    HighlightedNote = CitationObject.GNoteRefCollection.GetFirstOfType(Constants.NoteTypeCitation);

                    NotesWithoutHighlight = CitationObject.GNoteRefCollection.GetCollectionWithoutOne(HighlightedNote);

                    HLinkCitationModel t = CitationObject.HLink;
                    t.DisplayAs = CommonEnums.DisplayFormat.LinkCardMedium;
                    BaseDetail.Add(t);
                }
            }
        }
    }
}