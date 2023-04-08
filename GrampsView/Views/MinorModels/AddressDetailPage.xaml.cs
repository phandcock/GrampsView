// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.MinorModels;

namespace GrampsView.Views
{
    public partial class AddressDetailPage : ViewBasePage
    {
        public AddressDetailPage()
        {
            InitializeComponent();
        }

        public AddressDetailPage(IHLinkBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<AddressDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }

        private AddressDetailViewModel _viewModel { get; set; }
    }
}