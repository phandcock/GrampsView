// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Views;

namespace GrampsView.UserControls
{
    public partial class RepositoryRefCardSmall : SmallCardControlTemplate
    {
        public RepositoryRefCardSmall()
        {
            InitializeComponent();
        }

        void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            Navigation.PushAsync(new RepositoryRefDetailPage(args.Parameter as HLinkRepositoryRefModel));
        }
    }
}