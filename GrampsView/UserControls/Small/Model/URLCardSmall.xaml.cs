// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class URLCardSmall : SmallCardControlTemplate
    {
        public URLCardSmall()
        {
            InitializeComponent();
        }
        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("URLCardSmall", args);
        }
    }
}