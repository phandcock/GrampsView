//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="IPersonSummaryPageViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Data.Model;

    /// <summary>
    /// Interface Person Summary Page View ViewModel.
    /// </summary>
    public interface IPersonSummaryViewModel
    {
        /// <summary>
        /// Gets or sets the this person.
        /// </summary>
        /// <value>
        /// The this person.
        /// </value>
        HLinkPersonModel ThisPerson
        {
            get;
            set;
        }
    }
}
