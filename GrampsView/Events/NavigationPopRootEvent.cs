﻿// Copyright (c) phandcock.  All rights reserved.

using CommunityToolkit.Mvvm.Messaging.Messages;

namespace GrampsView.Events
{
    public class NavigationPopRootEvent : ValueChangedMessage<bool>
    {
        public NavigationPopRootEvent(bool value) : base(value)
        {
        }
    }
}