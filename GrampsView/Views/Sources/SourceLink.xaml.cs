// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.Sources;
using GrampsView.Views;

namespace GrampsView.UserControls
{
    public partial class SourceLink : ViewBasePage
    {
        public SourceLink()
        {
            InitializeComponent();
        }

        public SourceLink(IHLinkBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<SourceDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }

        private SourceDetailViewModel _viewModel { get; set; }
    }
}