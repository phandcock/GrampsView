// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class LinkCellCardControlTemplate : UControlTemplateBase
    {
        public LinkCellCardControlTemplate()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("LinkCellCardControlTemplate", args);
        }
    }
}