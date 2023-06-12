// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class MediaCardLinkSingle : LinkSingleCardControlTemplate
    {
        public MediaCardLinkSingle()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("MediaCardLinkSingle", args);
        }
    }
}