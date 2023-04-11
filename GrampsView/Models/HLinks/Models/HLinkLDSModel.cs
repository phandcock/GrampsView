// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Models.HLinks;

using System.Threading.Tasks;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// HLink to a URL
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

    public class HLinkLDSModel : HLinkBase, IHLinkLDSModel
    {
        public HLinkLDSModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconURL;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
        }

        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>

        public LdsOrdModel DeRef
        {
            get;

            set;
        } = new LdsOrdModel();

        public override bool Valid
        {
            get
            {
                if (!HLinkGlyphItem.Valid)
                {
                }

                return HLinkGlyphItem.Valid;
            }
        }

        /// <summary>
        /// No detail page to navigate to, just open the URL externally.
        /// </summary>
        public override async Task UCNavigate()
        {
            // TODO finish this await DeRef.OpenURL();
            return;
        }
    }
}