// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Data.DataView;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels;
using GrampsView.Models.HLinks;
using GrampsView.Views;

using System.Text.Json.Serialization;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// TODO xml 1.71 needed Child Reference model
    /// </summary>

    public class HLinkChildRefModel : HLinkBase, IHLinkChildRefModel
    {
        private PersonModel _Deref = new PersonModel();

        private bool DeRefCached = false;

        public HLinkChildRefModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconPeople;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPerson");
        }

        [JsonIgnore]
        public PersonModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.PersonDV.GetModelFromHLinkKey(HLinkKey);

                    if (_Deref.Valid)
                    {
                        DeRefCached = true;
                    }
                }

                return _Deref;
            }
        }

        /// <summary>
        /// Citation collection reference.
        /// </summary>
        /// <value>
        /// The citation collection reference.
        /// </value>

        public HLinkCitationModelCollection GCitationCollectionReference
        {
            get; set;
        }

        = new HLinkCitationModelCollection();

        public string GFatherRel
        {
            get;
            set;
        }

        = string.Empty;

        public string GMotherRel
        {
            get;
            set;
        }

        = string.Empty;

        /// <summary>
        /// Gets the note collection reference.
        /// </summary>
        /// <value>
        /// The note collection reference.
        /// </value>

        public HLinkNoteModelCollection GNoteCollectionReference
        {
            get; set;
        }

        = new HLinkNoteModelCollection();

        public string RelationShips
        {
            get
            {
                if (!string.IsNullOrEmpty(GFatherRel) & !string.IsNullOrEmpty(GMotherRel))
                {
                    return $"{GFatherRel}-{GMotherRel}";
                }

                return string.Empty;
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateDetail(new ChildRefDetailPage(this));
        }
    }
}