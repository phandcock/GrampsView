// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.Person;

using Microsoft.Extensions.DependencyInjection;

namespace GrampsView.Views
{
    public partial class ChildRefDetailPage : ViewBasePage
    {
        public ChildRefDetailPage()
        {
            InitializeComponent();
        }

        public ChildRefDetailPage(IHLinkBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<ChildRefDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }

        private ChildRefDetailViewModel _viewModel { get; set; }
    }
}