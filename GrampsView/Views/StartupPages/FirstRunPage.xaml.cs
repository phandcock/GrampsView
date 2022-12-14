﻿using GrampsView.ViewModels.StartupPages;

using System.Diagnostics;

namespace GrampsView.Views.StartupPages
{
    public sealed partial class FirstRunPage : ViewBasePage
    {
        public FirstRunPage()
        {

            Debug.WriteLine($"WhatsNewPage creation");

            InitializeComponent();
            BindingContext = Ioc.Default.GetRequiredService<FirstRunViewModel>();
        }


    }
}