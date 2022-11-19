namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class TagListPage : ViewBasePage
    {
        private TagListViewModel _viewModel { get; set; }

        public TagListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetService<TagListViewModel>();
        }
    }
}