namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class DateStrDetailPage : ViewBase
    {
        private DateStrDetailViewModel _viewModel { get; set; }

        public DateStrDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<DateStrDetailViewModel>();
        }
    }
}