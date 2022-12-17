using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels;

using SharedSharp.Model;
using SharedSharp.Models;

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

        [Obsolete]
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

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        public override void HandleViewModelParameters()
        {
            HLinkObject = CommonRoutines.GetHLinkParameter<HLinkEventModel>(BasePassedArguments);

            if (HLinkObject is not null && HLinkObject.Valid)
            {
                EventObject = HLinkObject.DeRef;

                if (EventObject is not null && EventObject.Valid)
                {
                    BaseModelBase = EventObject;
                    BaseTitleIcon = Constants.IconEvents;

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