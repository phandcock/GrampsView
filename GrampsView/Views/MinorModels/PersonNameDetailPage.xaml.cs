namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class PersonNameDetailPage : ViewBase
    {
        private BookMarkListPage _viewModel { get; set; }

        public PersonNameDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}