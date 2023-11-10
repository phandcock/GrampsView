// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.DBModels;
using GrampsView.Models.Collections.HLinks;
using GrampsView.ModelsDB.HLinks.Models;

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

        /// <summary>
        /// Gets or sets the public Event ViewModel.
        /// </summary>
        /// <value>
        /// The current event ViewModel.
        /// </value>
        public EventDBModel EventObject
        {
            get; set;
        }

        public HLinkNoteDBModel HighlightedNote
        {
            get; set;
        } = new HLinkNoteDBModel();

        public HLinkNoteDBModelCollection NotesWithoutHighlight
        {
            get; set;
        } = new HLinkNoteDBModelCollection();


        public HLinkEventDBModel HLinkObject { get; set; } = new HLinkEventDBModel();


        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        public override void HandleViewModelParameters()
        {
            if (base.NavigationParameter is not null && base.NavigationParameter.Valid)
            {
                HLinkObject = base.NavigationParameter as HLinkEventDBModel;

                EventObject = HLinkObject.DeRef;

                if (EventObject is not null && EventObject.Valid)
                {
                    BaseDBModelBase = EventObject;
                    BaseTitleIcon = Constants.IconEvents;

                    BaseDetail.Clear();

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
                    BaseDetail.Add(DL.EventDL.GetModelInfoFormatted(EventObject));

                    // If event note, display it while showing the full list further below.
                    HighlightedNote = EventObject.GNoteRefCollection.GetFirstOfType(Constants.NoteTypeEvent);

                    NotesWithoutHighlight = EventObject.GNoteRefCollection.GetCollectionWithoutOne(HighlightedNote);

                    // Add Event Link Card
                    HLinkEventDBModel t = EventObject.HLink;
                    t.DisplayAs = CommonEnums.DisplayFormat.LinkCardMedium;
                    BaseDetail.Add(t);
                }
            }
        }
    }
}