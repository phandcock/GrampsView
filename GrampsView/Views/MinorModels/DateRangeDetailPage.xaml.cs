﻿using GrampsView.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace GrampsView.Views
{
    public partial class DateRangeDetailPage : ViewBasePage
    {
        public DateRangeDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetService<DateRangeDetailViewModel>();
        }

        private DateRangeDetailViewModel _viewModel { get; set; }
    }
}