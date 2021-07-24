namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

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

        public EventDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
                                    : base(iocCommonLogging, iocEventAggregator)
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

        public HLinkNoteModel HLinkNote
        {
            get; set;
        } = new HLinkNoteModel();

        public HLinkEventModel HLinkObject
        {
            get; set;
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        public override void BaseHandleLoadEvent()
        {
            HLinkObject = CommonRoutines.GetHLinkParameter<HLinkEventModel>(BaseParamsHLink);

            if (!(HLinkObject is null) && HLinkObject.Valid)
            {
                EventObject = HLinkObject.DeRef;

                if (!(EventObject is null) && EventObject.Valid)
                {
                    BaseTitle = EventObject.GetDefaultText;
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
                    HLinkNote = EventObject.GNoteRefCollection.GetFirstOfType(CommonConstants.NoteTypeEvent);
                }
            }
        }
    }
}