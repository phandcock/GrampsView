namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class ChildRefDetailPage : ViewBase
    {
        private ChildRefDetailViewModel _viewModel { get; set; }

        public ChildRefDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<ChildRefDetailViewModel>();
        }
    }
}