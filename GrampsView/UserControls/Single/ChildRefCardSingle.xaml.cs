// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class ChildRefCardSingle : SingleCardControlTemplate
    {
        public ChildRefCardSingle()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("ChildRefCardSingle", args);
        }
    }
}