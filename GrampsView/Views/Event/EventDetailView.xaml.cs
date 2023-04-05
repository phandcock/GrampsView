// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.Event;

namespace GrampsView.Views
{
    public partial class EventDetailPage : ViewBasePage
    {
        public EventDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<EventDetailViewModel>();
        }

        public EventDetailPage(IHLinkBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<EventDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }

        private EventDetailViewModel _viewModel { get; set; }
    }
}