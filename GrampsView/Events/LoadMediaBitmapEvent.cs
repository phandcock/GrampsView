//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="GRAMPSLoadMediaBitmapEvent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Events
{
    using GrampsView.Data.Model;
    using Prism.Events;

    /// <summary>
    /// Pub Sub Gramps Data Loaded Event.
    /// </summary>
    public class LoadMediaBitmapEvent : PubSubEvent<HLinkMediaModel>
    {
    }
}