namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// <c>Viewmodel</c> for the Message Log Page.
    /// </summary>
    public class MessageLogViewModel : ViewModelBase
    {
        public ICommonNotifications _iocCommonNotifications;

        /// <summary>
        /// The injected data log
        /// </summary>
        private IDataLog _iocDataLog;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageLogViewModel"/> class.
        /// </summary>
        public MessageLogViewModel(IDataLog iocDataLog, ICommonNotifications iocCommonNotifications)

        {
            BaseTitle = "Message Log";

            BaseTitleIcon = CommonConstants.IconLog;

            Contract.Assert(iocDataLog != null);
            _iocDataLog = iocDataLog;
            _iocCommonNotifications = iocCommonNotifications;
        }

        /// <summary>
        /// Gets the data load log.
        /// </summary>
        /// <value>
        /// The data load log.
        /// </value>
        public ObservableCollection<DataLogEntry> MajorStatusList
        {
            get
            {
                return _iocDataLog.DataLoadLog;
            }
        }


    }
}