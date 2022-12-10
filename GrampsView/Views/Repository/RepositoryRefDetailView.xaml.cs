﻿namespace GrampsView.Views
{
    using GrampsView.ViewModels.Repository;

    using Microsoft.Extensions.DependencyInjection;

    public partial class RepositoryRefDetailPage : ViewBasePage
    {
        private RepositoryRefDetailViewModel _viewModel { get; set; }

        public RepositoryRefDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<RepositoryRefDetailViewModel>();
        }
    }
}