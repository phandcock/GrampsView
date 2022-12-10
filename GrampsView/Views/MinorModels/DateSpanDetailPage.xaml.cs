using GrampsView.ViewModels.MinorModels;

using Microsoft.Extensions.DependencyInjection;

namespace GrampsView.Views
{
    public partial class DateSpanDetailPage : ViewBasePage
    {
        public DateSpanDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<DateSpanDetailViewModel>();
        }

        private DateSpanDetailViewModel _viewModel { get; set; }
    }
}