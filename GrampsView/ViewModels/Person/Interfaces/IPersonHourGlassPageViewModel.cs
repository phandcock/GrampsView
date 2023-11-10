// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.ViewModels
{
    using GrampsView.Data.Collections;
    using GrampsView.Models.Collections.HLinks;
    using GrampsView.Models.DataModels;
    using GrampsView.ModelsDB.Collections.HLinks;

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

        HLinkEventDBModelCollection EventCollection
        {
            get;
        }

        HLinkMediaModelCollection MediaObjectCollection
        {
            get;
        }

        HLinkNoteDBModelCollection NoteCollection
        {
            get;
        }

        HLinkFamilyDBModelCollection ParentRelationshipCollection
        {
            get;
        }
    }
}