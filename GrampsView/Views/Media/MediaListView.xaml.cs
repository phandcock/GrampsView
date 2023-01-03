namespace GrampsView.Views
{
    using GrampsView.ViewModels.Media;

    public sealed partial class MediaListPage : ViewBasePage
    {


        public MediaListPage()
        {
            InitializeComponent();
            BindingContext = Ioc.Default.GetRequiredService<MediaListViewModel>();
        }
    }
}