using GrampsView.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace GrampsView.Views
{
    public partial class DateSpanDetailPage : ViewBasePage
    {
        public DateSpanDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetService<DateSpanDetailViewModel>();
        }

        private DateSpanDetailViewModel _viewModel { get; set; }
    }
}