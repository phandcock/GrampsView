namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

    using System;
    using System.ComponentModel;

    /// <summary>
    /// Defines the Event Detail Page View ViewModel.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.ViewModelBase"/>
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

        public HLinkEventModel HLinkObject
        {
            get; set;
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        public override void BaseHandleAppearingEvent()
        {
            HLinkObject = CommonRoutines.DeserialiseObject<HLinkEventModel>(Uri.UnescapeDataString(BaseParamsHLink));

            if (!(HLinkObject is null) && (HLinkObject.Valid))
            {
                EventObject = HLinkObject.DeRef;

                if (!(EventObject is null) && (EventObject.Valid))
                {
                    BaseTitle = EventObject.GetDefaultText;
                    BaseTitleIcon = CommonConstants.IconEvents;

                    // Get basic details
                    BaseDetail.Add(new CardListLineCollection("Event Detail")
                    {
                        new CardListLine("Type:", EventObject.GType),
                        new CardListLine("Role",HLinkObject.GRole),
                        new CardListLine("Event Age:", EventObject.GDate.GetAge),
                    });

                    // Get date card
                    BaseDetail.Add(EventObject.GDate.AsCardListLine());

                    // Add the description and event place card
                    BaseDetail.Add(new CardListLineCollection("Description")
                        {
                            new CardListLine(string.Empty, EventObject.GDescription)
                        });

                    BaseDetail.Add(EventObject.GPlace);

                    // Add Model details
                    BaseDetail.Add(DV.EventDV.GetModelInfoFormatted(EventObject));
                }
            }
        }
    }
}