namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class DateRangeDetailPage : ViewBase
    {
        private DateRangeDetailViewModel _viewModel { get; set; }

        public DateRangeDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<DateRangeDetailViewModel>();
        }
    }
}