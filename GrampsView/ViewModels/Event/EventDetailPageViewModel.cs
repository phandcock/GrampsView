namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Microsoft.Toolkit.Mvvm.Messaging;

    using SharedSharp.Logging;
    using SharedSharp.Model;

    using System.ComponentModel;

    /// <summary>
    /// Defines the Event Detail Page View ViewModel.
    /// </summary>

    public class EventDetailViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common Logging interface.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>

        /// <summary>
        /// Gets or sets the public Event ViewModel.
        /// </summary>
        /// <value>
        /// The current event ViewModel.
        /// </value>
        public EventModel EventObject
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

        public EventDetailViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
                                                                    : base(iocCommonLogging, iocEventAggregator)
        {
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        public override void HandleViewDataLoadEvent()
        {
            HLinkObject = CommonRoutines.GetHLinkParameter<HLinkEventModel>(BaseParamsHLink);

            if (!(HLinkObject is null) && HLinkObject.Valid)
            {
                EventObject = HLinkObject.DeRef;

                if (!(EventObject is null) && EventObject.Valid)
                {
                    BaseModelBase = EventObject;
                    BaseTitleIcon = CommonConstants.IconEvents;

                    // Get basic details
                    BaseDetail.Add(new CardListLineCollection("Event Detail")
                    {
                        new CardListLine("Type:", EventObject.GType),
                        new CardListLine("Role",HLinkObject.GRole),
                        new CardListLine("Years ago", EventObject.GDate.GetAge),
                        new CardListLine("Description", EventObject.GDescription),
                    });

                    // Get date card
                    BaseDetail.Add(EventObject.GDate.AsHLink("Event Date"));

                    BaseDetail.Add(EventObject.GPlace);

                    // Add Model details
                    BaseDetail.Add(DV.EventDV.GetModelInfoFormatted(EventObject));

                    // If event note, display it while showing the full list further below.
                    HighlightedNote = EventObject.GNoteRefCollection.GetFirstOfType(CommonConstants.NoteTypeEvent);

                    NotesWithoutHighlight = EventObject.GNoteRefCollection.GetCollectionWithoutOne(HighlightedNote);
                }
            }
        }
    }
}