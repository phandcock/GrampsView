namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class PlaceListPage : ViewBase
    {
        private PlaceListViewModel _viewModel { get; set; }

        public PlaceListPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = App.Current.Services.GetService<PlaceListViewModel>();
        }
    }
}