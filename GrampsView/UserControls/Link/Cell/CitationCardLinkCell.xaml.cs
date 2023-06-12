// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class CitationCardLinkCell : LinkCellCardControlTemplate
    {
        public CitationCardLinkCell()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("CitationCardLinkCell", args);
        }
    }
}