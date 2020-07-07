//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="IPersonHourGlassPageViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;

    /// <summary>
    /// Interface for the GrampsView common progress routines.
    /// </summary>
    public interface IPersonHourGlassViewModel
    {
        /// <summary>
        /// Gets a value indicating whether [progress ring active].
        /// </summary>
        /// <value><c>true</c> if [progress ring active]; otherwise, <c>false</c>.</value>
        PersonModel currentPersonModel
        {
            get;
        }

        HLinkEventModelCollection EventCollection
        {
            get;
        }

        HLinkMediaModelCollection MediaObjectCollection
        {
            get;
        }

        HLinkNoteModelCollection NoteCollection
        {
            get;
        }

        HLinkFamilyModelCollection ParentRelationshipCollection
        {
            get;
        }
    }
}