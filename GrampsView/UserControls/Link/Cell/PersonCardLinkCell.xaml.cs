// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class PersonCardLinkCell : LinkCellCardControlTemplate
    {
        public PersonCardLinkCell()
        {
            InitializeComponent();


        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("PersonCardLinkCell", args);
        }
    }
}