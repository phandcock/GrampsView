namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class TagListPage : ViewBase
    {
        private TagListViewModel _viewModel { get; set; }

        public TagListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<TagListViewModel>();
        }
    }
}