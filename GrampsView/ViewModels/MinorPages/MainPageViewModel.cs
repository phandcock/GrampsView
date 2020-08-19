namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;
    using GrampsView.Events;
    using GrampsView.Services;
    using GrampsView.Views;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services.Dialogs;

    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using Xamarin.Forms;

    public class MainPageViewModel : ViewModelBase
    {
        private readonly IDatabaseReloadDisplayService _DatabaseReloadDisplayService = new DatabaseReloadDisplayService();
        private readonly IFirstRunDisplayService _FirstRunDisplayService = new FirstRunDisplayService();
        private readonly IWhatsNewDisplayService _WhatsNewDisplayService = new WhatsNewDisplayService();
        private string CurrentPage = string.Empty;

        public MainPageViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService, FirstRunDisplayService iocFirstRunDisplayService, WhatsNewDisplayService iocWhatsNewDisplayService, DatabaseReloadDisplayService iocDatabaseReloadDisplayService, IDialogService dialogService)
                                                    : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            _WhatsNewDisplayService = iocWhatsNewDisplayService;

            _FirstRunDisplayService = iocFirstRunDisplayService;

            _DatabaseReloadDisplayService = iocDatabaseReloadDisplayService;

            BaseDialogService = dialogService;

            BaseEventAggregator.GetEvent<AppStartFirstRunEvent>().Subscribe(ServiceFirstRun, ThreadOption.UIThread);

            BaseEventAggregator.GetEvent<AppStartLoadDataEvent>().Subscribe(ServiceLoadData, ThreadOption.UIThread);

            BaseEventAggregator.GetEvent<AppStartReloadDatabaseEvent>().Subscribe(ServiceReloadDatabase, ThreadOption.UIThread);

            BaseEventAggregator.GetEvent<AppStartWhatsNewEvent>().Subscribe(ServiceWhatsNew, ThreadOption.UIThread);

            BaseEventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(LoadHubPage, ThreadOption.UIThread);

            BaseEventAggregator.GetEvent<DialogBoxEvent>().Subscribe(ErrorActionDialog, ThreadOption.UIThread);

            BaseEventAggregator.GetEvent<PageNavigateEvent>().Subscribe(OnNavigateCommandExecuted, ThreadOption.UIThread);

            BaseEventAggregator.GetEvent<PageNavigateParmsEvent>().Subscribe(OnNavigateParmsCommandExecuted, ThreadOption.UIThread);

            // Build the Menu
            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);

            MainMenuItems = new List<MainMenuItem>()

            {
                new MainMenuItem() { Title = "Hub", Icon = CommonConstants.IconHub, TargetType = nameof(HubPage) },

                new MainMenuItem() { Title = "Bookmarks", Icon = CommonConstants.IconBookMark, TargetType = nameof(BookMarkListPage) },

                new MainMenuItem() { Title = "Citations", Icon = CommonConstants.IconCitation, TargetType = nameof(CitationListPage) },

                new MainMenuItem() { Title = "Events", Icon = CommonConstants.IconEvents, TargetType = nameof(EventListPage) },

                new MainMenuItem() { Title = "Families", Icon = CommonConstants.IconFamilies, TargetType = nameof(FamilyListPage) },

                new MainMenuItem() { Title = "Media", Icon = CommonConstants.IconMedia, TargetType = nameof(MediaListPage) },

                new MainMenuItem() { Title = "Notes", Icon = CommonConstants.IconNotes, TargetType = nameof(NoteListPage) },

                new MainMenuItem() { Title = "People", Icon = CommonConstants.IconPeople, TargetType = nameof(PersonListPage) },

                new MainMenuItem() { Title = "Places", Icon = CommonConstants.IconPlace, TargetType = nameof(PlaceListPage) },

                new MainMenuItem() { Title = "Repositories", Icon = CommonConstants.IconRepository, TargetType = nameof(RepositoryListPage) },

                new MainMenuItem() { Title = "Sources", Icon = CommonConstants.IconSource, TargetType = nameof(SourceListPage) },

                new MainMenuItem() { Title = "Tags", Icon = CommonConstants.IconTag, TargetType = nameof(TagListPage) },

                            //<!--
                            //    new NavigationViewItemSeparator
                            //    {
                            //    },
                            //-->

                new MainMenuItem() { Title = "PeopleGraph", Icon = CommonConstants.IconPeopleGraph, TargetType = nameof(PeopleGraphPage) },

                            //<!--
                            //    new NavigationViewItemSeparator
                            //    {
                            //    },
                            //-->

                new MainMenuItem() { Title = "Search", Icon = CommonConstants.IconSearch, TargetType = nameof(SearchPage) },

                new MainMenuItem() { Title = "Choose data file", Icon = CommonConstants.IconChooseDataFile, TargetType = nameof(FileInputHandlerPage) },

                new MainMenuItem() { Title = "Settings", Icon = CommonConstants.IconSettings, TargetType = nameof(SettingsPage) },

                new MainMenuItem() { Title = "About", Icon = CommonConstants.IconAbout, TargetType = nameof(AboutPage) },
            };
        }

        public List<MainMenuItem> MainMenuItems { get; }

        public DelegateCommand<string> NavigateCommand { get; }

        public void ErrorActionDialog(ActionDialogArgs argADA)
        {
            Contract.Assert(argADA != null);

            DialogParameters t = new DialogParameters
            {
                { "adaArgs", argADA },
            };

            //Using the dialog service as-is
            BaseDialogService.ShowDialog("ErrorDialog", t);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="parameters">
        /// The navigation parameters.
        /// </param>
        /// <returns>
        /// Null Task.
        /// </returns>
        public override void PopulateViewModel()
        {
            //// Start chain
            ///     Check if data already loaded
            ///     FirstRun
            ///     Whats New
            ///     DataBase Reload Needed
            ///     Load Data
            ///     Goto Hub Page
            if (DataStore.DS.IsDataLoaded)
            {
                OnNavigateCommandExecuted(nameof(HubPage));
                return;
            }

            ServiceFirstRun();
        }

        public void ServiceFirstRun()
        {
            if (!_FirstRunDisplayService.ShowIfAppropriate(BaseEventAggregator))
            {
                BaseEventAggregator.GetEvent<AppStartWhatsNewEvent>().Publish();
            }
        }

        public void ServiceLoadData()
        {
            if (CommonLocalSettings.DataSerialised)
            {
                // Start data load
                BaseEventAggregator.GetEvent<PageNavigateEvent>().Publish(nameof(MessageLogPage));
                BaseEventAggregator.GetEvent<DataLoadStartEvent>().Publish(false);
                return;
            }

            // No Serialised Data and made it this far so some problem has occurred. Load everything
            // from the beginning.
            BaseEventAggregator.GetEvent<PageNavigateEvent>().Publish(nameof(FileInputHandlerPage));
        }

        public void ServiceReloadDatabase()
        {
            if (!_DatabaseReloadDisplayService.ShowIfAppropriate(BaseEventAggregator))
            {
                BaseEventAggregator.GetEvent<AppStartLoadDataEvent>().Publish();
            }
        }

        public void ServiceWhatsNew()
        {
            if (!_WhatsNewDisplayService.ShowIfAppropriate(BaseEventAggregator))
            {
                BaseEventAggregator.GetEvent<AppStartReloadDatabaseEvent>().Publish();
            }
        }

        private void LoadHubPage(object obj)
        {
            OnNavigateCommandExecuted(nameof(HubPage));
        }

        private async void OnNavigateCommandExecuted(string page)
        {
            try
            {
                if (page != CurrentPage)
                {
                    CurrentPage = page;

                    INavigationResult result = await BaseNavigationService.NavigateAsync(nameof(NavigationPage) + "/" + page.Trim()).ConfigureAwait(false);

                    if (!result.Success)
                    {
                        DataStore.CN.NotifyException("OnNavigateCommandExecuted", result.Exception);
                    }
                }
            }
            catch (System.Exception ex)
            {
                DataStore.CN.NotifyException("OnNavigateCommandExecuted", ex);
                throw;
            }
        }

        private async void OnNavigateParmsCommandExecuted(INavigationParameters obj)
        {
            obj.TryGetValue(CommonConstants.NavigationParameterTargetView, out string target);

            //if (target != CurrentPage)
            //{
            CurrentPage = target;

            string t = nameof(NavigationPage) + "/" + target.Trim();

            INavigationResult result = await BaseNavigationService.NavigateAsync(t, obj).ConfigureAwait(false);

            if (!result.Success)
            {
                DataStore.CN.NotifyException("OnNavigateParmsCommandExecuted", result.Exception);
            }
            //}
        }
    }
}