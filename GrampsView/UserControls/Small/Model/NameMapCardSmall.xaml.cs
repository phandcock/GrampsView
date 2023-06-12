// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class NameMapCardSmall : SmallCardControlTemplate
    {
        public NameMapCardSmall()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("NameMapCardSmall", args);
        }
    }
}