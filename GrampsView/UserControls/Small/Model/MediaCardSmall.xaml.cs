// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Views;

namespace GrampsView.UserControls
{
    public partial class MediaCardSmall : SmallCardControlTemplate
    {
        public MediaCardSmall()
        {
            InitializeComponent();
        }
        void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            Navigation.PushAsync(new MediaDetailPage(args.Parameter as HLinkMediaModel));
        }
    }
}