//-----------------------------------------------------------------------
//
// Status CHanged Event
//
// <copyright file="GVProgressMajorTextUpdate.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Events
{
    using Prism.Events;

    /// <summary>
    /// Notification that the Status has changed.
    /// </summary>
    public class StatusUpdated : PubSubEvent<string>
    {
    }
}