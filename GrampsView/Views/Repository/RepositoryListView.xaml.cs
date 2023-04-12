// Copyright (c) phandcock.  All rights reserved.

using GrampsView.ViewModels.Repository;

namespace GrampsView.Views
{
    public sealed partial class RepositoryListPage : ViewBasePage
    {
        private RepositoryListViewModel _viewModel { get; set; }

        public RepositoryListPage()
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<RepositoryListViewModel>();

            BindingContext = _viewModel;
        }
    }
}