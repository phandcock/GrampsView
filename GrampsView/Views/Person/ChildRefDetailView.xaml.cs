namespace GrampsView.Views
{
    using GrampsView.ViewModels.Person;

    using Microsoft.Extensions.DependencyInjection;

    public partial class ChildRefDetailPage : ViewBasePage
    {
        private ChildRefDetailViewModel _viewModel { get; set; }

        public ChildRefDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<ChildRefDetailViewModel>();
        }
    }
}