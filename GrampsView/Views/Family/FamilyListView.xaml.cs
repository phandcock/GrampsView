namespace GrampsView.Views
{
    public sealed partial class FamilyListPage : ViewBase
    {
        public FamilyListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}