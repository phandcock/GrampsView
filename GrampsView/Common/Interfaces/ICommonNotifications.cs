namespace GrampsView.Common
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for the GrampsView common progress routines.
    /// </summary>
    public interface ICommonNotifications
    {
        //string BottomMessage { get; }
        //string DataLogMessage { get; }

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

        /// <summary>
        /// Majors the status delete.
        /// </summary>
        /// <returns>
        /// </returns>
        Task DataLogEntryDelete();

        Task DataLogEntryReplace(string argMessage);

        Task MinorMessageAdd(string argMessage);

        void NotifyAlert(string argMessage);

        /// <summary>
        /// Notifies the dialog box.
        /// </summary>
        /// <param name="argMessage">
        /// The string message.
        /// </param>
        void NotifyDialogBox(ErrorInfo argADA);

        ///// <summary>
        ///// Notifies the error.
        ///// </summary>
        ///// <param name="argMessage">
        ///// The string message.
        ///// </param>
        //void NotifyError(string argMessage);

        ///// <summary>
        ///// Notifies the error.
        ///// </summary>
        ///// <param name="argMessage">
        ///// The string message.
        ///// </param>
        ///// <param name="argErrorDetail">
        ///// Error detail.
        ///// </param>
        //void NotifyError(string argMessage, ErrorInfo argErrorDetail = null);

        ///// <summary>
        ///// Helper to Notify Error.
        ///// </summary>
        ///// <param name="argMessage">
        ///// The string message.
        ///// </param>
        ///// <param name="argErrorDetail">
        ///// The argument error detail.
        ///// </param>
        //void NotifyError(string argMessage, string argErrorDetail);

        /// <summary>
        /// Helper to Notify Error.
        /// </summary>
        /// <param name="argMessage">
        /// The string message.
        /// </param>
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
        /// <param name="ex">
        /// The ex.
        /// </param>
        void NotifyException(string argMessage, Exception argException, ErrorInfo argExtraItems = null);
    }
}