namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using Prism.Events;

    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// Search ViewModel class.
    /// </summary>
    /// <seealso cref="GrampsView.ViewModels.ViewModelBase"/>
    public class SearchPageViewModel : ViewModelBase
    {
        private bool _isBusy;

        /// <summary>
        /// The search command backing store.
        /// </summary>
        private ICommand _searchCommand;

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
        public SearchPageViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Search Page";

            BaseTitleIcon = CommonConstants.IconSearch;

            SearchButtonCommand = new AsyncCommand<string>(buttonClickText => SearchProcessQuery(buttonClickText), _ => !IsBusy);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    SearchButtonCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public CardGroup ItemsFoundList
        {
            get; set;
        }

        = new CardGroup();

        /// <summary>
        /// Gets the search button command.
        /// </summary>
        /// <value>
        /// The search button command.
        /// </value>
        public IAsyncCommand<string> SearchButtonCommand
        {
            get;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [search items found].
        /// </summary>
        /// <value>
        /// <c>true</c> if [search items found]; otherwise, <c>false</c>.
        /// </value>
        public bool SearchItemsFound
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>
        /// The search text.
        /// </value>
        public string SearchText
        {
            get;

            set;
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
        public async Task ProcessQuery(string argSearch)
        {
            Contract.Assert(argSearch != null);

            SearchText = argSearch.Trim().ToLower(CultureInfo.CurrentCulture);

            if (SearchText.Length > 0)
            {
                SearchItemsFound = true;

                ItemsFoundList.Clear();

                ItemsFoundList.Add(DV.AddressDV.Search(SearchText));
                ItemsFoundList.Add(DV.CitationDV.Search(SearchText));
                ItemsFoundList.Add(DV.EventDV.Search(SearchText));
                ItemsFoundList.Add(DV.FamilyDV.Search(SearchText));
                ItemsFoundList.Add(DV.MediaDV.Search(SearchText));
                ItemsFoundList.Add(DV.NoteDV.Search(SearchText));
                ItemsFoundList.Add(DV.PersonDV.Search(SearchText));
                ItemsFoundList.Add(DV.PersonNameDV.Search(SearchText));
                ItemsFoundList.Add(DV.PlaceDV.Search(SearchText));

                if (ItemsFoundList.Count > 0)
                {
                    SearchItemsFound = true;
                }
                else
                {
                    SearchItemsFound = false;
                }
            }
        }

        /// <summary>
        /// Processes the search query.
        /// </summary>
        /// <param name="argSearch">
        /// </param>
        public async Task SearchProcessQuery(string argSearch)
        {
            // Handle issues with bounce onf EventToCommand
            if (lastArg != argSearch)
            {
                await ProcessQuery(argSearch);
            }

            lastArg = argSearch;
        }
    }
}