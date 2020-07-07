//-----------------------------------------------------------------------
//
//
//
// <copyright file="DataLoadStartEvent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Events
{
    using System;
    using System.Threading.Tasks;

    using Prism.Events;

    /// <summary>
    /// update the progress ring text.
    /// </summary>
    public class DataLoadStartEvent : PubSubEvent<bool>
    {
        //internal void Subscribe(Task task, ThreadOption backgroundThread) => throw new NotImplementedException();
    }
}