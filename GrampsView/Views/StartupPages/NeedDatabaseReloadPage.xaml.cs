namespace GrampsView.Views
{
    using GrampsView.ViewModels.StartupPages;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class NeedDatabaseReloadPage : ViewBasePage
    {
        private NeedDatabaseReloadViewModel _viewModel { get; set; }

        public NeedDatabaseReloadPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<NeedDatabaseReloadViewModel>();
        }
    }
}