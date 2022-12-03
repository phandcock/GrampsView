﻿//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="IAddressModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using GrampsView.Data.Collections;
    using GrampsView.Models.Collections.HLinks;

    /// <summary>
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.IModelBase"/>
    public interface IAddressModel : IModelBase
    {
        HLinkCitationModelCollection GCitationRefCollection { get; }

        /// <summary>
        /// Gets or sets the g citation reference collection.
        /// </summary>
        /// <value>
        /// The g citation reference collection.
        /// </value>
        string GCity { get; set; }

        string GCountry { get; set; }

        string GCounty { get; set; }

        DateObjectModel GDate { get; set; }
        string GLocality { get; set; }

        HLinkNoteModelCollection GNoteRefCollection { get; }

        string GPhone { get; set; }

        string GPostal { get; set; }

        string GState { get; set; }

        string GStreet { get; set; }

        /// <summary>
        /// Gets the hlink for the address model.
        /// </summary>
        /// <value>
        /// The h link.
        /// </value>
        HLinkAdressModel HLink { get; }
    }
}