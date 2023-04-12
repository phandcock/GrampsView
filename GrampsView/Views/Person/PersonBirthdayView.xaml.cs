// Copyright (c) phandcock.  All rights reserved.

using GrampsView.ViewModels;

namespace GrampsView.Views
{
    public sealed partial class PersonBirthdayPage : ViewBasePage
    {
        public PersonBirthdayPage()
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<PersonBirthdayViewModel>();

            BindingContext = _viewModel;
        }

        private PersonBirthdayViewModel _viewModel { get; set; }
    }
}