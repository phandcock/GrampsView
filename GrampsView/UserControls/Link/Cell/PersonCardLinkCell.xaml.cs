// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Views;

namespace GrampsView.UserControls
{
    public partial class PersonCardLinkCell : LinkCellCardControlTemplate
    {
        public PersonCardLinkCell()
        {
            InitializeComponent();


        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            Navigation.PushAsync(new PersonDetailPage(args.Parameter as HLinkPersonModel));
        }
    }
}