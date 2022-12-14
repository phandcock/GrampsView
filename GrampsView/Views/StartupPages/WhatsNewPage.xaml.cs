using GrampsView.ViewModels.StartupPages;

using System.Diagnostics;

namespace GrampsView.Views.StartupPages
{
    public sealed partial class WhatsNewPage : ViewBasePage
    {
        public WhatsNewPage()
        {
            Debug.WriteLine($"WhatsNewPage creation");

            InitializeComponent();

            BindingContext = Ioc.Default.GetRequiredService<WhatsNewViewModel>();
        }
    }
}