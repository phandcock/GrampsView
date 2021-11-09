namespace GrampsView.Views
{
    public partial class ChildRefDetailPage : ViewBase
    {
        public ChildRefDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}