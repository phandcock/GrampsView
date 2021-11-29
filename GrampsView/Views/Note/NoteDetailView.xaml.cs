namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class NoteDetailPage : ViewBase
    {
        private BookMarkListPage _viewModel { get; set; }

        public NoteDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}