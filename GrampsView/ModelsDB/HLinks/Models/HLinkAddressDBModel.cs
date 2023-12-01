// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Models.DataModels.Minor;
using GrampsView.Models.HLinks;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// HLink to an Address model. Not in XML Schema so use the standard hlink
    /// </summary>

    public class HLinkAddressDBModel : HLinkDBBase, IHLinkAddressDBModel
    {
        private AddressDBModel _Deref = new AddressDBModel();

        private bool DeRefCached;

        public HLinkAddressDBModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconAddress;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundAddress");
        }

        /// <summary>
        /// Gets and sets the address model.
        /// </summary>
        /// <value>
        /// The address model.
        /// </value>
        public AddressDBModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DL.AddressDL.GetModelFromHLinkKey(HLinkKey);

                    if (_Deref.Valid)
                    {
                        DeRefCached = true;
                    }
                }

                if (_Deref is null)
                {
                    _Deref = new AddressDBModel();
                }

                return _Deref;
            }
        }

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
        /// Compares to. Bases it on the HLInkKey for want of anything else that makes sense.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public new int CompareTo(object obj)
        {
            HLinkAddressDBModel arg = obj as HLinkAddressDBModel;

            // Null objects go first
            if (arg is null)
            {
                return 1;
            }

            // Can only comapre if they are the same type so assume equal
            if (arg.GetType() != typeof(HLinkAddressDBModel))
            {
                return 0;
            }

            return DeRef.CompareTo(arg.DeRef);
        }
    }
}