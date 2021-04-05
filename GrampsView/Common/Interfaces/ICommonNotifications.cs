namespace GrampsView.Common
{
    using GrampsView.Common.CustomClasses;

    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for the GrampsView common progress routines.
    /// </summary>
    public interface ICommonNotifications
    {
        ErrorInfo DialogArgs
        {
            get; set;
        }

        string MinorMessage
        {
            get; set;
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

        /// <summary>
        /// Majors the status add.
        /// </summary>
        /// <param name="argMessage">
        /// The string message.
        /// </param>
        /// <param name="showProgressRing">
        /// if set to <c>true</c> [show progress ring].
        /// </param>
        /// <returns>
        /// </returns>
        Task DataLogEntryAdd(string argMessage, bool argShowProgressRing);

        Task DataLogEntryClear();

        /// <summary>
        /// Majors the status delete.
        /// </summary>
        /// <returns>
        /// </returns>
        Task DataLogEntryDelete();

        Task DataLogEntryReplace(string argMessage);

        Task MinorMessageAdd(string argMessage);

        Task NotifyAlert(string argMessage, ErrorInfo argErrorDetail = null);

        /// <summary>
        /// Helper to Notify Error.
        /// </summary>
        /// <param name="argErrorDetail">
        /// The argument error detail.
        /// </param>
        Task NotifyError(ErrorInfo argErrorDetail = null);

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
        Task NotifyException(string argMessage, Exception argException, ErrorInfo argExtraItems = null);
    }
}