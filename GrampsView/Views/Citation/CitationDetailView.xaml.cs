// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.Citation;

namespace GrampsView.Views
{
    public partial class CitationDetailPage : ViewBasePage
    {
        private CitationDetailViewModel _viewModel;

        public CitationDetailPage()
        {
            InitializeComponent();
        }

        public CitationDetailPage(IHLinkBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<CitationDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }
    }
}