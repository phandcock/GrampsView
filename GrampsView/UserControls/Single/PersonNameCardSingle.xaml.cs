// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class PersonNameCardSingle : SingleCardControlTemplate
    {
        public PersonNameCardSingle()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("PersonNameCardSingle", args);
        }
    }
}