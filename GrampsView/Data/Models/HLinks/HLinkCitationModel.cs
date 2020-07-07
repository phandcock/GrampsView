// <copyright file="HLinkCitationModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

// XML 171 - All fields defined
//
// HLink

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    using GrampsView.Data.DataView;

    /// <summary>
    /// HLink to Citation Model.
    /// </summary>
    [DataContract]
    public class HLinkCitationModel : HLinkBase, IHLinkCitationModel
    {
        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public CitationModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.CitationDV.GetModelFromHLinkString(HLinkKey);
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
            // Null objects go first
            if (obj is null) { return 1; }

            // Can only compare if they are the same type so assume equal
            if (obj.GetType() != typeof(HLinkCitationModel))
            {
                return 0;
            }

            return DeRef.CompareTo((obj as HLinkCitationModel).DeRef);
        }
    }
}