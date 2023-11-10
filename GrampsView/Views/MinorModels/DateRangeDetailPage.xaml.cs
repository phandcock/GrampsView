// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.MinorModels;

namespace GrampsView.Views
{
    public partial class DateRangeDetailPage : ViewBasePage
    {
        public DateRangeDetailPage()
        {
            InitializeComponent();
        }

        public DateRangeDetailPage(IHLinkDBBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<DateRangeDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }

        private DateRangeDetailViewModel _viewModel { get; set; }
    }
}