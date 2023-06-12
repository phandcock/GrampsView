// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class LinkLargeCardControlTemplate : UControlTemplateBase
    {
        public LinkLargeCardControlTemplate()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("LinkLargeCardControlTemplate", args);
        }
    }
}