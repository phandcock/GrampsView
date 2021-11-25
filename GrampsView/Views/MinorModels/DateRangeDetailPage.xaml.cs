namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class DateRangeDetailPage : ViewBase
    {
        public DateRangeDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}