// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class LargeCardControlTemplate : UControlTemplateBase
    {
        public LargeCardControlTemplate()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("LargeCardControlTemplate", args);
        }
    }
}