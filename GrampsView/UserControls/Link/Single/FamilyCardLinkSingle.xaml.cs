// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class FamilyCardLinkSingle : LinkSingleCardControlTemplate
    {
        public FamilyCardLinkSingle()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("FamilyCardLinkSingle", args);
        }
    }
}