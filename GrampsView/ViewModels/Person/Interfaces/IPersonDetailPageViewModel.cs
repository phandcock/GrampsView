//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="IPersonDetailPageViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Data.Model;

    /// <summary>
    /// Interface for the GrampsView common progress routines.
    /// </summary>
    public interface IPersonDetailViewModel
    {
        /// <summary>
        /// Gets or sets the current person ViewModel.
        /// </summary>
        /// <value>
        /// The current person ViewModel.
        /// </value>
        PersonModel PersonObject { get; set; }

        ///// <summary>
        ///// Gets the person name details list.
        ///// </summary>
        ///// <value> The person name details list. </value>
        // GrampsViewDetailCollection PersonNameDetailsList { get; }

        ///// <summary>
        ///// Gets or sets the event collection.
        ///// </summary>
        ///// <value>The event collection.</value>
        // HLinkEventModelCollection EventCollection { get; set; }

        ///// <summary>
        ///// Gets or sets the media object collection.
        ///// </summary>
        ///// <value>The media object collection.</value>
        // HLinkMediaModelCollection MediaObjectCollection { get; set; }

        ///// <summary>
        ///// Gets or sets the note collection.
        ///// </summary>
        ///// <value>The note collection.</value>
        // HLinkNoteModelCollection NoteCollection { get; set; }

        ///// <summary>
        ///// Gets or sets the parent relationship collection.
        ///// </summary>
        ///// <value>The parent relationship collection.</value>
        // HLinkFamilyModelCollection ParentRelationshipCollection { get; set; }
    }
}