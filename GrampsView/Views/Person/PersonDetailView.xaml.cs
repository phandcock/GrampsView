// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.Person;

namespace GrampsView.Views
{
    public partial class PersonDetailPage : ViewBasePage
    {
        private PersonDetailViewModel _viewModel;

        public PersonDetailPage()
        {
            InitializeComponent();
        }

        public PersonDetailPage(IHLinkBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<PersonDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }
    }
}