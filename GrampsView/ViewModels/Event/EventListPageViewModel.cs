namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    public class EventListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventListViewModel"/> class.
        /// </summary>

        public EventListViewModel()

        {
            BaseTitle = "Event List";
            BaseTitleIcon = CommonConstants.IconEvents;
        }

        public CardGroupBase<HLinkEventModel> EventSource
        {
            get
            {
                return DV.EventDV.GetAllAsCardGroupBase();
            }
        }
    }
}