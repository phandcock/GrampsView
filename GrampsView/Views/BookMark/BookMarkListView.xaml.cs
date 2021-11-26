namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class BookMarkListPage : ViewBase
    {
        private BookMarkListPage _viewModel { get; set; }

        public BookMarkListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<BookMarkListPage>();
        }
    }
}