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

            RegisterListRoutes();

            RegisterDetailRoutes();
        }

        private void RegisterDetailRoutes()
        {
            Routing.RegisterRoute(nameof(AddressDetailPage), typeof(AddressDetailPage));

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

        private void RegisterListRoutes()
        {
            Routing.RegisterRoute(nameof(BookMarkListPage), typeof(BookMarkListPage));

            Routing.RegisterRoute(nameof(CitationListPage), typeof(CitationListPage));

            Routing.RegisterRoute(nameof(EventListPage), typeof(EventListPage));

            Routing.RegisterRoute(nameof(FamilyListPage), typeof(FamilyListPage));

            Routing.RegisterRoute(nameof(MediaListPage), typeof(MediaListPage));

            Routing.RegisterRoute(nameof(NoteListPage), typeof(NoteListPage));

            Routing.RegisterRoute(nameof(PersonBirthdayPage), typeof(PersonBirthdayPage));
            Routing.RegisterRoute(nameof(PersonListPage), typeof(PersonListPage));
            Routing.RegisterRoute(nameof(PlaceListPage), typeof(PlaceListPage));

            Routing.RegisterRoute(nameof(RepositoryListPage), typeof(RepositoryListPage));

            Routing.RegisterRoute(nameof(SourceListPage), typeof(SourceListPage));

            Routing.RegisterRoute(nameof(TagListPage), typeof(TagListPage));
        }

        private void RegisterMiscRoutes()
        {
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));

            Routing.RegisterRoute(nameof(FileInputHandlerPage), typeof(FileInputHandlerPage));
            Routing.RegisterRoute(nameof(FirstRunPage), typeof(FirstRunPage));

            Routing.RegisterRoute(nameof(HubPage), typeof(HubPage));

            Routing.RegisterRoute(nameof(MessageLogPage), typeof(MessageLogPage));

            Routing.RegisterRoute(nameof(NeedDatabaseReloadPage), typeof(NeedDatabaseReloadPage));

            Routing.RegisterRoute(nameof(PeopleGraphPage), typeof(PeopleGraphPage));

            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));

            Routing.RegisterRoute(nameof(WhatsNewPage), typeof(WhatsNewPage));
        }
    }
}