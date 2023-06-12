// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class RepositoryRefCardSmall : SmallCardControlTemplate
    {
        public RepositoryRefCardSmall()
        {
            InitializeComponent();
        }
        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("RepositoryRefCardSmall", args);
        }
    }
}