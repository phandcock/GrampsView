//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="HLinkFamilyModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

// XML 171 - All fields defined
//
// HLink

/// <summary>
/// </summary>
namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    using GrampsView.Data.DataView;

    /// <summary>
    /// HLink to the Family Model.
    /// </summary>
    [DataContract]
    public class HLinkFamilyModel : HLinkBase, IHLinkFamilyModel
    {
        /// <summary>
        /// Gets.
        /// </summary>
        public FamilyModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return new FamilyDataView().GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return new FamilyModel();
                }
            }
        }
    }
}