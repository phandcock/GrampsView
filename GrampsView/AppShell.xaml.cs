namespace GrampsView
{
    using GrampsView.Views;

    using Xamarin.Forms;

    public partial class AppShell : Shell
    {
        public AppShell()

        {
            InitializeComponent();

            RegisterMiscRoutes();

            RegisterDetailRoutes();
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        private void RegisterDetailRoutes()
        {
            Routing.RegisterRoute(nameof(AddressDetailPage), typeof(AddressDetailPage));
            Routing.RegisterRoute(nameof(AttributeDetailPage), typeof(AttributeDetailPage));

            Routing.RegisterRoute(nameof(CitationDetailPage), typeof(CitationDetailPage));

            Routing.RegisterRoute(nameof(EventDetailPage), typeof(EventDetailPage));

            Routing.RegisterRoute(nameof(FamilyDetailPage), typeof(FamilyDetailPage));

            Routing.RegisterRoute(nameof(MediaDetailPage), typeof(MediaDetailPage));

            Routing.RegisterRoute(nameof(NoteDetailPage), typeof(NoteDetailPage));

            Routing.RegisterRoute(nameof(PersonDetailPage), typeof(PersonDetailPage));
            Routing.RegisterRoute(nameof(PersonNameDetailPage), typeof(PersonNameDetailPage));
            Routing.RegisterRoute(nameof(PlaceDetailPage), typeof(PlaceDetailPage));

            Routing.RegisterRoute(nameof(RepositoryDetailPage), typeof(RepositoryDetailPage));

            Routing.RegisterRoute(nameof(SourceDetailPage), typeof(SourceDetailPage));

            Routing.RegisterRoute(nameof(TagDetailPage), typeof(TagDetailPage));
        }

        private void RegisterMiscRoutes()
        {
            Routing.RegisterRoute(nameof(FileInputHandlerPage), typeof(FileInputHandlerPage));
            Routing.RegisterRoute(nameof(FirstRunPage), typeof(FirstRunPage));

            Routing.RegisterRoute(nameof(NeedDatabaseReloadPage), typeof(NeedDatabaseReloadPage));

            Routing.RegisterRoute(nameof(WhatsNewPage), typeof(WhatsNewPage));
        }
    }
}