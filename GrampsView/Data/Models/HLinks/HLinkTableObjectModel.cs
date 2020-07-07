//-----------------------------------------------------------------------
//
// Various note models
//
// <copyright file="HLinkTableObjectModel.cs" company="PlaceholderCompany">
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
    public class HLinkTableObjectModel : HLinkBase, IHLinkTableObjectModel
    {
    }
}