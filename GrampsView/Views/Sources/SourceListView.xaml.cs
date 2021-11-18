namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class SourceListPage : ViewBase
    {
        public SourceListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<SourceListViewModel>();
        }
    }
}