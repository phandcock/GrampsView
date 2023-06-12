// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class EventCardLinkCell : LinkCellCardControlTemplate
    {
        public EventCardLinkCell()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("EventCardLinkCell", args);
        }
    }
}