namespace GrampsView.Views
{
    public partial class CitationListPage : ViewBase
    {
        public CitationListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}