// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.DataView;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels;
using GrampsView.ModelsDB.Collections.HLinks;

using System.Text.Json.Serialization;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// GRAMPS Alt element class. TODO Update fields as per Schema
    /// </summary>
    /// TODO Update fields as per Schema
    public class PersonRefModel : ModelBase, IPersonRefModel
    {
        [JsonIgnore]
        public PersonModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.PersonDV.GetModelFromHLinkKey(HLinkKey);
                }
                else
                {
                    return new PersonModel();
                }
            }
        }

        public HLinkCitationDBModelCollection GCitationCollection
        {
            get;
        }

            = new HLinkCitationDBModelCollection();

        /// <summary>
        /// Gets or sets the g text.
        /// </summary>
        /// <value>
        /// The g text.
        /// </value>
        public HLinkNoteDBModelCollection GNoteCollection
        {
            get;
        }

            = new HLinkNoteDBModelCollection();

        public string GRelationship
        {
            get; set;
        }
    }
}