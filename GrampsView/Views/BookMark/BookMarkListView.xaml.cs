namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class BookMarkListPage : ViewBasePage
    {
        private BookMarkListViewModel _viewModel { get; set; }

        public BookMarkListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<BookMarkListViewModel>();
        }
    }
}