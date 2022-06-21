namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class NoteDetailPage : ViewBasePage
    {
        private NoteDetailViewModel _viewModel { get; set; }

        public NoteDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<NoteDetailViewModel>();
        }
    }
}