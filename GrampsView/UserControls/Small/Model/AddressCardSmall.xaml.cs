// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class AddressCardSmall : SmallCardControlTemplate
    {
        public AddressCardSmall()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("AddressCardSmall", args);
        }
    }
}