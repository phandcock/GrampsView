// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels;
using GrampsView.Models.HLinks;
using GrampsView.Models.HLinks.Models;

using System.ComponentModel;

namespace GrampsView.ViewModels.Event
{
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

        public EventDetailViewModel(ILog iocCommonLogging, IMessenger iocEventAggregator)
                                                                            : base(iocCommonLogging)
        {
        }

        public HLinkBase EventDate { get; set; } = new HLinkDateModelStr();

        /// <summary>
        /// Gets or sets the public Event ViewModel.
        /// </summary>
        /// <value>
        /// The current event ViewModel.
        /// </value>
        public EventModel EventObject
        {
            get; set;
        } = new EventModel();

        public CardListLineCollection ExtraDetails { get; set; } = new CardListLineCollection();

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

        public HLinkPlaceModel PlaceLocation { get; set; } = new HLinkPlaceModel();

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        public override void HandleViewModelParameters()
        {
            HLinkObject = CommonRoutines.GetHLinkParameter<HLinkEventModel>(HLinkSerial);

            if (HLinkObject is not null && HLinkObject.Valid)
            {
                EventObject = HLinkObject.DeRef;

                if (EventObject is not null && EventObject.Valid)
                {
                    BaseModelBase = EventObject;
                    BaseTitleIcon = Constants.IconEvents;

                    // Get basic details
                    ExtraDetails = new CardListLineCollection("Event Detail")
                    {
                        new CardListLine("Type:", EventObject.GType),
                        new CardListLine("Role",HLinkObject.GRole),
                        new CardListLine("Years ago", EventObject.GDate.GetAge),
                        new CardListLine("Description", EventObject.GDescription),
                    };

                    // Get date card
                    EventDate = EventObject.GDate.AsHLink("Event Date");

                    PlaceLocation = EventObject.GPlace;

                    // Add Model details
                    StandardDetails = DV.EventDV.GetModelInfoFormatted(EventObject);

                    // If event note, display it while showing the full list further below.
                    HighlightedNote = EventObject.GNoteRefCollection.GetFirstOfType(Constants.NoteTypeEvent);

                    NotesWithoutHighlight = EventObject.GNoteRefCollection.GetCollectionWithoutOne(HighlightedNote);

                    // Add Event Link Card
                    HLinkEventModel t = EventObject.HLink;
                    t.DisplayAs = CommonEnums.DisplayFormat.LinkCardMedium;
                    BaseDetail.Add(t);
                }
            }
        }
    }
}