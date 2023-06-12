// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class FamilyCardLinkCell : LinkCellCardControlTemplate
    {
        public FamilyCardLinkCell()
        {
            InitializeComponent();
        }
        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("FamilyCardLinkCell", args);
        }
    }
}