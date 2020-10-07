namespace GrampsView.ViewModels
{
    using GrampsView.Assets.Strings;
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.UserControls;

    using Prism.Events;
    using Prism.Navigation;

    using System.Diagnostics.Contracts;
    using System.Windows.Input;

    using Xamarin.Forms;

    /// <summary>
    /// Search ViewModel class.
    /// </summary>
    /// <seealso cref="GrampsView.ViewModels.ViewModelBase"/>
    public class SearchPageViewModel : ViewModelBase
    {
        /// <summary>
        /// The search command backing store.
        /// </summary>
        private ICommand _searchCommand;

        /// <summary>
        /// The local search text.
        /// </summary>
        private string _SearchText = string.Empty;

        private string lastArg = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common Logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// Event Aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// NavigationService
        /// </param>
        public SearchPageViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Search Page";

            BaseTitleIcon = CommonConstants.IconSearch;

            SearchButtonCommand = new Command<string>(SearchProcessQuery);
        }

        public CardGroupBase<HLinkAdressModel> AddressList
        {
            get
            {
                return DV.AddressDV.Search(SearchText);
            }
        }

        public CardGroupBase<HLinkCitationModel> CitationList
        {
            get
            {
                return DV.CitationDV.Search(SearchText);
            }
        }

        /// <summary>Gets or sets a value indicating whether [search items found].</summary>
        /// <value>
        ///   <c>true</c> if [search items found]; otherwise, <c>false</c>.</value>
        public bool SearchItemsFound { get; set;  }


        public CardGroupBase<HLinkEventModel> EventList
        {
            get
            {
                return DV.EventDV.Search(SearchText);
            }
        }

        public CardGroupBase<HLinkFamilyModel> FamilyList
        {
            get
            {
                return DV.FamilyDV.Search(SearchText);
            }
        }

        public CardGroupBase<HLinkMediaModel> MediaList
        {
            get
            {
                return DV.MediaDV.Search(SearchText);
            }
        }

        public CardGroupBase<HLinkNoteModel> NoteList
        {
            get
            {
                return DV.NoteDV.Search(SearchText);
            }
        }

        public CardGroupBase<HLinkPersonModel> PersonList
        {
            get
            {
                return DV.PersonDV.Search(SearchText);
            }
        }

        public CardGroupBase<HLinkPersonNameModel> PersonNameList
        {
            get
            {
                return DV.PersonNameDV.Search(SearchText);
            }
        }

        public CardGroupBase<HLinkPlaceModel> PlaceList
        {
            get
            {
                return DV.PlaceDV.Search(SearchText);
            }
        }

        /// <summary>
        /// Gets the search button command.
        /// </summary>
        /// <value>
        /// The search button command.
        /// </value>
        public ICommand SearchButtonCommand { get; private set; }

        /// <summary>
        /// Handles the search command.
        /// </summary>
        /// <value>
        /// The search command.
        /// </value>
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command<string>((text) =>
                {
                    // TODO Fix search display as you go
                    //ProcessQuery(text, 10);
                }));
            }
        }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>
        /// The search text.
        /// </value>
        public string SearchText
        {
            get
            {
                return _SearchText;
            }

            set
            {
                if (!(value is null))
                {
                    SetProperty(ref _SearchText, value);
                }
            }
        }

        public override void PopulateViewModel()
        {
            // TODO Do we need this?
        }

        /// <summary>
        /// Processes the query.
        /// </summary>
        /// <param name="argSearch">
        /// Search Text.
        /// </param>
        /// <param name="argLimit">
        /// Search Limit for search terms found while typing.
        /// </param>
        public void ProcessQuery(string argSearch)
        {
            Contract.Assert(argSearch != null);

            if (argSearch.Length > 0)
            {
                SearchText = argSearch;

                SearchItemsFound = false;

                // Trigger refresh of View fields via INotifyPropertyChanged
                RaisePropertyChanged(string.Empty);
            }

        }

        /// <summary>
        /// Processes the search query.
        /// </summary>
        /// <param name="argSearch">
        /// </param>
        public void SearchProcessQuery(string argSearch)
        {
            // Handle issues with bounce onf EventToCommand
            if (lastArg != argSearch)
            {
                ProcessQuery(argSearch);
            }

            lastArg = argSearch;
        }
    }
}