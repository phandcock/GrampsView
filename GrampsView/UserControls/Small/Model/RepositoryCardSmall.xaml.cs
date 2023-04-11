// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Views;

namespace GrampsView.UserControls
{
    public partial class RepositoryCardSmall : SmallCardControlTemplate
    {
        public RepositoryCardSmall()
        {
            InitializeComponent();
        }

        void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            Navigation.PushAsync(new RepositoryDetailPage(args.Parameter as HLinkRepositoryModel));
        }
    }
}