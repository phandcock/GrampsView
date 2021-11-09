namespace GrampsView.Views
{
    public partial class PersonDetailPage : ViewBase
    {
        public PersonDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}