﻿//-----------------------------------------------------------------------
//
// Common routines for the CommonRoutines
//
// <copyright file="CommonLogging.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;
    using Microsoft.Extensions.Logging;

    using System;
    using System.Collections.Generic;

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

        public void Error(string argMessage, AdditionalInfoItems argErrorDetail = null)
        {
            AdditionalInfoItems errorDetail = new AdditionalInfoItems
            {
                { "Message", argMessage }
            };

            if (argErrorDetail != null)
            {
                foreach (var item in argErrorDetail)
                {
                    errorDetail.Add(item.Key, item.Value);
                }
            }

            // Only Start App Center if there
            if (!CommonRoutines.IsEmulator())
            {
                Crashes.TrackError(null, errorDetail);

                Analytics.TrackEvent(argMessage, errorDetail);
            }

            Log.LogError(argMessage, argErrorDetail);
        }

        public void Exception(string argMessage, Exception argEx, AdditionalInfoItems argExtraItems = null)
        {
            if (argMessage is null)
            {
                throw new ArgumentNullException(nameof(argMessage));
            }

            if (argEx is null)
            {
                throw new ArgumentNullException(nameof(argEx));
            }

            Dictionary<string, string> errorDetail = new Dictionary<string, string>
            {
                { "Message", argMessage },

                { "Exception Message", argEx.Message },
                { "Exception Source", argEx.Source },
                { "Exception StackTrace", argEx.StackTrace }
            };

            if (argExtraItems != null)
            {
                foreach (var item in argExtraItems)
                {
                    errorDetail.Add(item.Key, item.Value);
                }
            }

            if (argEx.InnerException != null)
            {
                errorDetail.Add("Inner Exception", argEx.InnerException.Message);
            }

            Log.LogCritical(argMessage, errorDetail);

            // Only Start App Center if there string exceptionMessage = argMessage + " - Exception:"

            if (!CommonRoutines.IsEmulator())
            {
                Crashes.TrackError(argEx, errorDetail);
            }
        }

        public ILogger GetLog(string argCategory)
        {
            if (argCategory is null)
            {
                return LogFactory.CreateLogger("GrampsView"); ;
            }

            return LogFactory.CreateLogger(argCategory);
        }

        public void LogGeneral(string argMessage)
        {
            if (argMessage is null)
            {
                throw new ArgumentNullException(nameof(argMessage));
            }

            AdditionalInfoItems errorDetail = new AdditionalInfoItems
            {
                //{ "Message", ex.Message },
            };

            LogGeneral(argMessage, errorDetail);
        }

        public void LogGeneral(string argMessage, AdditionalInfoItems argDetails)
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

            AdditionalInfoItems moreDetail = new AdditionalInfoItems
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