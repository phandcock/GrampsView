// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class FamilyCardSmall : SmallCardControlTemplate
    {
        public FamilyCardSmall()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("FamilyCardSmall", args);
        }
    }
}