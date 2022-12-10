namespace GrampsView.Views
{
    using GrampsView.ViewModels.MinorModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class DateValDetailPage : ViewBasePage
    {
        private DateValDetailViewModel _viewModel { get; set; }

        public DateValDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = Ioc.Default.GetRequiredService<DateValDetailViewModel>();
        }
    }
}