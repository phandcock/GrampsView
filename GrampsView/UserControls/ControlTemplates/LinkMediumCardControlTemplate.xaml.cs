// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class LinkMediumCardControlTemplate : UControlTemplateBase
    {
        public LinkMediumCardControlTemplate()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("LinkMediumCardControlTemplate", args);
        }
    }
}