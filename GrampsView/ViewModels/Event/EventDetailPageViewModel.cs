namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;
    using Prism.Navigation;

    /// <summary>
    /// Defines the EVent Detail Page View ViewModel.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.ViewModelBase"/>
    public class EventDetailViewModel : ViewModelBase
    {
        private HLinkEventModel _HLinkObject = new HLinkEventModel();

        /// <summary>
        /// Holds the Event ViewModel.
        /// </summary>
        private EventModel localEventObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonModelGridBuilder">
        /// The ioc common model grid builder.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        /// <param name="iocCommonLogging">
        /// Common Logging interface.
        /// </param>
        public EventDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
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
            get
            {
                return localEventObject;
            }

            set
            {
                SetProperty(ref localEventObject, value);
            }
        }

        public HLinkEventModel HLinkObject
        {
            get
            {
                return _HLinkObject;
            }

            set
            {
                SetProperty(ref _HLinkObject, value);
            }
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        public override void PopulateViewModel()
        {
            HLinkObject = BaseNavParamsHLink as HLinkEventModel;

            if (!(HLinkObject is null) && (HLinkObject.Valid))
            {
                EventObject = HLinkObject.DeRef;

                // Trigger refresh of View fields via INotifyPropertyChanged
                RaisePropertyChanged(string.Empty);

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

                    // Add Model details
                    BaseDetail.Add(DV.EventDV.GetModelInfoFormatted(localEventObject));

                    // Add the description and event place card
                    CardListLineCollection t1 = new CardListLineCollection
                        {
                            new CardListLine("Description", EventObject.GDescription)
                        };
                    BaseDetail.Add(t1);

                    BaseDetail.Add(EventObject.GPlace);
                }
            }
        }
    }
}