namespace GrampsView.Common
{
    using GrampsView.Common.CustomClasses;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for the GrampsView common progress routines.
    /// </summary>
    public interface ICommonNotifications
    {
        IDataLog DataLog
        {
            get;
        }

        bool DialogShown
        {
            get; set;
        }

        string MinorMessage
        {
            get; set;
        }

        Queue<ErrorInfo> PopupQueue
        {
            get;
        }

        /// <summary>
        /// Changes the loading message.
        /// </summary>
        /// <param name="argMessage">
        /// The string message.
        /// </param>
        /// <returns>
        /// </returns>
        Task BottomMessageAdd(string argMessage);

        Task BottomMessageReplace(string argMessage);

        /// <summary>
        /// Notifies the general status.
        /// </summary>
        /// <param name="strMessage">
        /// The string message.
        /// </param>
        /// <param name="showProgressRing">
        /// if set to <c>true</c> [show progress ring].
        /// </param>
        Task DataLogEntryAdd(string argMessage);

        Task DataLogEntryReplace(string argMessage);

        Task DataLogHide();

        Task DataLogShow();

        Task MinorMessageAdd(string argMessage);

        void NotifyAlert(string argMessage, ErrorInfo argErrorDetail = null);

        /// <summary>
        /// Helper to Notify Error.
        /// </summary>
        /// <param name="argErrorDetail">
        /// The argument error detail.
        /// </param>
        void NotifyError(ErrorInfo argErrorDetail = null);

        /// <summary>
        /// Notify the user about an Exception.
        /// </summary>
        /// <param name="argMessage">
        /// Message to be displayed
        /// </param>
        /// <param name="argException">
        /// Exception details
        /// </param>
        /// <param name="argExtraItems">
        /// Extra information
        /// </param>
        void NotifyException(string argMessage, Exception argException, ErrorInfo argExtraItems = null);

        void PopUpShow();
    }
}