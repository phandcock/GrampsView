// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Models.HLinks;
using GrampsView.Views;

using System.Text.Json.Serialization;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Data model for a source reference.
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

    public class HLinkSourceModel : HLinkBase, IHLinkSourceModel
    {
        private SourceModel _Deref = new();

        private bool DeRefCached;

        public HLinkSourceModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconSource;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundSource");
        }

        /// <summary>
        /// Gets the model from the HLink.
        /// </summary>
        /// <value>
        /// The HLink reference.
        /// </value>
        [JsonIgnore]
        public SourceModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.SourceDV.GetModelFromHLinkKey(HLinkKey);

                    if (_Deref.Valid)
                    {
                        DeRefCached = true;
                    }
                }

                return _Deref;
            }
        }

        public override Page NavigationPage()
        {
            return new SourceDetailPage(this);
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="argOobj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public int CompareTo(HLinkSourceModel argOobj)
        {
            return DeRef.CompareTo(argOobj);
        }

        /// <summary>Compares to.</summary>
        /// <param name="obj"></param>
        /// <returns>
        ///   <br />
        /// </returns>
        public new int CompareTo(object obj)
        {
            // Null objects go first
            if (obj is null)
            {
                return 1;
            }

            // Can only compare if they are the same type so assume equal
            return obj.GetType() != typeof(HLinkSourceModel) ? 0 : DeRef.CompareTo((obj as HLinkSourceModel).DeRef);
        }


    }
}