//-----------------------------------------------------------------------
//
//
//
// <copyright file="GRAMPSPersonCurrentChanged.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Events
{
    using GrampsView.Data.Model;
    using Prism.Events;

    /// <summary>
    /// update the progress ring text.
    /// </summary>
    public class PersonCurrentChanged : PubSubEvent<HLinkPersonModel>
    {
    }
}