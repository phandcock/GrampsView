// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Date;

using System.Text.Json.Serialization;

namespace GrampsView.Models.HLinks.Models
{
    /// <summary>
    /// <para> HLink to a Place Model <font color="#333333"> </font> </para>
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
    /// <para> <br/> </para>
    /// </summary>

    public class HLinkPlaceModel : HLinkBase, IHLinkPlaceModel
    {
        private PlaceModel _Deref = new PlaceModel();

        private bool DeRefCached = false;

        public HLinkPlaceModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconPlace;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPlace");
        }

        /// <summary>
        /// Gets or sets the optional place date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateObjectModelBase Date { get; set; } = new DateObjectModelVal();

        [JsonIgnore]
        public PlaceModel DeRef
        {
            get
            {
                if (Valid && !DeRefCached)
                {
                    _Deref = DV.PlaceDV.GetModelFromHLinkKey(HLinkKey);

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