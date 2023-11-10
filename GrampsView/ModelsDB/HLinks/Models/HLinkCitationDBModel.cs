// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.DBModels;
using GrampsView.Models.HLinks;
using GrampsView.ModelsDB.HLinks.Interfaces;
using GrampsView.Views;

using System.Text.Json.Serialization;

namespace GrampsView.ModelsDB.HLinks.Models
{
    /// <summary>
    /// Data model for a Citation HLink.
    /// <list type="table">
    /// <listheader>
    /// <term> Item </term>
    /// <term> Status </term>
    /// </listheader>
    /// <item>
    /// <description> XML 1.71 check </description>
    /// <description> Done </description>
    /// </item>
    /// </list>
    /// </summary>

    public class HLinkCitationDBModel : HLinkDBBase, IHLinkCitationDBModel
    {
        private CitationDBModel _Deref = new CitationDBModel();

        private bool DeRefCached = false;

        public HLinkCitationDBModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconCitation;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundCitation");
        }

        public override Page NavigationPage()
        {
            return new CitationDetailPage(this);
        }

        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        [JsonIgnore]
        public CitationDBModel DeRef
        {
            get
            {
                if (Valid && !DeRefCached)
                {
                    _Deref = DL.CitationDL.GetModelFromHLinkKey(HLinkKey);

                    if (_Deref.Valid)
                    {
                        DeRefCached = true;
                    }
                }

                return _Deref;
            }
        }

        /// <summary>
        /// CompareTo. Bases it on the HLinkKey for want of anything else that makes sense.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public new int CompareTo(object obj)
        {
            // Null objects go first
            if (obj is null)
            {
                return 1;
            }

            // Can only compare if they are the same type so assume equal
            if (obj.GetType() != typeof(HLinkCitationDBModel))
            {
                return 0;
            }

            return DeRef.CompareTo((obj as HLinkCitationDBModel).DeRef);
        }


    }
}