// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Minor;

namespace GrampsView.ViewModels.Note
{
    public class NoteDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteDetailViewModel"/> class. Common logging.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// Common Event Aggregator.
        /// </param>
        [Obsolete]
        public NoteDetailViewModel(ILog iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging)
        {
            BaseTitleIcon = Constants.IconNotes;
        }

        public INoteModel NoteObject
        {
            get; set;
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        public override void HandleViewModelParameters()
        {
            if (base.NavigationParameter is not null && base.NavigationParameter.Valid)
            {
                HLinkNoteModel HLinkObject = base.NavigationParameter as HLinkNoteModel;

                NoteObject = HLinkObject.DeRef;

                BaseModelBase = NoteObject;

                // Get basic details
                BaseDetail.Add(new CardListLineCollection("Note Detail")
                {
                    new CardListLine("Type:", NoteObject.GType),
                    new CardListLine("Formatted:", NoteObject.GIsFormated),
                });

                BaseDetail.Clear();

                // Add Model details
                BaseDetail.Add(DV.NoteDV.GetModelInfoFormatted((NoteModel)NoteObject));

                // Handle Link Note types
                if (NoteObject.GType == Constants.NoteTypeLink)
                {
                    URLModel newLinkURL = new()
                    {
                        GDescription = NoteObject.ToString(),
                        GHRef = new Uri(NoteObject.GStyledText.TextShort),
                        // ModelItemGlyph = NoteObject.ModelItemGlyph,
                    };

                    BaseDetail.Add(newLinkURL.HLink);
                }
            }
        }
    }
}