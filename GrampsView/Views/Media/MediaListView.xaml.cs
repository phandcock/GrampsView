// Copyright (c) phandcock.  All rights reserved.

using GrampsView.ViewModels.Media;

namespace GrampsView.Views
{
    public sealed partial class MediaListPage : ViewBasePage
    {
        public MediaListPage()
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<MediaListViewModel>();

            BindingContext = _viewModel;
        }

        private MediaListViewModel _viewModel { get; set; }
    }
}