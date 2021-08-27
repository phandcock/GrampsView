namespace GrampsView.Common
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;
    using GrampsView.Events;
    using GrampsView.Views;

    using Prism.Events;

    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.Extensions;
    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// Common Notification routines.
    /// </summary>
    [DataContract]
    public class CommonNotifications : ObservableObject, ICommonNotifications
    {
        /// <summary>
        /// Common logging routines.
        /// </summary>
        private readonly ICommonLogging _iocCommonLogging;

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
        public CommonNotifications(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
        {
            _iocCommonLogging = iocCommonLogging;
            _iocEventAggregator = iocEventAggregator;

            _iocEventAggregator.GetEvent<ShowPopUpEvent>().Subscribe(ShowPopUp, ThreadOption.UIThread);
        }

        public IDataLog DataLog
        {
            get;
        }

        = new CommonDataLog();

        public bool DialogShown
        {
            get; set;
        } = false;

        //= new ErrorInfo();
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

        public Queue<ErrorInfo> PopupQueue { get; } = new Queue<ErrorInfo>();

        public static void ShowPopUp()
        {
            if (DataStore.Instance.CN.PopupQueue.Count > 0)
            {
                DataStore.Instance.CN.PopUpShow();
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

                DataLogEntryReplace(argMessage);
            }

            return;
        }

        /// <summary>
        /// Majors the status add.
        /// </summary>
        /// <param name="argMessage">
        /// The string message.
        /// </param>
        /// <returns>
        /// <br/>
        /// </returns>
        public async Task DataLogEntryAdd(string argMessage)
        {
            DataLog.Add(argMessage);

            _iocCommonLogging.Progress("DataLogEntryAdd: " + argMessage);

            MinorMessage = argMessage;

            return;
        }

        public void DataLogEntryReplace(string argMessage)
        {
            // await Task.Run(() => _iocEventAggregator.GetEvent<StatusUpdated>().Publish(argMessage)).ConfigureAwait(false);

            DataLog.Replace(argMessage);

            _iocCommonLogging.Progress("DataLogEntryReplace: " + argMessage);

            MinorMessage = argMessage;

            return;
        }

        public async Task DataLogHide()
        {
            await CommonRoutines.NavigateBack();
        }

        public async Task DataLogShow()
        {
            await CommonRoutines.NavigateAsync(nameof(MessageLogPage));
        }

        public async Task MinorMessageAdd(string argMessage)
        {
            if (!string.IsNullOrEmpty(argMessage))
            {
                _iocCommonLogging.LogVariable("MinorMessageAdd", argMessage);

                MinorMessage = argMessage;

                DataLogEntryReplace(argMessage);
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

            PopupQueue.Enqueue(argErrorDetail);

            _iocEventAggregator.GetEvent<ShowPopUpEvent>().Publish();
        }

        public void NotifyError(ErrorInfo argErrorDetail)
        {
            if (argErrorDetail is null)
            {
                argErrorDetail = new ErrorInfo();
            }

            argErrorDetail.DialogBoxTitle = "Error";

            _iocCommonLogging.Error(argErrorDetail);

            PopupQueue.Enqueue(argErrorDetail);

            _iocEventAggregator.GetEvent<ShowPopUpEvent>().Publish();
        }

        /// <summary>
        /// Notify the user about an Exception.
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

            PopupQueue.Enqueue(argExtraItems);

            _iocEventAggregator.GetEvent<ShowPopUpEvent>().Publish();

            _iocCommonLogging.Exception(argException, argExtraItems);

            CommonLocalSettings.DataSerialised = false;
        }

        public void PopUpShow()
        {
            ErrorPopup popup = new ErrorPopup();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Application.Current.MainPage.Navigation.ShowPopupAsync(popup);
            });

            // // TODO Ignore exception. See https://github.com/xamarin/XamarinCommunityToolkit/issues/1251

            // // TODO fix UWP Release issue until fixed
        }
    }
}