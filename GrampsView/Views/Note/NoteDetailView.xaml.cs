namespace GrampsView.Views
{
    public partial class NoteDetailPage : ViewBase
    {
        public NoteDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}