// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.Sources;

using Microsoft.Extensions.DependencyInjection;

namespace GrampsView.Views
{
    public partial class SourceDetailPage : ViewBasePage
    {
        public SourceDetailPage()
        {
            InitializeComponent();
        }

        public SourceDetailPage(IHLinkBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<SourceDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }

        private SourceDetailViewModel _viewModel { get; set; }
    }
}