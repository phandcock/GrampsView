namespace GrampsView.Views
{
    public partial class PlaceDetailPage : ViewBase
    {
        public PlaceDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}