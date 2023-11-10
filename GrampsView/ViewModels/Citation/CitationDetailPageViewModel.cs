// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.DBModels;
using GrampsView.Models.Collections.HLinks;
using GrampsView.ModelsDB.HLinks.Models;

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

        public CitationDBModel CitationObject
        {
            get; set;
        } = new CitationDBModel();

        public HLinkNoteDBModel HighlightedNote
        {
            get; set;
        } = new HLinkNoteDBModel();

        public HLinkEventDBModel HLinkObject
        {
            get; set;
        } = new HLinkEventDBModel();

        public HLinkNoteDBModelCollection NotesWithoutHighlight
        {
            get; set;
        } = new HLinkNoteDBModelCollection();

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
                HLinkCitationDBModel HLinkObject = base.NavigationParameter as HLinkCitationDBModel;

                CitationObject = HLinkObject.DeRef;

                if (CitationObject is not null)
                {
                    BaseDBModelBase = CitationObject;
                    BaseTitleIcon = Constants.IconCitation;

                    BaseDetail.Clear();

                    BaseDetail.Add(new CardListLineCollection("Citation Detail")
                {
                    new CardListLine("Page:", CitationObject.GPage),
                    new CardListLine("Confidence:", CitationObject.GConfidence.ToString())
                });

                    // Get date card
                    BaseDetail.Add(CitationObject.GDateContent.AsHLink("Event Date"));

                    BaseDetail.Add(DL.CitationDL.GetModelInfoFormatted(CitationObject));

                    // If event note, display it while showing the full list further below.
                    HighlightedNote = CitationObject.GNoteRefCollection.GetFirstOfType(Constants.NoteTypeCitation);

                    NotesWithoutHighlight = CitationObject.GNoteRefCollection.GetCollectionWithoutOne(HighlightedNote);

                    HLinkCitationDBModel t = CitationObject.HLink;
                    t.DisplayAs = CommonEnums.DisplayFormat.LinkCardMedium;
                    BaseDetail.Add(t);
                }
            }
        }
    }
}