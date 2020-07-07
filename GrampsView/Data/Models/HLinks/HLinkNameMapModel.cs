// <copyright file="HLinkNameMapModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    using GrampsView.Data.DataView;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkNameMapModel : HLinkBase, IHLinkNameMapModel
    {
        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public NameMapModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return new NameMapDataView().GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}