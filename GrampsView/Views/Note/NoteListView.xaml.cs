namespace GrampsView.Views
{
    public partial class NoteListPage : ViewBase
    {
        public NoteListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}