// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class MapCardSmall : SmallCardControlTemplate
    {
        public MapCardSmall()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("MapCardSmall", args);
        }
    }
}