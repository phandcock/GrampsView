﻿namespace GrampsView.Data.Model
{
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    /// <summary>
    /// GRAMPS Alt element class. TODO Update fields as per Schema
    /// </summary>
    /// TODO Update fields as per Schema
    public class PersonRefModel : ModelBase, IPersonRefModel
    {
        public new PersonModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.PersonDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return new PersonModel();
                }
            }
        }

        public HLinkCitationModelCollection GCitationCollection
        {
            get;
        }

            = new HLinkCitationModelCollection();

        /// <summary>
        /// Gets or sets the g text.
        /// </summary>
        /// <value>
        /// The g text.
        /// </value>
        public HLinkNoteModelCollection GNoteCollection
        {
            get;
        }

            = new HLinkNoteModelCollection();

        public string GRelationship { get; set; }
    }
}