﻿namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using CommunityToolkit.Mvvm.Messaging;

    using SharedSharp.Logging;
    using GrampsView.Models.Collections.HLinks;

    /// <summary>
    /// View Model for the Event Section Page.
    /// </summary>
    public class PersonBirthdayViewModel : ViewModelBase
    {
        public PersonBirthdayViewModel(SharedSharp.Logging.Interfaces.ILog iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging)
        {
            BaseTitle = "Person Birthday List";
            BaseTitleIcon = Constants.IconPeopleBirthday;
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