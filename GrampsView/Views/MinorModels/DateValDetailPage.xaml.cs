namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class DateValDetailPage : ViewBase
    {
        public DateValDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}