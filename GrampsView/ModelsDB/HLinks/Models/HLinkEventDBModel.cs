// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Data.DataView;
using GrampsView.DBModels;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.HLinks;
using GrampsView.ModelsDB.HLinks.Interfaces;
using GrampsView.Views;

namespace GrampsView.ModelsDB.HLinks.Models
{
    /// <summary>
    /// HLink to Event Model but with its own fields as per Gramps
    /// </summary>

    public class HLinkEventDBModel : HLinkDBBase, IHLinkEventDBModel
    {
        private EventDBModel _Deref = new EventDBModel();

        private string _GRole;

        private bool DeRefCached = false;

        public HLinkEventDBModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconEvents;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundEvent");
        }

        public override Page NavigationPage()
        {
            return new EventDetailPage(this);
        }

        /// <summary>
        /// Gets the Event Model pointed to.
        /// </summary>
        /// <value>
        /// The Event Model.
        /// </value>
        public EventDBModel DeRef
        {
            get
            {
                if (Valid && !DeRefCached)
                {
                    _Deref = DL.EventDL.GetModelFromHLinkKey(HLinkKey);

                    if (_Deref.Valid)
                    {
                        DeRefCached = true;
                    }
                }

                return _Deref;
            }
        }

        /// <summary>
        /// Gets or sets the Attribute Collection.
        /// </summary>
        /// <value>
        /// The Attribute.
        /// </value>

        public HLinkAttributeModelCollection GAttributeRefCollection { get; set; } = new HLinkAttributeModelCollection();

        /// <summary>
        /// Gets or sets the Note Model collection.
        /// </summary>
        /// <value>
        /// The g note model collection.
        /// </value>

        public HLinkNoteDBModelCollection GNoteRefCollection { get; set; } = new HLinkNoteDBModelCollection();

        public string GRole
        {
            get
            {
                return _GRole;
            }

            set
            {
                SetProperty(ref _GRole, value);
            }
        }
    }
}