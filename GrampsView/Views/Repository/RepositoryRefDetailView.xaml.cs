// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.Repository;

using Microsoft.Extensions.DependencyInjection;

namespace GrampsView.Views
{
    public partial class RepositoryRefDetailPage : ViewBasePage
    {
        public RepositoryRefDetailPage()
        {
            InitializeComponent();
        }

        public RepositoryRefDetailPage(IHLinkBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<RepositoryRefDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }

        private RepositoryRefDetailViewModel _viewModel { get; set; }
    }
}