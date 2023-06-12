// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class SourceCardLinkSingle : LinkSingleCardControlTemplate
    {
        public SourceCardLinkSingle()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("SourceCardLinkSingle", args);
        }
    }
}