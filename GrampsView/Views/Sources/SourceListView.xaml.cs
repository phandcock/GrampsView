using GrampsView.ViewModels;

namespace GrampsView.Views
{
    public sealed partial class SourceListPage : ViewBasePage
    {
        public SourceListPage()
        {
            InitializeComponent();
            BindingContext = Ioc.Default.GetRequiredService<SourceListViewModel>();
        }
    }
}