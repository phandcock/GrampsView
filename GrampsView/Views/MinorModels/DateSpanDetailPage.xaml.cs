namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class DateSpanDetailPage : ViewBase
    {
        public DateSpanDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}