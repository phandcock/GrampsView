namespace GrampsView.Views
{
    using GrampsView.ViewModels.Media;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class MediaListPage : ViewBasePage
    {
        private MediaListViewModel _viewModel { get; set; }

        public MediaListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<MediaListViewModel>();
        }
    }
}