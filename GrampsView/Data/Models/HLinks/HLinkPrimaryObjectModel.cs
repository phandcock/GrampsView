//-----------------------------------------------------------------------
//
// Various note models
//
// <copyright file="HLinkPrimaryObjectModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkPrimaryObjectModel : HLinkBase, IHLinkPrimaryObjectModel
    {
    }
}