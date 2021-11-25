namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class DateStrDetailPage : ViewBase
    {
        public DateStrDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}