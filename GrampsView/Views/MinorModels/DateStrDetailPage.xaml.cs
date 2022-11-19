namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class DateStrDetailPage : ViewBasePage
    {
        private DateStrDetailViewModel _viewModel { get; set; }

        public DateStrDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = Ioc.Default.GetService<DateStrDetailViewModel>();
        }
    }
}