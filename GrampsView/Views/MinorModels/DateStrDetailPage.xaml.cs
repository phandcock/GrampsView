using GrampsView.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace GrampsView.Views
{
    public partial class DateStrDetailPage : ViewBasePage
    {
        public DateStrDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<DateStrDetailViewModel>();
        }

        private DateStrDetailViewModel _viewModel { get; set; }
    }
}