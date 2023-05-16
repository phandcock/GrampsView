// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Views;

namespace GrampsView.UserControls
{
    public partial class ChildRefCardSmall : SmallCardControlTemplate
    {
        public ChildRefCardSmall()
        {
            InitializeComponent();
        }

        void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            Navigation.PushAsync(new ChildRefDetailPage(args.Parameter as HLinkChildRefModel));
        }
    }
}