// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class LinkSingleCardControlTemplate : UControlTemplateBase
    {
        public LinkSingleCardControlTemplate()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("LinkSingleCardControlTemplate", args);
        }
    }
}