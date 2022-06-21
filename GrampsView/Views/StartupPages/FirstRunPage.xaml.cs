﻿namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class FirstRunPage : ViewBasePage
    {
        private FirstRunViewModel _viewModel { get; set; }

        public FirstRunPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<FirstRunViewModel>();
        }
    }
}