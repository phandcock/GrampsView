namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.Messaging;

    using SharedSharp.Logging;
    using SharedSharp.Messages;
    using SharedSharp.Model;

    using System.Collections.ObjectModel;

    public class MessageLogViewModel : ViewModelBase
    {
        public ObservableCollection<CardListLine> DataLoadLog { get; } = new ObservableCollection<CardListLine>();

        public MessageLogViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
                    : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "MessageLog";
            BaseTitleIcon = CommonConstants.IconDDefault;

            // Setup Event Handlers
            App.Current.Services.GetService<IMessenger>().Register<MessageLogAdd>(this, (r, m) =>
            {
                DataLoadLog.Add(m.Value);
            });

            App.Current.Services.GetService<IMessenger>().Register<MessageLogReplace>(this, (r, m) =>
            {
                DataLoadLog[DataLoadLog.Count - 1] = m.Value;
            });
        }
    }
}