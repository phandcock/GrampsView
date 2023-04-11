// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Views;

namespace GrampsView.UserControls
{
    public partial class FamilyCardSmall : SmallCardControlTemplate
    {
        public FamilyCardSmall()
        {
            InitializeComponent();
        }

        void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            Navigation.PushAsync(new FamilyDetailPage(args.Parameter as HLinkFamilyModel));
        }
    }
}