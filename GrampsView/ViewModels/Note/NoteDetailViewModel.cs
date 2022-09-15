namespace GrampsView.ViewModels
{
    using CommunityToolkit.Mvvm.Messaging;

    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using SharedSharp.Logging;
    using SharedSharp.Model;

    using System;

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
        public NoteDetailViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
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
        public override void HandleViewDataLoadEvent()
        {
            HLinkNoteModel HLinkObject = CommonRoutines.GetHLinkParameter<HLinkNoteModel>((BaseParamsHLink));

            if (!(HLinkObject is null) && HLinkObject.Valid)
            {
                NoteObject = HLinkObject.DeRef;

                BaseModelBase = NoteObject;

                // Get basic details
                BaseDetail.Add(new CardListLineCollection("Note Detail")
                {
                    new CardListLine("Type:", NoteObject.GType),
                    new CardListLine("Formatted:", NoteObject.GIsFormated),
                });

                // Add Model details
                BaseDetail.Add(DV.NoteDV.GetModelInfoFormatted((NoteModel)NoteObject));

                // Handle Link Note types
                if (NoteObject.GType == Constants.NoteTypeLink)
                {
                    URLModel newLinkURL = new URLModel
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