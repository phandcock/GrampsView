namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class NoteListPage : ViewBase
    {
        private BookMarkListPage _viewModel { get; set; }

        public NoteListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}