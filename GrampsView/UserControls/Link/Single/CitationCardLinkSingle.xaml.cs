// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class CitationCardLinkSingle : LinkSingleCardControlTemplate
    {
        public CitationCardLinkSingle()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("CitationCardLinkSingle", args);
        }
    }
}