namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.Messaging;

    using SharedSharp.Logging;

    using System.Collections.ObjectModel;

    public class MessageLogViewModel : ViewModelBase
    {
        public ObservableCollection<DataLogEntry> DataLoadLog
        {
            get
            {
                return App.Current.Services.GetService<IErrorNotifications>().DataLog.DataLoadLog;
            }
        }

        public MessageLogViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
                    : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "MessageLog";
            BaseTitleIcon = CommonConstants.IconDDefault;
        }
    }
}