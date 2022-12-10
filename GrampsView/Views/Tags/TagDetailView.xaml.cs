namespace GrampsView.Views
{
    using GrampsView.ViewModels.Tags;

    using Microsoft.Extensions.DependencyInjection;

    public partial class TagDetailPage : ViewBasePage
    {
        private TagDetailViewModel _viewModel { get; set; }

        public TagDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<TagDetailViewModel>();
        }
    }
}