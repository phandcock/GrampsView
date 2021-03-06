﻿// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkURLModel : HLinkBase, IHLinkURLModel
    {
        public HLinkURLModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconURL;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
        }

        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        [DataMember]
        public new URLModel DeRef
        {
            get;

            set;
        } = new URLModel();

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

        public override async Task UCNavigate()
        {
            await DeRef.OpenURL();
            return;
        }
    }
}