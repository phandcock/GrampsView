namespace GrampsView.Common
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Events;

    using Prism.Events;

    using System;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// Common Notification routines.
    /// </summary>
    [DataContract]
    public class CommonNotifications : CommonBindableBase, ICommonNotifications
    {
        private readonly IDataLog _DataLog;

        /// <summary>
        /// Common logging routines.
        /// </summary>
        private readonly ICommonLogging _iocCommonLogging;

        /// <summary>
        /// Injected Event Aggregator.
        /// </summary>
        private readonly IEventAggregator _iocEventAggregator;

        private string _MinorMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonNotifications"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common Logging routines
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        /// <param name="iocDataLog">
        /// </param>
        public CommonNotifications(ICommonLogging iocCommonLogging,
                                   IEventAggregator iocEventAggregator, IDataLog iocDataLog)
        {
            if (iocEventAggregator is null)
            {
                throw new ArgumentNullException(nameof(iocEventAggregator));
            }

            _iocEventAggregator = iocEventAggregator;

            _iocCommonLogging = iocCommonLogging;

            _DataLog = iocDataLog;
        }

        public string MinorMessage
        {
            get
            {
                return _MinorMessage;
            }

            set
            {
                SetProperty(ref _MinorMessage, value);
            }
        }

        /// <summary>
        /// Changes the bottom message.
        /// </summary>
        /// <param name="argMessage">
        /// The string message.
        /// </param>
        /// <returns>
        /// </returns>
        public async Task BottomMessageAdd(string argMessage)
        {
            // _iocEventAggregator.GetEvent<ProgressLoading>().Publish(argMessage);

            if (!string.IsNullOrEmpty(argMessage))
            {
                _iocCommonLogging.LogVariable("BottomMessageAdd", argMessage);

                await DataLogEntryAdd(argMessage).ConfigureAwait(false);
            }

            return;
        }

        public async Task BottomMessageReplace(string argMessage)
        {
            // _iocEventAggregator.GetEvent<ProgressLoading>().Publish(argMessage);

            if (!string.IsNullOrEmpty(argMessage))
            {
                _iocCommonLogging.LogVariable("BottomMessageReplace", argMessage);

                await DataLogEntryReplace(argMessage).ConfigureAwait(false);
            }

            return;
        }

        /// <summary>
        /// Notifies the general status.
        /// </summary>
        /// <param name="strMessage">
        /// The string message.
        /// </param>
        /// <returns>
        /// </returns>
        public async Task DataLogEntryAdd(string strMessage)
        {
            await DataLogEntryAdd(strMessage, false).ConfigureAwait(false);
        }

        /// <summary>
        /// Majors the status add.
        /// </summary>
        /// <param name="argMessage">
        /// The string message.
        /// </param>
        /// <param name="argShowProgressRing">
        /// if set to <c>true</c> [show progress ring].
        /// </param>
        /// <returns>
        /// </returns>
        public async Task DataLogEntryAdd(string argMessage, bool argShowProgressRing)
        {
            // await Task.Run(() => _iocEventAggregator.GetEvent<StatusUpdated>().Publish(argMessage)).ConfigureAwait(false);

            await _DataLog.Add(argMessage).ConfigureAwait(false);

            _iocCommonLogging.Progress("DataLogEntryAdd: " + argMessage);

            MinorMessage = argMessage;

            // majorStatusQueue.Enqueue(new QueueItem { Text = strMessage, showProgressRing = false });
            return;
        }

        public async Task DataLogEntryClear()
        {
            await _DataLog.Clear().ConfigureAwait(false);
        }

        /// <summary>
        /// Majors the status delete.
        /// </summary>
        /// <returns>
        /// </returns>
        public async Task DataLogEntryDelete()
        {
            await _DataLog.Remove().ConfigureAwait(false);
        }

        public async Task DataLogEntryReplace(string argMessage)
        {
            // await Task.Run(() => _iocEventAggregator.GetEvent<StatusUpdated>().Publish(argMessage)).ConfigureAwait(false);

            await _DataLog.Replace(argMessage).ConfigureAwait(false);

            _iocCommonLogging.Progress("DataLogEntryReplace: " + argMessage);

            MinorMessage = argMessage;

            return;
        }

        public async Task MinorMessageAdd(string argMessage)
        {
            // _iocEventAggregator.GetEvent<ProgressLoading>().Publish(argMessage);

            if (!string.IsNullOrEmpty(argMessage))
            {
                _iocCommonLogging.LogVariable("MinorMessageAdd", argMessage);

                MinorMessage = argMessage;

                // TODO display in a status bar when we have worked out how
                await DataLogEntryReplace(argMessage).ConfigureAwait(false);
            }

            return;
        }

        public void NotifyAlert(string argMessage, ErrorInfo argErrorDetail = null)
        {
            if (argErrorDetail == null)
            {
                argErrorDetail = new ErrorInfo();
            }

            argErrorDetail.DialogBoxTitle = "Alert";
            argErrorDetail.Text = argMessage;

            // TODO not very clean but what to do when displaying messages before hub page is loaded
            _iocEventAggregator.GetEvent<DialogBoxEvent>().Publish(argErrorDetail);
        }

        public void NotifyError(ErrorInfo argErrorDetail)
        {
            if (argErrorDetail is null)
            {
                argErrorDetail = new ErrorInfo();
            }

            argErrorDetail.DialogBoxTitle = "Error";

            _iocCommonLogging.Error(argErrorDetail);

            // TODO not very clean but what to do when displaying messages before hub page is loaded
            _iocEventAggregator.GetEvent<DialogBoxEvent>().Publish(argErrorDetail);
        }

        /// <summary>
        /// notify the user about an Exception.
        /// </summary>
        /// <param name="argMessage">
        /// general description of where the Exception occurred.
        /// </param>
        /// <param name="ex">
        /// Exception object.
        /// </param>
        public void NotifyException(string argMessage, Exception argException, ErrorInfo argExtraItems = null)
        {
            if (argException is null)
            {
                throw new ArgumentNullException(nameof(argException));
            }

            if (argExtraItems is null)
            {
                argExtraItems = new ErrorInfo();
            }

            argExtraItems.DialogBoxTitle = "Exception";
            argExtraItems.Text = argMessage;

            argExtraItems.Add("Exception Message", argException.Message);
            argExtraItems.Add("Exception Source", argException.Source);

            if (argException.InnerException != null)
            {
                argExtraItems.Add("Innerexception", argException.InnerException.ToString());
            }

            argExtraItems.Add("Stack Trace", argException.StackTrace);

            _iocEventAggregator.GetEvent<DialogBoxEvent>().Publish(argExtraItems);

            _iocCommonLogging.Exception(argException, argExtraItems);

            // Remove serialised data in case it is the issue
            CommonLocalSettings.DataSerialised = false;
        }
    }
}