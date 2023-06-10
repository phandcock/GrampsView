// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPageControlTemplate : UControlTemplateBase
    {
        public DetailPageControlTemplate()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("DetailPageControlTemplate", args);
        }
    }
}