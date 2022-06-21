namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class DateSpanDetailPage : ViewBasePage
    {
        private DateSpanDetailViewModel _viewModel { get; set; }

        public DateSpanDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<DateSpanDetailViewModel>();
        }
    }
}