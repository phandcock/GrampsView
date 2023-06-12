// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class ChildRefCardSmall : SmallCardControlTemplate
    {
        public ChildRefCardSmall()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("ChildRefCardSmall", args);
        }
    }
}