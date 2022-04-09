namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using Microsoft.Toolkit.Mvvm.Messaging;

    using SharedSharp.Logging;

    /// <summary>
    /// View Model for the Event Section Page.
    /// </summary>
    public class PersonBirthdayViewModel : ViewModelBase
    {
        public PersonBirthdayViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Person Birthday List";
            BaseTitleIcon = CommonConstants.IconPeopleBirthday;
        }

        public bool BirthdayShowOnlyLivingFlag
        {
            get
            {
                return CommonLocalSettings.BirthdayShowOnlyLivingFlag;
            }
            set
            {
                CommonLocalSettings.BirthdayShowOnlyLivingFlag = value;
            }
        }

        public Group<HLinkPersonModelCollection> PersonSource
        {
            get
            {
                return DV.PersonDV.GetAllAsGroupedBirthDayCardGroup(BirthdayShowOnlyLivingFlag);
            }
        }
    }
}