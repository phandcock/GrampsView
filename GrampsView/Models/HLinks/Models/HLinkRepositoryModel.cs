// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Models.HLinks;

using System.Text.Json.Serialization;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// <para> Repository HLink. </para>
    /// <para> XML 1.71 check done </para>
    /// </summary>

    public class HLinkRepositoryModel : HLinkBase, IHLinkRepositoryModel
    {
        private RepositoryModel _Deref = new RepositoryModel();

        private bool DeRefCached = false;

        public HLinkRepositoryModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconRepository;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundRepository");
        }

        /// <summary>
        /// Gets the dereference.
        /// </summary>
        /// <value>
        /// The de reference.
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
    }
}