// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.Tags;

using Microsoft.Extensions.DependencyInjection;

namespace GrampsView.Views
{
    public partial class TagDetailPage : ViewBasePage
    {
        public TagDetailPage()
        {
            InitializeComponent();
        }

        public TagDetailPage(IHLinkBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<TagDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }

        private TagDetailViewModel _viewModel { get; set; }
    }
}