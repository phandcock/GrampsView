// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class EventCardSmall : SmallCardControlTemplate
    {
        public EventCardSmall()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("EventCardSmall", args);
        }
    }
}