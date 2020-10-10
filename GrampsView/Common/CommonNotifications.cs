//-----------------------------------------------------------------------
//
// Common routines for the CommonProgressRoutines
//
// <copyright file="CommonNotifications.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using GrampsView.Events;

    using Prism.Events;

    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// Common Progress routines.
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

        private string _BottomMessage = string.Empty;
        private string _DataLogMessage = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonNotifications"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common Logging routines
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
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

        public string BottomMessage
        {
            get
            {
                return _BottomMessage;
            }

            private set
            {
                SetProperty(ref _BottomMessage, value);
            }
        }

        public string DataLogMessage
        {
            get
            {
                return _DataLogMessage;
            }

            private set
            {
                SetProperty(ref _DataLogMessage, value);
            }
        }

        /// <summary>
        /// Changes the loading message.
        /// </summary>
        /// <param name="argMessage">
        /// The string message.
        /// </param>
        /// <returns>
        /// </returns>
        public async Task BottomMessageAdd(string argMessage)
        {
            _iocEventAggregator.GetEvent<ProgressLoading>().Publish(argMessage);

            if (!string.IsNullOrEmpty(argMessage))
            {
                _iocCommonLogging.LogVariable("BottomMessageAdd", argMessage);

                await DataLogEntryAdd(argMessage).ConfigureAwait(false);
            }

            return;
        }

        public async Task BottomMessageReplace(string argMessage)
        {
            _iocEventAggregator.GetEvent<ProgressLoading>().Publish(argMessage);

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
            await Task.Run(() => _iocEventAggregator.GetEvent<StatusUpdated>().Publish(argMessage)).ConfigureAwait(false);

            await _DataLog.Add(argMessage).ConfigureAwait(false);

            _iocCommonLogging.LogProgress("DataLogEntryAdd: " + argMessage);

            DataLogMessage = argMessage;

            // majorStatusQueue.Enqueue(new QueueItem { Text = strMessage, showProgressRing = false });
            return;
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
            await Task.Run(() => _iocEventAggregator.GetEvent<StatusUpdated>().Publish(argMessage)).ConfigureAwait(false);

            await _DataLog.Replace(argMessage).ConfigureAwait(false);

            _iocCommonLogging.LogProgress("DataLogEntryReplace: " + argMessage);

            DataLogMessage = argMessage;

            return;
        }

        public void NotifyAlert(string strMessage)
        {
            Dictionary<string, string> argErrorDetail = new Dictionary<string, string>();

            NotifyDialogBox("Alert", strMessage, argErrorDetail);
        }

        /// <summary>
        /// Handle DialogBox messages.
        /// </summary>
        /// <param name="argADA">
        /// The message text.
        /// </param>
        public void NotifyDialogBox(ActionDialogArgs argADA)
        {
            // TODO not very clean but what to do when displaying messages before hub page is loaded
            _iocEventAggregator.GetEvent<DialogBoxEvent>().Publish(argADA);
        }

        /// <summary>
        /// Notifies the user of an error and logs it for further analysis.
        /// </summary>
        /// <param name="argMessage">
        /// The argument message.
        /// </param>
        /// <param name="argErrorDetail">
        /// The argument error detail.
        /// </param>
        public void NotifyDialogBox(string argHeader, string argMessage, Dictionary<string, string> argErrorDetail)
        {
            if (argErrorDetail is null)
            {
                argErrorDetail = new Dictionary<string, string>();
            }

            ActionDialogArgs t = new ActionDialogArgs
            {
                Name = argHeader,
                Text = argMessage,
            };

            foreach (var item in argErrorDetail)
            {
                t.ItemDetails.Add(item.Key, item.Value);
            }

            NotifyDialogBox(t);

            //DataLogEntryAdd(argMessage);

            //argErrorDetail.Add("Error", argMessage);

            //CommonLogging.LogError(argMessage, argErrorDetail);
        }

        public void NotifyError(string argMessage, Dictionary<string, string> argErrorDetail)
        {
            if (argErrorDetail is null)
            {
                argErrorDetail = new Dictionary<string, string>();
            }

            CommonLogging.LogError(argMessage, argErrorDetail);

            NotifyDialogBox("Error", argMessage, argErrorDetail);
        }

        /// <summary>
        /// Notifies the error.
        /// </summary>
        /// <param name="argMessage">
        /// The string message.
        /// </param>
        public void NotifyError(string argMessage)
        {
            Dictionary<string, string> argErrorDetail = new Dictionary<string, string>();

            NotifyError(argMessage, argErrorDetail);
        }

        /// <summary>
        /// Helper to Notify Error.
        /// </summary>
        /// <param name="strMessage">
        /// The string message.
        /// </param>
        /// <param name="argErrorDetail">
        /// The argument error detail.
        /// </param>
        public void NotifyError(string argMessage, string argDetails)
        {
            Dictionary<string, string> argErrorDetail = new Dictionary<string, string>
            {
                { "Details", argDetails }
            };

            NotifyError(argMessage, argErrorDetail);
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
        public void NotifyException(string argMessage, Exception ex)
        {
            if (ex is null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

            string exceptionMessage = argMessage + " - Exception:" + ex.Message + " - " + ex.Source + " - " + ex.InnerException + " - " + ex.StackTrace;

            NotifyError(exceptionMessage);

            CommonLogging.LogException(argMessage, ex);

            // Remove serialised data in case it is the issue
            CommonLocalSettings.DataSerialised = false;
        }
    }
}