// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class MediaCardSmall : SmallCardControlTemplate
    {
        public MediaCardSmall()
        {
            InitializeComponent();
        }
        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("MediaCardSmall", args);
        }
    }
}