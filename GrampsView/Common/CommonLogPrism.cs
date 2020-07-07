// <copyright file="CommonLogPrism.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Common
{
    using System;
    using System.Diagnostics;
    using GrampsView.Data.Repository;
    using Prism.Logging;

    /// <summary>
    /// Logger Facade for Prism.
    /// TODO: Only until made obsolete in Prism 8
    /// </summary>
    /// <seealso cref="Prism.Logging.ILoggerFacade"/>
    public class CommonLogPrism : ILoggerFacade
    {
        private readonly ICommonLogging Logger;

        public CommonLogPrism(ICommonLogging iocCommonLogging)
        {
            Logger = iocCommonLogging;
        }

        /// <summary>
        /// Writes a log message.
        /// </summary>
        /// <param name="message">
        /// The message to write.
        /// </param>
        /// <param name="category">
        /// The message category.
        /// </param>
        /// <param name="priority">
        /// Not used by Log4Net; pass Priority.None.
        /// </param>
        public void Log(string message, Category category, Priority priority)
        {
            string now = DateTime.Now.Ticks.ToString(System.Globalization.CultureInfo.CurrentCulture);

            switch (category)
            {
                case Category.Debug:
                    Logger.LogGeneral(now + "Debug:" + message);
                    break;

                case Category.Warn:
                    Logger.LogGeneral(now + "Warn:" + message);
                    break;

                case Category.Exception:
                    Logger.LogGeneral(now + "Exception:" + message);
                    break;

                case Category.Info:
                    Logger.LogGeneral(now + "Info:" + message);
                    break;

                default:
                    Logger.LogGeneral(now + "Unknown category:" + category + ":" + message);
                    break;
            }
        }
    }
}