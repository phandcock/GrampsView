// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels;

namespace GrampsView.Views
{
    public sealed partial class PersonBirthdayPage : ViewBasePage
    {
        public PersonBirthdayPage()
        {
            InitializeComponent();
        }

        public PersonBirthdayPage(IHLinkBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<PersonBirthdayViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }

        private PersonBirthdayViewModel _viewModel { get; set; }
    }
}