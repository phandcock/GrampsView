namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class PlaceDetailPage : ViewBase
    {
        private BookMarkListPage _viewModel { get; set; }

        public PlaceDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}