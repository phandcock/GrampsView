namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    public sealed partial class BookMarkListPage : ViewBasePage
    {
        public BookMarkListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetService<BookMarkListViewModel>();
        }

        private BookMarkListViewModel _viewModel { get; set; }
    }
}