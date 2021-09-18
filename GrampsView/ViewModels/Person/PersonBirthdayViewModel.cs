namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using Prism.Events;

    /// <summary>
    /// View Model for the Event Section Page.
    /// </summary>
    public class PersonBirthdayViewModel : ViewModelBase
    {
        public PersonBirthdayViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Person Birthday List";
            BaseTitleIcon = CommonConstants.IconPeopleBirthday;
        }

        public Group<HLinkPersonModelCollection> PersonSource
        {
            get
            {
                return DV.PersonDV.GetAllAsGroupedBirthDayCardGroup();
            }
        }
    }
}