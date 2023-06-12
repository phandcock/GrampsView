// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class PlaceLocationCardSmall : SmallCardControlTemplate
    {
        public PlaceLocationCardSmall()
        {
            InitializeComponent();
        }
        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("PlaceLocationCardSmall", args);
        }
    }
}