namespace GrampsView.Common
{
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;
    using Microsoft.Extensions.Logging;

    using System;

    /// <summary>
    /// Provides default logging services usign microsoft.extensions.logger
    /// </summary>
    public class CommonLogging : ICommonLogging
    {
        private static ILogger Log;

        private readonly ILoggerFactory LogFactory = LoggerFactory.Create(builder => { builder.SetMinimumLevel(LogLevel.Trace).AddConsole().AddDebug(); });

        public CommonLogging()
        {
            Log = LogFactory.CreateLogger("GrampsView");

            Log.LogInformation("Log started");
        }

        public void Error(ErrorInfo argErrorDetail = null)
        {
            // Only Start App Center if there
            if (!CommonRoutines.IsEmulator())
            {
                Analytics.TrackEvent("Error", argErrorDetail);
            }

            Log.LogError(argErrorDetail.ToString());
        }

        /// <summary>
        /// Exceptions the specified argument ex.
        /// </summary>
        /// <param name="argEx">
        /// The argument ex.
        /// </param>
        /// <param name="argExtraItems">
        /// The argument extra items.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// argEx
        /// </exception>
        /// <remarks>
        /// Assumes the ErrorInfo parameter already has had the Exxception detail added to it.
        /// </remarks>
        public void Exception(Exception argEx, ErrorInfo argExtraItems = null)
        {
            if (argEx is null)
            {
                throw new ArgumentNullException(nameof(argEx));
            }

            Log.LogCritical(argExtraItems.Name, argExtraItems.ToString());

            if (!CommonRoutines.IsEmulator())
            {
                Crashes.TrackError(argEx, argExtraItems);
            }
        }

        public ILogger GetLog(string argCategory)
        {
            if (argCategory is null)
            {
                return LogFactory.CreateLogger("GrampsView");
                ;
            }

            return LogFactory.CreateLogger(argCategory);
        }

        public void LogGeneral(string argMessage)
        {
            if (argMessage is null)
            {
                throw new ArgumentNullException(nameof(argMessage));
            }

            ErrorInfo errorDetail = new ErrorInfo
            {
                //{ "Message", ex.Message },
            };

            LogGeneral(argMessage, errorDetail);
        }

        public void LogGeneral(string argMessage, ErrorInfo argDetails)
        {
            if (argMessage is null)
            {
                throw new ArgumentNullException(nameof(argMessage));
            }

            if (argDetails is null)
            {
                throw new ArgumentNullException(nameof(argDetails));
            }

            Log.LogDebug(argMessage, argDetails);
        }

        public void LogVariable(string argName, string argValue)
        {
            if (argName is null)
            {
                throw new ArgumentNullException(nameof(argName));
            }

            if (argValue is null)
            {
                throw new ArgumentNullException(nameof(argValue));
            }

            ErrorInfo moreDetail = new ErrorInfo
            {
                //{ "Message", ex.Message },
            };

            Log.LogDebug(argName + " = " + argValue, moreDetail);
        }

        public void Progress(string argMessage)
        {
            if (argMessage is null)
            {
                throw new ArgumentNullException(nameof(argMessage));
            }

            Log.LogTrace(argMessage);
        }

        public void RoutineEntry(string argRoutineName)
        {
            if (argRoutineName is null)
            {
                throw new ArgumentNullException(nameof(argRoutineName));
            }

            Log.LogTrace("Start=> " + argRoutineName);
        }

        public void RoutineExit(string argRoutineName)
        {
            if (argRoutineName is null)
            {
                throw new ArgumentNullException(nameof(argRoutineName));
            }

            Log.LogTrace("End <= " + argRoutineName);
        }
    }
}