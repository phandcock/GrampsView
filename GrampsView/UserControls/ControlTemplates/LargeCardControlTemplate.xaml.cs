// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.HLinks;

namespace GrampsView.UserControls
{
    public partial class LargeCardControlTemplate : ContentView
    {
        public LargeCardControlTemplate()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            Ioc.Default.GetRequiredService<ILog>().Variable("OnTapGestureRecognizerTapped", args.Parameter.ToString(), Microsoft.Extensions.Logging.LogLevel.Trace);

            Navigation.PushAsync((args.Parameter as HLinkBase).NavigationPage());
        }
    }
}