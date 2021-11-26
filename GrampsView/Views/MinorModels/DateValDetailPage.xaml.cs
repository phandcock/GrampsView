namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class DateValDetailPage : ViewBase
    {
        private DateValDetailViewModel _viewModel { get; set; }

        public DateValDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<DateValDetailViewModel>();
        }
    }
}