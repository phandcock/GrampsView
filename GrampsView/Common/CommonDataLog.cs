﻿namespace GrampsView.Common
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.Essentials;

    /// <summary>
    /// Common Progress routines.
    /// </summary>
    [DataContract]
    public class CommonDataLog : CommonBindableBase, IDataLog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommonNotifications"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common Logging routines
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        public CommonDataLog()
        {
        }

        /// <summary>
        /// Gets the data load log.
        /// </summary>
        /// <value>
        /// The data load log.
        /// </value>
        public ObservableCollection<DataLogEntry> DataLoadLog { get; } = new ObservableCollection<DataLogEntry>();

        public bool DismissFlag
        {
            get; set;
        }

      = false;

        /// <summary>
        /// Adds the specified entry argument .
        /// </summary>
        /// <param name="argEntry">
        /// The argument entry.
        /// </param>
        /// <returns>
        /// <br/>
        /// </returns>
        public async Task<bool> Add(string argEntry)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
          {
              //DataLogEntry t = default(DataLogEntry);

              if (!string.IsNullOrEmpty(argEntry))
              {
                  DataLoadLog.Add(BuildDataLogEntry(argEntry));
              }
          }).ConfigureAwait(false);

            return true;
        }

        public async Task<bool> Clear()
        {
            DataLoadLog.Clear();

            return true;
        }

        /// <summary>
        /// Removes the first DataLog entry.
        /// </summary>
        /// <returns>
        /// <br/>
        /// </returns>
        public async Task<bool> Remove()
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (DataLoadLog.Count > 0)
                {
                    DataLoadLog.Remove(DataLoadLog.Last());
                }
            }).ConfigureAwait(false);

            return true;
        }

        /// <summary>
        /// Replaces the first datalog entry.
        /// </summary>
        /// <param name="argEntry">
        /// The argument entry.
        /// </param>
        /// <returns>
        /// <br/>
        /// </returns>
        public async Task<bool> Replace(string argEntry)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (!string.IsNullOrEmpty(argEntry))
                {
                    if (DataLoadLog.Count > 0)
                    {
                        DataLoadLog[DataLoadLog.Count - 1] = BuildDataLogEntry(argEntry);
                    }
                }
            }).ConfigureAwait(false);

            return true;
        }

        /// <summary>
        /// Builds a data log entry.
        /// </summary>
        /// <param name="argEntryText">
        /// The argument entry text.
        /// </param>
        /// <returns>
        /// <br/>
        /// </returns>
        private DataLogEntry BuildDataLogEntry(string argEntryText)
        {
            DataLogEntry t = new DataLogEntry
            {
                Label = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:HH: mm:ss}", DateTime.Now).Trim(),
                Text = argEntryText.Trim()
            };

            return t;
        }
    }
}