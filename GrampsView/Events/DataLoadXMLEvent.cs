//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="DataLoadXMLEvent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Events
{
    using Prism.Events;

    /// <summary>
    /// Pub Sub Gramps Data Loaded Event.
    /// </summary>
    public class DataLoadXMLEvent : PubSubEvent<object>
    {
    }
}