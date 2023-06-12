// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class LDSOrdCardSmall : SmallCardControlTemplate
    {
        public LDSOrdCardSmall()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("LDSOrdCardSmall", args);
        }
    }
}