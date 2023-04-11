// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Views;

namespace GrampsView.UserControls
{
    public partial class SmallCardControlTemplate : ContentView
    {
        public SmallCardControlTemplate()
        {
            InitializeComponent();
        }

        void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            Navigation.PushAsync(new PersonDetailPage());
        }
    }
}