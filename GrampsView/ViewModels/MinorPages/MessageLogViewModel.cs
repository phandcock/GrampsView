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
    using GrampsView.Data.Repository;

    using Prism.Events;
    using Prism.Navigation;

    using System.Collections.ObjectModel;

    /// <summary>
    /// <c>Viewmodel</c> for the Message Log Page.
    /// </summary>
    public class MessageLogViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageLogViewModel"/> class.
        /// </summary>
        public MessageLogViewModel()

        {
            BaseTitle = "Message Log";

            BaseTitleIcon = CommonConstants.IconLog;
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
                return DataStore.CN.DataLoadLog;
            }
        }
    }
}