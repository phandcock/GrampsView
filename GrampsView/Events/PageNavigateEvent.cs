//-----------------------------------------------------------------------
//
// Page Title changed event
//
// <copyright file="PageTitleChangedEvent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Events
{
    using Prism.Events;
    using Prism.Navigation;

    public class PageNavigateEvent : PubSubEvent<string>
    {
    }

    public class PageNavigateParmsEvent : PubSubEvent<INavigationParameters>
    {
    }
}