//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="ICommonLogging.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace GrampsView.Common
{
    /// <summary>
    /// Interface for the GrampsView common progress routines.
    /// </summary>
    public interface ICommonLogging
    {
        void Error(ErrorInfo argErrorDetail = null);

        void Exception(Exception argEx, ErrorInfo argExtraItems = null);

        void LogGeneral(string argMessage);

        void LogGeneral(string argMessage, ErrorInfo argDetails);

        /// <summary>
        /// Logs the variable.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        void LogVariable(string name, string value);

        /// <summary>
        /// Logs the progress.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        void Progress(string value);

        /// <summary>
        /// Log start of routine.
        /// </summary>
        /// <param name="routine">
        /// Subroutine starting.
        /// </param>
        void RoutineEntry(string routine);

        /// <summary>
        /// Logs the routine exit.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void RoutineExit(string message);
    }
}