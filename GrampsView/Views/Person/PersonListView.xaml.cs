// Copyright (c) phandcock.  All rights reserved.

using GrampsView.ViewModels;

namespace GrampsView.Views
{
    public sealed partial class PersonListPage : ViewBasePage
    {
        public PersonListPage()
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<PersonListViewModel>();

            BindingContext = _viewModel;
        }

        private PersonListViewModel _viewModel { get; set; }
    }
}