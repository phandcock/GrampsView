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
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.Essentials;

    /// <summary>
    /// Common Progress routines.
    /// </summary>
    [DataContract]
    public class CommonNotifications : CommonBindableBase, ICommonNotifications
    {
        /// <summary>
        /// Common logging routines.
        /// </summary>
        private readonly ICommonLogging _iocCommonLogging;

        /// <summary>
        /// Injected Event Aggregator.
        /// </summary>
        private readonly IEventAggregator _iocEventAggregator;

        private string _MajorStatusMessage = string.Empty;

        private string _MinorStatusMessage = string.Empty;

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
                                   IEventAggregator iocEventAggregator)
        {
            if (iocEventAggregator is null)
            {
                throw new ArgumentNullException(nameof(iocEventAggregator));
            }

            _iocEventAggregator = iocEventAggregator;

            _iocCommonLogging = iocCommonLogging;

            //_EventAggregator.GetEvent<GVNotificationLogAdd>().Subscribe(DataLoadLogAdd, ThreadOption.UIThread);
        }

        /// <summary>
        /// Gets the data load log.
        /// </summary>
        /// <value>
        /// The data load log.
        /// </value>
        public ObservableCollection<DataLogEntry> DataLoadLog { get; } = new ObservableCollection<DataLogEntry>();

        public string MajorStatusMessage
        {
            get
            {
                return _MajorStatusMessage;
            }

            private set
            {
                SetProperty(ref _MajorStatusMessage, value);
            }
        }

        public string MinorStatusMessage
        {
            get
            {
                return _MinorStatusMessage;
            }

            private set
            {
                SetProperty(ref _MinorStatusMessage, value);
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
        public async Task ChangeLoadingMessage(string argMessage)
        {
            _iocEventAggregator.GetEvent<ProgressLoading>().Publish(argMessage);

            if (!string.IsNullOrEmpty(argMessage))
            {
                _iocCommonLogging.LogVariable("ChangeLoadingMessage", argMessage);

                await MajorStatusAdd(argMessage).ConfigureAwait(false);
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
        public async Task MajorStatusAdd(string strMessage)
        {
            await MajorStatusAdd(strMessage, false).ConfigureAwait(false);
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
        public async Task MajorStatusAdd(string argMessage, bool argShowProgressRing)
        {
            await Task.Run(() => _iocEventAggregator.GetEvent<StatusUpdated>().Publish(argMessage)).ConfigureAwait(false);

            await DataLoadLogAdd(argMessage);

            _iocCommonLogging.LogProgress("MajorStatusAdd" + argMessage);

            MajorStatusMessage = argMessage;

            // majorStatusQueue.Enqueue(new QueueItem { Text = strMessage, showProgressRing = false });
            return;
        }

        /// <summary>
        /// Majors the status delete.
        /// </summary>
        /// <returns>
        /// </returns>
        public async Task MajorStatusDelete()
        {
            //MajorStatusMessage = string.Empty;

            await DataLoadLogRemove();

            //// Pop top item
            // if (majorStatusQueue.Count > 0) { QueueItem oldItem = majorStatusQueue.Dequeue();

            // localCL.LogVariable("NotifyUserGeneral", "Major Status Delete => " + oldItem.Text); }

            //// Display current item
            // if (majorStatusQueue.Count > 0) { QueueItem currentItem = majorStatusQueue.Peek();

            // await Task.Run(() =>
            // localEventAggregator.GetEvent<GVProgressMajorTextUpdate>().Publish(currentItem.Text)).ConfigureAwait(false);
            // } else { await Task.Run(() =>
            // localEventAggregator.GetEvent<GVProgressMajorTextUpdate>().Publish(null)).ConfigureAwait(false); }
        }

        public async Task MinorStatusAdd(string argMessage)
        {
            await Task.Run(() => _iocEventAggregator.GetEvent<StatusUpdated>().Publish(argMessage)).ConfigureAwait(false);

            await DataLoadLogAdd(argMessage);

            _iocCommonLogging.LogVariable("MinorStatusAdd", argMessage);

            MinorStatusMessage = argMessage;

            // majorStatusQueue.Enqueue(new QueueItem { Text = strMessage, showProgressRing = false });
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
                ItemDetails = argErrorDetail,
            };

            NotifyDialogBox(t);

            //MajorStatusAdd(argMessage);

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

        private async Task<bool> DataLoadLogAdd(string entry)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
          {
              DataLogEntry t = default(DataLogEntry);

              if (!string.IsNullOrEmpty(entry))
              {
                  t.Label = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:HH: mm:ss}", System.DateTime.Now);
                  t.Text = entry;

                  DataLoadLog.Insert(0, t);
              }
          });

            return true;
        }

        private async Task<bool> DataLoadLogRemove()
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (DataLoadLog.Count > 0)
                {
                    DataLoadLog.Remove(DataLoadLog.FirstOrDefault());
                }
            });

            return true;
        }
    }
}