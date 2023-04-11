// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Views;

namespace GrampsView.UserControls
{
    public partial class PersonNameCardSmall : SmallCardControlTemplate
    {
        public PersonNameCardSmall()
        {
            InitializeComponent();
        }

        void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            Navigation.PushAsync(new PersonNameDetailPage(args.Parameter as HLinkPersonNameModel));
        }
    }
}