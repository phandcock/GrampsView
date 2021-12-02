namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class MediaListPage : ViewBase
    {
        private MediaListViewModel _viewModel { get; set; }

        public MediaListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<MediaListViewModel>();
        }
    }
}