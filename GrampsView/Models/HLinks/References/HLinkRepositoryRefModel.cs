// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.HLinks;

using System.Text.Json.Serialization;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// <para> Repository HLink. </para>
    /// <para> XML 1.71 check done </para>
    /// </summary>

    public class HLinkRepositoryRefModel : HLinkBase, IHLinkRepositoryRef
    {
        private RepositoryModel _Deref = new RepositoryModel();

        private bool DeRefCached;

        public HLinkRepositoryRefModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconRepository;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundRepository");
        }

        /// <summary>
        /// Gets the dereference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        [JsonIgnore]
        public RepositoryModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.RepositoryDV.GetModelFromHLinkKey(HLinkKey);

                    if (_Deref.Valid)
                    {
                        DeRefCached = true;
                    }
                }

                return _Deref;
            }
        }

        /// <summary>
        /// Gets or sets the call no.
        /// </summary>
        /// <value>
        /// The call no.
        /// </value>

        public string GCallNo
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the medium.
        /// </summary>
        /// <value>
        /// The medium.
        /// </value>

        public string GMedium
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the note reference.
        /// </summary>
        /// <value>
        /// The note reference.
        /// </value>

        public HLinkNoteDBModelCollection GNoteRef
        {
            get;
            set;
        }

        = new HLinkNoteDBModelCollection();

        public override string ToString()
        {
            if (DeRef.Valid)
            {
                return DeRef.ToString();
            };

            return "~None";
        }


    }
}