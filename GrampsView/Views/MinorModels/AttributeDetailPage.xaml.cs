namespace GrampsView.Views
{
    public partial class AttributeDetailPage : ViewBase
    {
        public AttributeDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}