namespace GrampsView.Views
{
    public partial class FamilyDetailPage : ViewBase
    {
        public FamilyDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}