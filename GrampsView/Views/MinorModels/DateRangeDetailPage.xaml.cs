namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class DateRangeDetailPage : ViewBasePage
    {
        private DateRangeDetailViewModel _viewModel { get; set; }

        public DateRangeDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = Ioc.Default.GetService<DateRangeDetailViewModel>();
        }
    }
}