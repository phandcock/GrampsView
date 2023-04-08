// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.Repository;

namespace GrampsView.Views
{
    public partial class RepositoryDetailPage : ViewBasePage
    {
        public RepositoryDetailPage()
        {
            InitializeComponent();
        }

        public RepositoryDetailPage(IHLinkBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<RepositoryDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }

        private RepositoryDetailViewModel _viewModel { get; set; }
    }
}