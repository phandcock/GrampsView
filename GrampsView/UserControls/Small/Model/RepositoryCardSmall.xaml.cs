// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class RepositoryCardSmall : SmallCardControlTemplate
    {
        public RepositoryCardSmall()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("RepositoryCardSmall", args);
        }
    }
}