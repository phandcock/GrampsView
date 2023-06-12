// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class PersonCardSmall : SmallCardControlTemplate
    {
        public PersonCardSmall()
        {
            InitializeComponent();
        }
        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("PersonCardSmall", args);
        }
    }
}