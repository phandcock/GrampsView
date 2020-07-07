// <copyright file="HLinkAdressModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

// XML 171 - Not in definition so created this for use with BackLink functionality
//
// HLink

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    using GrampsView.Data.DataView;

    /// <summary>
    /// HLink to an Address model.
    /// </summary>
    [DataContract]
    public class HLinkAdressModel : HLinkBase, IHLinkAddressModel
    {
        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public AddressModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.AddressDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return null;
                }
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
            HLinkAdressModel arg = obj as HLinkAdressModel;

            // Null objects go first
            if (arg is null) { return 1; }

            // Can only comapre if they are the same type so assume equal
            if (arg.GetType() != typeof(HLinkAdressModel))
            {
                return 0;
            }

            return DeRef.CompareTo(arg.DeRef);
        }
    }
}