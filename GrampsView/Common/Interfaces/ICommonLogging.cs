//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="ICommonLogging.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace GrampsView.Common
{
    /// <summary>
    /// Interface for the GrampsView common progress routines.
    /// </summary>
    public interface ICommonLogging
    {
        void LogGeneral(string argMessage);

        void LogGeneral(string argMessage, Dictionary<string, string> argDetails);

        /// <summary>
        /// Logs the progress.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        void LogProgress(string value);

        /// <summary>
        /// Log start of routine.
        /// </summary>
        /// <param name="routine">
        /// Subroutine starting.
        /// </param>
        void LogRoutineEntry(string routine);

        /// <summary>
        /// Logs the routine exit.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void LogRoutineExit(string message);

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
    }
}