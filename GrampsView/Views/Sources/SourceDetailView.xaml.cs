namespace GrampsView.Views
{
    public partial class SourceDetailPage : ViewBase
    {
        public SourceDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}