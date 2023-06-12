// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class RepositoryCardLinkSingle : LinkSingleCardControlTemplate
    {
        public RepositoryCardLinkSingle()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("RepositoryCardLinkSingle", args);
        }
    }
}