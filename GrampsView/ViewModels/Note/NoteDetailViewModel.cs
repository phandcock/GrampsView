namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

    using System;

    /// <summary>
    /// Defines the EVent Detail Page View ViewModel.
    /// </summary>
    public class NoteDetailViewModel : ViewModelBase
    {
        private INoteModel _NoteObject = new NoteModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteDetailViewModel"/> class. Common logging.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// Common Event Aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// </param>
        public NoteDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitleIcon = CommonConstants.IconNotes;
        }

        /// <summary>
        /// Gets or sets the public Event ViewModel.
        /// </summary>
        /// <value>
        /// The current event ViewModel.
        /// </value>
        public INoteModel NoteObject
        {
            get
            {
                return _NoteObject;
            }

            set
            {
                SetProperty(ref _NoteObject, value);
            }
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        public override void BaseHandleAppearingEvent()
        {
            HLinkNoteModel HLinkObject = CommonRoutines.DeserialiseObject<HLinkNoteModel>(Uri.UnescapeDataString(BaseParamsHLink));

            if (!(HLinkObject is null) && (HLinkObject.Valid))
            {
                NoteObject = HLinkObject.DeRef;

                // Trigger refresh of View fields via INotifyPropertyChanged
                RaisePropertyChanged(string.Empty);

                BaseTitle = NoteObject.GetDefaultText;

                // Get basic details
                BaseDetail.Add(new CardListLineCollection("Note Detail")
                {
                    new CardListLine("Type:", NoteObject.GType),
                    new CardListLine("Formatted:", NoteObject.GIsFormated),
                });

                // Add Model details
                BaseDetail.Add(DV.NoteDV.GetModelInfoFormatted((NoteModel)NoteObject));
            }
        }
    }
}