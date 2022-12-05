using GrampsView.ViewModels.MinorPages;

namespace GrampsView.Views
{
    public sealed partial class HubPage : ViewBasePage
    {
        public HubPage()
        {
            InitializeComponent();
            BindingContext = Ioc.Default.GetRequiredService<HubViewModel>();
        }
    }
}