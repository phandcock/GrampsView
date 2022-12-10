namespace GrampsView.Views
{
    using GrampsView.ViewModels.MinorModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class AttributeDetailPage : ViewBasePage
    {
        private AttributeDetailViewModel _viewModel { get; set; }

        public AttributeDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<AttributeDetailViewModel>();
        }
    }
}