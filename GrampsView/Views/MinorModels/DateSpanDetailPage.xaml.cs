// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.MinorModels;

namespace GrampsView.Views
{
    public partial class DateSpanDetailPage : ViewBasePage
    {
        public DateSpanDetailPage()
        {
            InitializeComponent();
        }

        public DateSpanDetailPage(IHLinkBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<DateSpanDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }

        private DateSpanDetailViewModel _viewModel { get; set; }
    }
}