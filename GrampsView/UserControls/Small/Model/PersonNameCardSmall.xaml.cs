// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class PersonNameCardSmall : SmallCardControlTemplate
    {
        public PersonNameCardSmall()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("PersonNameCardSmall", args);
        }
    }
}