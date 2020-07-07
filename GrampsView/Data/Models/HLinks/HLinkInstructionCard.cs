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

    public class HLinkInstructionCard : HLinkBase
    {
        public string CardText { get; set; }
    }
}