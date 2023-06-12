// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class SourceCardLinkCell : LinkCellCardControlTemplate
    {
        public SourceCardLinkCell()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("SourceCardLinkCell", args);
        }
    }
}