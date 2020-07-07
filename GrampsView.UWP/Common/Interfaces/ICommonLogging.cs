//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="ICommonLogging.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsViewNonUI.Common
{
    /// <summary>
    /// Interface for the GrampsView common progress routines.
    /// </summary>
    public interface ICommonLogging
    {
        ///// <summary>
        ///// Gets the log channel.
        ///// </summary>
        ///// <value>The log channel.</value>
        //LoggingChannel LogChannel
        //{
        //    get;
        //}

        /// <summary>
        /// Closes the logging.
        /// </summary>
        void CloseLogging();

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