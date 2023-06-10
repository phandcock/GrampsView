// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class SingleCardControlTemplate : UControlTemplateBase
    {
        public SingleCardControlTemplate()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("SingleCardControlTemplate", args);
        }
    }
}