// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.Places;

namespace GrampsView.Views
{
    public sealed partial class PlaceDetailPage : ViewBasePage
    {
        public PlaceDetailPage()
        {
            InitializeComponent();
        }

        public PlaceDetailPage(IHLinkBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<PlaceDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }

        private PlaceDetailViewModel _viewModel { get; set; }
    }
}