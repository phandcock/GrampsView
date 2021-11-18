namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class PlaceDetailPage : ViewBase
    {
        public PlaceDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}