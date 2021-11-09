namespace GrampsView.Views
{
    public partial class PersonNameDetailPage : ViewBase
    {
        public PersonNameDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}