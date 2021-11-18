﻿namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class PlaceListPage : ViewBase
    {
        public PlaceListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}