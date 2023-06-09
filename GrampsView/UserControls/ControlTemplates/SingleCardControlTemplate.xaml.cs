// Copyright (c) phandcock.  All rights reserved.

using SharedSharp.Errors.Interfaces;

namespace GrampsView.UserControls
{
    public partial class SingleCardControlTemplate : ContentView
    {
        public SingleCardControlTemplate()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("SingleCardControlTemplate-OnTapGestureRecognizerTapped", new NotImplementedException());
        }
    }
}