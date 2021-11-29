namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class PlaceListPage : ViewBase
    {
        private BookMarkListPage _viewModel { get; set; }

        public PlaceListPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}