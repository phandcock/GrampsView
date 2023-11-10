// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels.Date;
using GrampsView.ModelsDB.Collections.HLinks;

namespace GrampsView.Models.DataModels.Interfaces
{
    /// <summary>
    ///   <br />
    /// </summary>
    public interface IAddressModel : IModelBase
    {
        HLinkCitationDBModelCollection GCitationRefCollection { get; }

        /// <summary>
        /// Gets or sets the g citation reference collection.
        /// </summary>
        /// <value>
        /// The gcitation reference collection.
        /// </value>
        string GCity { get; set; }

        string GCountry { get; set; }

        string GCounty { get; set; }

        DateObjectModelBase GDate { get; set; }
        string GLocality { get; set; }

        HLinkNoteDBModelCollection GNoteRefCollection { get; }

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