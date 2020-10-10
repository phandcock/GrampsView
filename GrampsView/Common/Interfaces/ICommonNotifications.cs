//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="ICommonNotifications.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for the GrampsView common progress routines.
    /// </summary>
    public interface ICommonNotifications
    {
     
        string DataLogMessage { get; }

        string BottomMessage { get; }

        /// <summary>
        /// Changes the loading message.
        /// </summary>
        /// <param name="strMessage">
        /// The string message.
        /// </param>
        /// <returns>
        /// </returns>
        Task BottomMessageAdd(string strMessage);

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
        Task DataLogEntryAdd(string strMessage);

        Task DataLogEntryReplace(string argMessage);

        /// <summary>
        /// Majors the status add.
        /// </summary>
        /// <param name="strMessage">
        /// The string message.
        /// </param>
        /// <param name="showProgressRing">
        /// if set to <c>true</c> [show progress ring].
        /// </param>
        /// <returns>
        /// </returns>
        Task DataLogEntryAdd(string strMessage, bool showProgressRing);

        /// <summary>
        /// Majors the status delete.
        /// </summary>
        /// <returns>
        /// </returns>
        Task DataLogEntryDelete();

 

        void NotifyAlert(string strMessage);

        /// <summary>
        /// Notifies the dialog box.
        /// </summary>
        /// <param name="strMessage">
        /// The string message.
        /// </param>
        void NotifyDialogBox(ActionDialogArgs argADA);

        /// <summary>
        /// Notifies the error.
        /// </summary>
        /// <param name="strMessage">
        /// The string message.
        /// </param>
        void NotifyError(string strMessage);

        /// <summary>
        /// Notifies the error.
        /// </summary>
        /// <param name="strMessage">
        /// The string message.
        /// </param>
        /// <param name="argErrorDetail">
        /// Error detail.
        /// </param>
        void NotifyError(string strMessage, Dictionary<string, string> argErrorDetail);

        /// <summary>
        /// Helper to Notify Error.
        /// </summary>
        /// <param name="strMessage">
        /// The string message.
        /// </param>
        /// <param name="argErrorDetail">
        /// The argument error detail.
        /// </param>
        void NotifyError(string strMessage, string argErrorDetail);

        /// <summary>
        /// Notify the user about an Exception.
        /// </summary>
        /// <param name="strMessage">
        /// The string message.
        /// </param>
        /// <param name="ex">
        /// The ex.
        /// </param>
        void NotifyException(string strMessage, Exception ex);
    }
}