namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;

    using Prism.Events;

    using System.Collections.ObjectModel;

    public class MessageLogViewModel : ViewModelBase
    {
        public MessageLogViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "MessageLog";
            BaseTitleIcon = CommonConstants.IconDDefault;
        }

        public ObservableCollection<DataLogEntry> DataLoadLog
        {
            get
            {
                return DataStore.Instance.CN.DataLog.DataLoadLog;
            }
        }
    }
}