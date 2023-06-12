// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class SourceCardMediumLink : LinkMediumCardControlTemplate
    {
        public SourceCardMediumLink()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("SourceCardMediumLink", args);
        }
    }
}