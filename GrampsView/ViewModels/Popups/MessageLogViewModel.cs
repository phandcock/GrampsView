namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;

    using System.Collections.ObjectModel;

    /// <summary>
    /// <c>Viewmodel</c> for the Message Log Page.
    /// </summary>
    public class MessageLogViewModel : CommonBindableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageLogViewModel"/> class.
        /// </summary>
        public MessageLogViewModel()
        {
        }

        public bool DismissFlag
        {
            get
            {
                return DataStore.Instance.CN.DataLog.DismissFlag;
            }
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
                return DataStore.Instance.CN.DataLog.DataLoadLog;
            }
        }
    }
}