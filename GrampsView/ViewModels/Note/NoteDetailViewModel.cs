namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

    using System;

    using static GrampsView.Common.CommonEnums;

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
        public NoteDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitleIcon = CommonConstants.IconNotes;
        }

        public INoteModel NoteObject
        {
            get; set;
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        public override void BaseHandleLoadEvent()
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
                if (NoteObject.GType == CommonConstants.NoteTypeLink)
                {
                    URLModel newLinkURL = new URLModel
                    {
                        GDescription = NoteObject.GetDefaultText,
                        URLType = URIType.URL,
                        GHRef = new Uri(NoteObject.TextShort)
                    };

                    BaseDetail.Add(newLinkURL);
                }
            }
        }
    }
}