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

    public class HLinkSurnameModel : HLinkBase, IHLinkSurnameModel
    {
        public HLinkSurnameModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconDDefault;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
        }

        /// <summary>
        /// Gets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>

        public SurnameModel DeRef
        {
            get;

            set;
        } = new SurnameModel();

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

        public override string ToString()
        {
            if (DeRef != null)
            {
                return DeRef.DefaultTextShort;
            }

            return base.ToString();
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