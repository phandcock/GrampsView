// Copyright (c) phandcock.  All rights reserved.

using GrampsView.ViewModels.Places;

namespace GrampsView.Views
{
    public sealed partial class PlaceDetailPage : ViewBasePage
    {
        public PlaceDetailPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = Ioc.Default.GetRequiredService<PlaceDetailViewModel>();
        }

        private PlaceDetailViewModel _viewModel { get; set; }
    }
}