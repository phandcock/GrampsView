// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class SmallCardControlTemplate : UControlTemplateBase
    {
        public SmallCardControlTemplate()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("SmallCardControlTemplate", args);
        }
    }
}