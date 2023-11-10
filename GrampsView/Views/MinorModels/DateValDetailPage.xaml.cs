// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.MinorModels;

namespace GrampsView.Views
{
    public partial class DateValDetailPage : ViewBasePage
    {
        public DateValDetailPage()
        {
            InitializeComponent();
        }

        public DateValDetailPage(IHLinkDBBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<DateValDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }

        private DateValDetailViewModel _viewModel { get; set; }
    }
}