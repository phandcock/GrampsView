// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class FamilyCardSingle : SingleCardControlTemplate
    {
        public FamilyCardSingle()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("FamilyCardSingle", args);
        }
    }
}