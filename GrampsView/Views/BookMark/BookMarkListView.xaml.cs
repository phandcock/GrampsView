namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    public sealed partial class BookMarkListPage : ViewBasePage
    {
        public BookMarkListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<BookMarkListViewModel>();
        }

        private BookMarkListViewModel _viewModel { get; set; }
    }
}