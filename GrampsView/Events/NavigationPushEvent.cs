// Copyright (c) phandcock.  All rights reserved.

using CommunityToolkit.Mvvm.Messaging.Messages;

namespace GrampsView.Events
{
    public class NavigationPushEvent : ValueChangedMessage<Page>
    {
        public NavigationPushEvent(Page value) : base(value)
        {
        }
    }
}