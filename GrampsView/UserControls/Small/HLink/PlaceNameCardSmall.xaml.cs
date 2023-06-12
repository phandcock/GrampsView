// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class PlaceNameCardSmall : SmallCardControlTemplateNS
    {
        public PlaceNameCardSmall()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("PlaceNameCardSmall", args);
        }
    }
}