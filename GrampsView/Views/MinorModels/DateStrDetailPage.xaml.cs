// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.MinorModels;

namespace GrampsView.Views
{
    public partial class DateStrDetailPage : ViewBasePage
    {
        public DateStrDetailPage()
        {
            InitializeComponent();

        }

        private DateStrDetailViewModel _viewModel { get; set; }

        public DateStrDetailPage(IHLinkDBBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<DateStrDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }
    }
}