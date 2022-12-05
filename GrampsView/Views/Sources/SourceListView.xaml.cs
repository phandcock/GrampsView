namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class SourceListPage : ViewBasePage
    {
        private SourceListViewModel _viewModel { get; set; }

        public SourceListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<SourceListViewModel>();
        }
    }
}