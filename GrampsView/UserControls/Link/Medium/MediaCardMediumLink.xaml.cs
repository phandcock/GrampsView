// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class MediaCardMediumLink : LinkMediumCardControlTemplate
    {
        public MediaCardMediumLink()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("MediaCardMediumLink", args);
        }
    }
}