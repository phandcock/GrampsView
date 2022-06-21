namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class FileInputHandlerPage : ViewBasePage
    {
        private FileInputHandlerViewModel _viewModel { get; set; }

        public FileInputHandlerPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<FileInputHandlerViewModel>();
        }
    }
}