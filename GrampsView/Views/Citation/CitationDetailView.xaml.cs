using GrampsView.ViewModels.Citation;

namespace GrampsView.Views
{
    public partial class CitationDetailPage : ViewBasePage
    {
        public CitationDetailPage()
        {
            InitializeComponent();
            BindingContext = Ioc.Default.GetRequiredService<CitationDetailViewModel>();
        }
    }
}