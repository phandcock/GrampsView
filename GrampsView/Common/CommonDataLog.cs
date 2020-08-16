//-----------------------------------------------------------------------
//
// Common routines for the CommonProgressRoutines
//
// <copyright file="CommonNotifications.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using Prism.Events;

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
        /// Common logging routines.
        /// </summary>
        private readonly ICommonLogging _iocCommonLogging;

        /// <summary>
        /// Injected Event Aggregator.
        /// </summary>
        private readonly IEventAggregator _iocEventAggregator;

        private string _MajorStatusMessage = string.Empty;

        private string _MinorStatusMessage = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonNotifications"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common Logging routines
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        public CommonDataLog(ICommonLogging iocCommonLogging,
                                   IEventAggregator iocEventAggregator)
        {
            if (iocEventAggregator is null)
            {
                throw new ArgumentNullException(nameof(iocEventAggregator));
            }

            _iocEventAggregator = iocEventAggregator;

            _iocCommonLogging = iocCommonLogging;
        }

        /// <summary>
        /// Gets the data load log.
        /// </summary>
        /// <value>
        /// The data load log.
        /// </value>
        public ObservableCollection<DataLogEntry> DataLoadLog { get; } = new ObservableCollection<DataLogEntry>();

        public async Task<bool> Add(string entry)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
          {
              DataLogEntry t = default(DataLogEntry);

              if (!string.IsNullOrEmpty(entry))
              {
                  t.Label = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:HH: mm:ss}", DateTime.Now).Trim();
                  t.Text = entry.Trim();

                  DataLoadLog.Insert(0, t);
              }
          }).ConfigureAwait(false);

            return true;
        }

        public async Task<bool> Remove()
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (DataLoadLog.Count > 0)
                {
                    DataLoadLog.Remove(DataLoadLog.FirstOrDefault());
                }
            }).ConfigureAwait(false);

            return true;
        }

        public async Task<bool> Replace(string entry)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                DataLogEntry t = default(DataLogEntry);

                if (!string.IsNullOrEmpty(entry))
                {
                    t.Label = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:HH: mm:ss}", DateTime.Now).Trim();
                    t.Text = entry.Trim();

                    // TODO fix this so it replaces
                    DataLoadLog.Insert(0, t);
                }
            }).ConfigureAwait(false);

            return true;
        }
    }
}