namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class TagDetailPage : ViewBase
    {
        private TagDetailViewModel _viewModel { get; set; }

        public TagDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<TagDetailViewModel>();
        }
    }
}