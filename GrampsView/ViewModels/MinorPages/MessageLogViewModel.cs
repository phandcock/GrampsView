//-----------------------------------------------------------------------
// <copyright file="MessageLogViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Message Log routines.
/// </summary>
namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// <c>Viewmodel</c> for the Message Log Page.
    /// </summary>
    public class MessageLogViewModel : ViewModelBase
    {
        /// <summary>
        /// The injected data log
        /// </summary>
        private IDataLog _iocDataLog;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageLogViewModel"/> class.
        /// </summary>
        public MessageLogViewModel(IDataLog iocDataLog)

        {
            BaseTitle = "Message Log";

            BaseTitleIcon = CommonConstants.IconLog;

            Contract.Assert(iocDataLog != null);
            _iocDataLog = iocDataLog;
        }

        /// <summary>
        /// Gets the data load log.
        /// </summary>
        /// <value>
        /// The data load log.
        /// </value>
        public ObservableCollection<DataLogEntry> MajorStatusList
        {
            get
            {
                return _iocDataLog.DataLoadLog;
            }
        }
    }
}