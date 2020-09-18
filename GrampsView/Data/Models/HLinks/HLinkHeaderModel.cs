// <copyright file="HLinkHeaderModel.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    using GrampsView.Data.DataView;

    /// <summary>
    /// HLink to the Header model.
    /// </summary>

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkHeaderModel : HLinkBase, IHLinkHeaderModel
    {
        /// <summary>
        /// Gets the dereference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public HeaderModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return new HeaderDataView().GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}