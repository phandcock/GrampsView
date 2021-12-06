﻿namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class PersonDetailPage : ViewBase
    {
        private PersonDetailViewModel _viewModel { get; set; }

        public PersonDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<PersonDetailViewModel>();
        }
    }
}