namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class NoteListPage : ViewBasePage
    {
        private NoteListViewModel _viewModel { get; set; }

        public NoteListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<NoteListViewModel>();
        }
    }
}