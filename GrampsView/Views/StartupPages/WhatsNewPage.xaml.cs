namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class WhatsNewPage : ViewBase
    {
        public WhatsNewPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<WhatsNewViewModel>();
        }
    }
}