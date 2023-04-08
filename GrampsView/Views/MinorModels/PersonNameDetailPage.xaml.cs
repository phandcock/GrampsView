// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.MinorModels;

using Microsoft.Extensions.DependencyInjection;

namespace GrampsView.Views
{
    public partial class PersonNameDetailPage : ViewBasePage
    {
        private PersonNameDetailViewModel _viewModel { get; set; }

        public PersonNameDetailPage()
        {
            InitializeComponent();
        }

        public PersonNameDetailPage(IHLinkBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<PersonNameDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }
    }
}