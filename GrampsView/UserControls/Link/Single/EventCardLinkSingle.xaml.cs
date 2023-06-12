// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class EventCardLinkSingle : LinkSingleCardControlTemplate
    {
        public EventCardLinkSingle()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("EventCardLinkSingle", args);
        }
    }
}