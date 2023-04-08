// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.Family;

using Microsoft.Extensions.DependencyInjection;

namespace GrampsView.Views
{
    public partial class FamilyDetailPage : ViewBasePage
    {
        public FamilyDetailPage()
        {
            InitializeComponent();
        }

        public FamilyDetailPage(IHLinkBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<FamilyDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }

        private FamilyDetailViewModel _viewModel { get; set; }
    }
}