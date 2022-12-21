using GrampsView.Views;
using GrampsView.Views.StartupPages;

namespace GrampsView
{
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

        private static void RegisterMiscRoutes()
        {
            Routing.RegisterRoute(nameof(FileInputHandlerPage), typeof(FileInputHandlerPage));
            Routing.RegisterRoute(nameof(FirstRunPage), typeof(FirstRunPage));

            Routing.RegisterRoute(nameof(HubPage), typeof(NeedDatabaseReloadPage));

            Routing.RegisterRoute(nameof(NeedDatabaseReloadPage), typeof(NeedDatabaseReloadPage));

            Routing.RegisterRoute(nameof(WhatsNewPage), typeof(WhatsNewPage));
        }

        private void RegisterDetailRoutes()
        {
            Routing.RegisterRoute(nameof(AddressDetailPage), typeof(AddressDetailPage));
            Routing.RegisterRoute(nameof(AttributeDetailPage), typeof(AttributeDetailPage));

            Routing.RegisterRoute(nameof(ChildRefDetailPage), typeof(ChildRefDetailPage));
            Routing.RegisterRoute(nameof(CitationDetailPage), typeof(CitationDetailPage));

            Routing.RegisterRoute(nameof(DateSpanDetailPage), typeof(DateSpanDetailPage));
            Routing.RegisterRoute(nameof(DateRangeDetailPage), typeof(DateRangeDetailPage));
            Routing.RegisterRoute(nameof(DateStrDetailPage), typeof(DateStrDetailPage));
            Routing.RegisterRoute(nameof(DateValDetailPage), typeof(DateValDetailPage));

            Routing.RegisterRoute(nameof(EventDetailPage), typeof(EventDetailPage));

            Routing.RegisterRoute(nameof(FamilyDetailPage), typeof(FamilyDetailPage));

            Routing.RegisterRoute(nameof(MediaDetailPage), typeof(MediaDetailPage));

            Routing.RegisterRoute(nameof(NoteDetailPage), typeof(NoteDetailPage));

            Routing.RegisterRoute(nameof(PersonDetailPage), typeof(PersonDetailPage));
            Routing.RegisterRoute(nameof(PersonNameDetailPage), typeof(PersonNameDetailPage));
            Routing.RegisterRoute(nameof(PlaceDetailPage), typeof(PlaceDetailPage));

            Routing.RegisterRoute(nameof(RepositoryDetailPage), typeof(RepositoryDetailPage));
            Routing.RegisterRoute(nameof(RepositoryRefDetailPage), typeof(RepositoryRefDetailPage));

            Routing.RegisterRoute(nameof(SourceDetailPage), typeof(SourceDetailPage));

            Routing.RegisterRoute(nameof(TagDetailPage), typeof(TagDetailPage));

            Routing.RegisterRoute(nameof(FirstRunPage), typeof(FirstRunPage));
            Routing.RegisterRoute(nameof(WhatsNewPage), typeof(WhatsNewPage));
        }
    }
}