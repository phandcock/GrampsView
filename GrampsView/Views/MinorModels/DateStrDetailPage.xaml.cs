// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.MinorModels;

using Microsoft.Extensions.DependencyInjection;

namespace GrampsView.Views
{
    public partial class DateStrDetailPage : ViewBasePage
    {
        public DateStrDetailPage()
        {
            InitializeComponent();

        }

        private DateStrDetailViewModel _viewModel { get; set; }

        public DateStrDetailPage(IHLinkBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<DateStrDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }
    }
}