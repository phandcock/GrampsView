// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class CitationCardSmall : SmallCardControlTemplate
    {
        public CitationCardSmall()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("CitationCardSmall", args);
        }
    }
}