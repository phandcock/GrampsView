// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class AttributeCardSingle : SingleCardControlTemplate
    {
        public AttributeCardSingle()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("AttributeCardSingle", args);
        }
    }
}