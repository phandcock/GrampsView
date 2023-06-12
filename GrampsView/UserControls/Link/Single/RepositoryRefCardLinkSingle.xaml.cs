// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class RepositoryRefCardLinkSingle : LinkSingleCardControlTemplate
    {
        public RepositoryRefCardLinkSingle()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("RepositoryRefCardLinkSingle", args);
        }
    }
}